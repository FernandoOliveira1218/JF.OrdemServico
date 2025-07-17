using JF.OrdemServico.Domain.Interfaces.Messages;
using JF.OrdemServico.Domain.Interfaces.Repositories;
using JF.OrdemServico.Infra.Data.Context;
using JF.OrdemServico.Infra.Data.Repositories;
using JF.OrdemServico.Infra.Messages;
using JF.OrdemServico.Worker.Workers;
using RabbitMQ.Client;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHostedService<ChamadoWorker>();

builder.Services.AddSingleton<MongoContext>();
builder.Services.AddScoped<IChamadoRepository, ChamadoLogRepository>();

// Configuracao da MessageBus
builder.Services.AddSingleton<IMessageBus, RabbitMqMessageBus>();

var rabbitSection = builder.Configuration.GetSection("RabbitMQSettings");
var rabbitSettings = new ConnectionFactory()
{
    HostName = rabbitSection["Host"] ?? "localhost",
    UserName = rabbitSection["UserName"] ?? "guest",
    Password = rabbitSection["Password"] ?? "guest"
};

builder.Services.AddSingleton(rabbitSettings.CreateConnectionAsync().GetAwaiter().GetResult());

var host = builder.Build();
host.Run();
