using FluentValidation;
using MediatR;
using N5WebApi.Domain.Exceptions;
using Permission = N5WebApi.src.Domain.Models.Permissions;
using Result = N5WebApi.Domain.Abstractions.Result;
using N5WebApi.Application.Abstractions;
using N5WebApi.Application.App.Permissions.Dtos;
using N5WebApi.Application.App.Permissions.Services;
using N5WebApi.Application.App.Permissions.Validators;
using System.Text.Json;

namespace N5WebApi.Application.App.Permissions.Handlers.CommandHandlers;

public sealed class CreatePermissionHandler(
    PermissionService _permissionService,
    IElasticService<Permission> _elasticService,
    ILogger<CreatePermissionHandler> _logger) 
    : IRequestHandler<CreatePermissionRequest, Result>
{
    public async Task<Result> Handle(CreatePermissionRequest request, CancellationToken cancellationToken)
    {
        new CreatePermissionsValidator().ValidateAndThrow(request);
        _logger.LogInformation("Create Permission Request: {request}", JsonSerializer.Serialize(request));

        Permission newPermission = new(
                request.EmployeeForename,
                request.EmployeeSurename,
                request.PermisionType,
                request.PermissionDate
            );

        try
        {
            _elasticService.CreateDocument(newPermission);
            Result response =  await _permissionService.CreateAsync(newPermission);

            _logger.LogInformation("Response generated: {response}", JsonSerializer.Serialize(response));
            return response;
        }
        catch (Exception ex) 
        {
            _logger.LogError("Unhandled Error in {handler}. Description: {description}", nameof(CreatePermissionHandler), ex.Message);
            return Result.Failure(SaveChangesErrors.SaveChangesError);
        }
    }
}
