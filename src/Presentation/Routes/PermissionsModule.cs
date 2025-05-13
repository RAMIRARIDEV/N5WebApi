using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using N5WebApi.Application.Abstractions;
using N5WebApi.Application.App.Permissions.Dtos;
using N5WebApi.Infrastructure.Dtos;

namespace N5WebApi.src.Presentation.Routes;

public sealed class PermissionsModule : CarterModule
{
    public PermissionsModule() : base("Api/Permissions")
    {
        WithTags("Permissions")
        .IncludeInOpenApi();
    }
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/Create", async (
            [FromBody] CreatePermissionRequest request,
            IEventPublisher eventPublisher,
            IOptions<KafkaSettings> kafkaSettings,
            ISender sender) =>
        {
            await eventPublisher.SendAsync(
                       kafkaSettings.Value.Topic,
                       new EventTask("Permissions/Create")
                       );

            return await sender.Send(request);
        })
            .WithDescription("Create new Permissions.");


        app.MapPut("/Update", async(
            [FromBody] UpdatePermissionRequest request,
            IEventPublisher eventPublisher,
            IOptions < KafkaSettings > kafkaSettings,
            ISender sender) =>
        {
            await eventPublisher.SendAsync(
                       kafkaSettings.Value.Topic,
                        new EventTask("Permissions/Update")
                    );

            return await sender.Send(request);
        })
             .WithDescription("Update exists Permissions.");
        
        app.MapPost("/Search", async (
            [FromBody] SearchPermissionRequest request,
            IEventPublisher eventPublisher,
            IOptions<KafkaSettings> kafkaSettings,
            ISender sender) =>
        {
            await eventPublisher.SendAsync(
                       kafkaSettings.Value.Topic,
                    new EventTask("Permissions/Search")
             );

            return await sender.Send(request);
        })
             .WithDescription("Get Permissions by filters.");
        
    }
}