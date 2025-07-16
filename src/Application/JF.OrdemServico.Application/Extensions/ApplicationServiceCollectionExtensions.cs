using FluentValidation;
using JF.OrdemServico.Application.Services;
using JF.OrdemServico.Domain.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace JF.OrdemServico.Application.Extensions;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        services.AddScoped<IChamadoService, ChamadoService>();
        services.AddScoped<IClienteService, ClienteService>();

        return services;
    }
}