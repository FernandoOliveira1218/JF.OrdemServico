using JF.OrdemServico.Domain.Interfaces.Repositories;
using JF.OrdemServico.Infra.Data.Common;
using JF.OrdemServico.Infra.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace JF.OrdemServico.Infra.Extensions;

public static class InfraServiceCollectionExtensions
{
    public static IServiceCollection AddInfra(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
        services.AddScoped<IChamadoRepository, ChamadoRepository>();
        services.AddScoped<IClienteRepository, ClienteRepository>();

        // Adicione contextos e configurações de banco aqui

        return services;
    }
}