using JF.OrdemServico.Domain.Entities;
using JF.OrdemServico.Domain.Interfaces.Messages;
using JF.OrdemServico.Domain.Interfaces.Repositories;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace JF.OrdemServico.Worker.Workers;

public class ChamadoWorker : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IMessageBus _messageBus;

    public ChamadoWorker(IServiceScopeFactory scopeFactory, IMessageBus messageBus)
    {
        _scopeFactory = scopeFactory;
        _messageBus = messageBus;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _messageBus.ConsumirAsync("chamado-finalizado", async (ea) =>
        {
            try
            {
                var body = (ea as BasicDeliverEventArgs)?.Body ?? default;
                var json = Encoding.UTF8.GetString(body.ToArray());

                var chamado = JsonSerializer.Deserialize<Chamado>(json);
                if (chamado is null) return;

                using var scope = _scopeFactory.CreateScope();
                var repository = scope.ServiceProvider.GetRequiredService<IChamadoLogRepository>();

                await repository.AddAsync(chamado);
            }
            catch (Exception ex)
            {
                // TODO: log ou tratamento de erro
                Console.WriteLine($"Erro no worker: {ex.Message}");
            }
        });

        // Aguarda indefinidamente enquanto o worker escuta mensagens
        await Task.CompletedTask;
    }
}