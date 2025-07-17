using JF.OrdemServico.Domain.Interfaces.Repositories;
using JF.OrdemServico.Domain.Interfaces.Services;
using JF.OrdemServico.Infra.Authentication;
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

        // Configuracao da Autenthicação JWT
        services.AddScoped<IAuthService, AuthService>();

        var jwtSection = configuration.GetSection("JwtSettings");

        var jwtSettings = new JwtSettings(jwtSection["SecretKey"], jwtSection["Issuer"], jwtSection["Audience"], int.Parse(jwtSection["ExpirationMinutes"] ?? "60"));
        services.AddSingleton(jwtSettings);

        // Repositórios
        services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
        services.AddScoped<IChamadoRepository, ChamadoRepository>();
        services.AddScoped<IClienteRepository, ClienteRepository>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();

        return services;
    }
}