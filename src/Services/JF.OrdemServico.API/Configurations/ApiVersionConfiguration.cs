using Microsoft.AspNetCore.Mvc;

namespace JF.OrdemServico.API.Configurations;

public static class ApiVersionConfiguration
{
    public static IServiceCollection AddApiVersioningConfiguration(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>

        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
        });

        return services;
    }
}