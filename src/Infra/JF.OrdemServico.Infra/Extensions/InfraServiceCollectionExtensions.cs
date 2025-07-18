using JF.OrdemServico.Domain.Interfaces.Messages;
using JF.OrdemServico.Domain.Interfaces.Repositories;
using JF.OrdemServico.Domain.Interfaces.Services;
using JF.OrdemServico.Infra.Authentication;
using JF.OrdemServico.Infra.Data.Common;
using JF.OrdemServico.Infra.Data.Context;
using JF.OrdemServico.Infra.Data.Repositories;
using JF.OrdemServico.Infra.Messages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace JF.OrdemServico.Infra.Extensions;

public static class InfraServiceCollectionExtensions
{
    public static IServiceCollection AddInfra(this IServiceCollection services, IConfiguration configuration)
    {
        // Configuração do DbContext com PostgreSQL
        services.AddSingleton<MongoContext>();
        services.AddDbContext<OrdemServicoDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("PostgreSql"), npgsqlOptions =>
        {
            npgsqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(10),
            errorCodesToAdd: null);
        }));

        // Configuracao da Autenthicação JWT
        services.AddScoped<IAuthService, AuthService>();

        var jwtSection = configuration.GetSection("JwtSettings");

        var jwtSettings = new JwtSettings(jwtSection["SecretKey"], jwtSection["Issuer"], jwtSection["Audience"], int.Parse(jwtSection["ExpirationMinutes"] ?? "60"));
        services.AddSingleton(jwtSettings);

        // Configuracao da MessageBus
        services.AddSingleton<IMessageBus, RabbitMqMessageBus>();

        var rabbitSection = configuration.GetSection("RabbitMQSettings");
        var rabbitFactory = new ConnectionFactory()
        {
            HostName = rabbitSection["Host"] ?? "localhost",
            UserName = rabbitSection["UserName"] ?? "guest",
            Password = rabbitSection["Password"] ?? "guest"
        };

        services.AddSingleton(rabbitFactory);

        // Repositórios
        services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
        services.AddScoped<IChamadoRepository, ChamadoRepository>();
        services.AddScoped<IClienteRepository, ClienteRepository>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        
        return services;
    }
}