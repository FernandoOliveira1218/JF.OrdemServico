using JF.OrdemServico.Infra.Extensions;
using JF.OrdemServico.Application.Extensions;
using JF.OrdemServico.API.Configurations;

namespace JF.OrdemServico.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration config)
    {
        services.AddApplication();
        services.AddInfra(config);

        services.AddJwtConfiguration(config);
        services.AddSwaggerConfiguration();
        services.AddCorsConfiguration();
        services.AddApiVersioningConfiguration();

        return services;
    }
}