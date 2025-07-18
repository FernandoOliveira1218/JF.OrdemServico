using JF.OrdemServico.Infra.Extensions;
using JF.OrdemServico.Worker.Extensions;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddInfra(builder.Configuration);

builder.Services.AddWorker();

var host = builder.Build();
host.Run();
