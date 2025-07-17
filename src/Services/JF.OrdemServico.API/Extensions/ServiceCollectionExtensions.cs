using JF.OrdemServico.API.Configurations;
using JF.OrdemServico.API.DTOs.Mappings;
using JF.OrdemServico.API.Filters;
using JF.OrdemServico.Application.Extensions;
using JF.OrdemServico.Domain.Common;
using JF.OrdemServico.Infra.Extensions;

namespace JF.OrdemServico.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration config)
    {
        services.AddApplication();
        services.AddInfra(config);

        services.AddScoped<NotificationContext>();

        services.AddJwtConfiguration(config);
        services.AddCorsConfiguration();
        services.AddApiVersioningConfiguration();

        services.AddSwaggerConfiguration();
        services.AddEndpointsApiExplorer();

        services.AddAutoMapper
        (
            cfg =>
            cfg.AddProfile<MapProfile>()
        );

        services.AddScoped<ModelValidationFilter>();
        services.AddControllers(options =>
        {
            options.Filters.Add<ModelValidationFilter>();
        }).AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
        }); ;

        return services;
    }
}