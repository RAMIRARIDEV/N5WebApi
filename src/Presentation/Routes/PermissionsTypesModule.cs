using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using N5WebApi.Application.Abstractions;
using N5WebApi.Application.App.PermissionsTypes.Dtos;
using N5WebApi.Infrastructure.Dtos;

namespace N5WebApi.src.Presentation.Routes;

public sealed class PermissionsTypesModule : CarterModule
{
    public PermissionsTypesModule() : base("Api/PermissionsTypes")
    {
        WithTags("PermissionsTypes")
        .IncludeInOpenApi();
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/Create", async (
            [FromBody] CreatePermissionTypeRequest request,
            IEventPublisher eventPublisher,
            IOptions<KafkaSettings> kafkaSettings,
            ISender sender) =>
        {
            await eventPublisher.SendAsync(
                kafkaSettings.Value.Topic,
                    new EventTask("PermissionsTypes/Create")
                );

            return await sender.Send(request);
        })
            .WithDescription("Create new Permissions Types.");


        app.MapPut("/Update", async (
            [FromBody] UpdatePermissionTypeRequest request,
            IEventPublisher eventPublisher,
            IOptions<KafkaSettings> kafkaSettings,
            ISender sender) => 
        {
            await eventPublisher.SendAsync(
                kafkaSettings.Value.Topic,
                    new EventTask("PermissionsTypes/Update")
                    );

            return await sender.Send(request);
        })
            .WithDescription("Update exists Permissions Types.");

        app.MapPost("/Search", async (
            [FromBody] SearchPermissionTypeRequest request,
            IEventPublisher eventPublisher,
            IOptions<KafkaSettings> kafkaSettings,
            ISender sender) => 
        {
            await eventPublisher.SendAsync(
                kafkaSettings.Value.Topic,
                    new EventTask("PermissionsTypes/Search")
                    );

            return await sender.Send(request);
        })
             .WithDescription("Get Permissions Types by filters.");
        
    }
}
