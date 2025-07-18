using JF.OrdemServico.Infra.Extensions;
using JF.OrdemServico.Worker.Workers;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHostedService<ChamadoWorker>();

builder.Services.AddInfra(builder.Configuration);

var host = builder.Build();
host.Run();
