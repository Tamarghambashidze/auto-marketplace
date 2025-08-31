using FinalProject.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});

builder.Services.ConfigureServices(builder.Configuration);

var app = builder.Build();

app.ConfigureMiddleware();

app.Run();
