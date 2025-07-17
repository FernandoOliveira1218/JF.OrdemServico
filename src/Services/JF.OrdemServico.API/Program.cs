
using JF.OrdemServico.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddConfiguration(builder.Configuration);

var app = builder.Build();

app.UseAppConfiguration();

app.Run();