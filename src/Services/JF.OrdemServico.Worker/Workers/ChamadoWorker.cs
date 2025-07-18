using JF.OrdemServico.Domain.Interfaces.Messages;
using JF.OrdemServico.Worker.DTOs;
using JF.OrdemServico.Worker.Repositories;

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
        await _messageBus.ConsumirAsync<ChamadoLog>("chamado.finalizado", async chamado =>
        {
            if (chamado == null)
            {
                return;
            }

            using var scope = _scopeFactory.CreateScope();
            var repository = scope.ServiceProvider.GetRequiredService<IChamadoLogRepository>();

            await repository.AddAsync((ChamadoLog)chamado);
        });

        await Task.CompletedTask;
    }
}