using Carter;
using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using N5WebApi.Application.Abstractions;
using N5WebApi.Application.App.Permissions.Services;
using N5WebApi.Application.App.PermissionsTypes.Services;
using N5WebApi.Infrastructure.Dtos;
using N5WebApi.Infrastructure.Producers;
using N5WebApi.src.Domain.Models;
using N5WebApi.src.Infrastructure.Data;
using Serilog.Sinks.Elasticsearch;
using Serilog;
using System.Text.Json.Serialization;

namespace N5WebApi.src;

public static class IoC
{
    public static void AddKafka(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<KafkaSettings>(
        configuration.GetSection(nameof(KafkaSettings)));
        services.AddScoped<IEventPublisher, KafkaProducer>();
    }
    public static void AddElastic(this IServiceCollection services, IConfiguration configuration)
    {
        ElasticsearchClientSettings ElasticSetting = new ElasticsearchClientSettings(
                                                new Uri(Environment.GetEnvironmentVariable("ELASTIC_URL") ?? "http://localhost:9200")
                                                )
                                                .DefaultIndex(Environment.GetEnvironmentVariable("ELASTIC_DEFAULT_INDEX") ?? "DefaultIndexName")
                                                .ServerCertificateValidationCallback(CertificateValidations.AllowAll);

        services.AddSingleton(new ElasticsearchClient(ElasticSetting));
        services.AddScoped<IElasticService<Permissions>, ElasticService<Permissions>>();
        services.AddScoped<IElasticService<PermissionTypes>, ElasticService<PermissionTypes>>();

        Log.Logger = new LoggerConfiguration()
        .WriteTo.Console()
        .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(Environment.GetEnvironmentVariable("ELASTIC_URL") ?? "http://localhost:9200"))
        {
            AutoRegisterTemplate = true,
            IndexFormat = "N5"
        })
        .Enrich.FromLogContext()
        .CreateLogger();


    }
    public static void AddDataBase(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = Environment.GetEnvironmentVariable("ConnectionString") ?? configuration["ConnectionString"] ?? "";
        services.AddDbContext<Context>(x => x.UseSqlServer(connectionString));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    internal static void AddServices(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation()
            .AddFluentValidationClientsideAdapters()
            .AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

        services.AddScoped<PermissionService>();
        services.AddScoped<PermissionTypesService>();

        services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            options.JsonSerializerOptions.PropertyNamingPolicy = null;
            options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        });

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "N5 API",
                Version = "v1",
                Description = "A simple app for management Permissions"
            });
        });

        services.AddCarter();
        services.AddHttpClient();
    }
}
