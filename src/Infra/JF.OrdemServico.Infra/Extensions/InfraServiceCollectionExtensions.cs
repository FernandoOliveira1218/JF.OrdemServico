using JF.OrdemServico.Domain.Interfaces.Repositories;
using JF.OrdemServico.Infra.Data.Common;
using JF.OrdemServico.Infra.Data.Context;
using JF.OrdemServico.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JF.OrdemServico.Infra.Extensions;

public static class InfraServiceCollectionExtensions
{
    public static IServiceCollection AddInfra(this IServiceCollection services, IConfiguration configuration)
    {
        // Configuração do DbContext com PostgreSQL
        services.AddDbContext<OrdemServicoDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("PostgreSql")));

        // Repositórios
        services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
        services.AddScoped<IChamadoRepository, ChamadoRepository>();
        services.AddScoped<IClienteRepository, ClienteRepository>();

        return services;
    }
}