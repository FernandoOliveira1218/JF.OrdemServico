using JF.OrdemServico.Domain.Interfaces.Messages;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace JF.OrdemServico.Infra.Messages;

public class RabbitMqMessageBus : IMessageBus
{
    private readonly IConnection _connection;

    public RabbitMqMessageBus(IConnection connection)
    {
        _connection = connection;
    }

    public async Task PublishAsync(string queue, object message)
    {
        using var channel = await _connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(queue, durable: true, exclusive: false, autoDelete: false);

        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);

        await channel.BasicPublishAsync(exchange: "", routingKey: queue, body: body);
    }
}