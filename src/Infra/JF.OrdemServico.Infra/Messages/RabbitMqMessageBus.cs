using JF.OrdemServico.Domain.Interfaces.Messages;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace JF.OrdemServico.Infra.Messages;

public class RabbitMqMessageBus : IMessageBus
{
    private readonly IConnection _connection;
    private readonly IChannel _channel;

    public RabbitMqMessageBus(IConnection connection)
    {
        _connection = connection;
        _channel = _connection.CreateChannelAsync().GetAwaiter().GetResult();
    }

    public async Task PublishAsync(string queue, object message)
    {
        await _channel.QueueDeclareAsync(queue, durable: true, exclusive: false, autoDelete: false);

        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);

        await _channel.BasicPublishAsync(exchange: "", routingKey: queue, body: body);
    }

    public async Task ConsumirAsync<T>(string queue, Func<object?, Task> handler)
    {
        await _channel.QueueDeclareAsync(queue: queue, durable: true, exclusive: false, autoDelete: false, arguments: null);

        var consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.ReceivedAsync += async (_, ea) =>
        {
            try
            {
                var json = Encoding.UTF8.GetString(ea.Body.ToArray());

                var message = JsonSerializer.Deserialize<T>(json);

                await handler(message);

                await _channel.BasicAckAsync(deliveryTag: ea.DeliveryTag, multiple: false);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro no processamento da mensagem: {ex.Message}");

                // Reenfileira a mensagem para tentar processar depois
                await _channel.BasicNackAsync(deliveryTag: ea.DeliveryTag, multiple: false, requeue: true);
            }
        };

        await _channel.BasicConsumeAsync(queue: queue, autoAck: false, consumer: consumer);
    }


    public void Dispose()
    {
        _channel?.Dispose();
        _connection?.Dispose();
    }
}