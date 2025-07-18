using JF.OrdemServico.Worker.Repositories;
using JF.OrdemServico.Worker.Workers;

namespace JF.OrdemServico.Worker.Extensions;

public static class WorkerServiceCollectionExtensions
{
    public static IServiceCollection AddWorker(this IServiceCollection services)
    {
        services.AddHostedService<ChamadoWorker>();

        // Repositórios
        services.AddScoped<IChamadoLogRepository, ChamadoLogRepository>();

        return services;
    }
}