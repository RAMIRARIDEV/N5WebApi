using FluentValidation;
using MediatR;
using N5WebApi.Domain.Abstractions;
using N5WebApi.Domain.Exceptions;
using Permission = N5WebApi.src.Domain.Models.Permissions;
using N5WebApi.Application.Abstractions;
using N5WebApi.Application.App.Permissions.Dtos;
using N5WebApi.Application.App.Permissions.Services;
using N5WebApi.Application.App.Permissions.Validators;
using System.Text.Json;

namespace N5WebApi.Application.App.Permissions.Handlers.CommandHandlers;

internal sealed class UpdatePermissionHandler(
    PermissionService _permissionService,
    IElasticService<Permission> _persistanceService,
    ILogger<UpdatePermissionHandler> _logger)
    : IRequestHandler<UpdatePermissionRequest, Result>
{
    public async Task<Result> Handle(UpdatePermissionRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating Permission Request: {request}", JsonSerializer.Serialize(request));
        new UpdatePermissionsValidator().ValidateAndThrow(request);

        Permission permissions = await _permissionService.GetByIdAsync(request.Id);

        if (permissions is null)
            return Result.Failure(GetFromDbErrors.GetFromDbError);

        permissions.UpdatePermission(
            request.EmployeeForename,
            request.EmployeeSurename,
            request.PermisionType,
            request.PermissionDate
          );

        try
        {
            _persistanceService.CreateDocument(permissions);
            Result response = await _permissionService.UpdateAsync(permissions);

            _logger.LogInformation("Response generated: {response}", JsonSerializer.Serialize(response));
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError("Unhandled Error in {handler}. Description: {description}", nameof(UpdatePermissionHandler), ex.Message);

            return Result.Failure(SaveChangesErrors.SaveChangesError);
        }
    }
}
