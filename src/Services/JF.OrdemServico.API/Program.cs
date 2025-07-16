
using JF.OrdemServico.API.Extensions;
using JF.OrdemServico.API.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddConfiguration(builder.Configuration);

var app = builder.Build();

app.UseAppConfiguration();

app.Run();