using FluentValidation;
using JF.OrdemServico.Application.Common;
using JF.OrdemServico.Domain.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace JF.OrdemServico.Application.Extensions;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddScoped(typeof(IServiceBase<>), typeof(ServiceBase<>));
        // services.AddScoped<IChamadoService, ChamadoService>(); // Se houver um service concreto

        return services;
    }
}