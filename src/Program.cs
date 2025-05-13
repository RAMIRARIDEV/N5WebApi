using Carter;
using DotNetEnv;
using N5WebApi.Application.Extensions;
using N5WebApi.src;
using N5WebApi.src.Infrastructure.Data;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
Env.Load();

builder.Services.AddElastic(builder.Configuration);

builder.Services.AddDataBase(builder.Configuration);

builder.Services.AddKafka(builder.Configuration);

builder.Services.AddServices();

builder.WebHost.UseUrls(Environment.GetEnvironmentVariable("WebHostURL") ?? "http://0.0.0.0:80");

builder.Host.UseSerilog();

WebApplication app = builder.Build();

if (!builder.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<Context>();
        dbContext.Database.EnsureCreated();
        await DbServiceExtensions.WaitForSqlServerAsync(dbContext);
    }
}

app.UseSwagger();
app.UseSwaggerUI();

app.MapCarter();
app.Run();
