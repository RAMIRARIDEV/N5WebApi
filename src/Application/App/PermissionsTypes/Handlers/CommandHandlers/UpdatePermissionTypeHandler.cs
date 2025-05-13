using FluentValidation;
using MediatR;
using N5WebApi.Domain.Abstractions;
using N5WebApi.Domain.Exceptions;
using N5WebApi.src.Domain.Models;
using N5WebApi.Application.App.PermissionsTypes.Dtos;
using N5WebApi.Application.App.PermissionsTypes.Services;
using N5WebApi.Application.App.PermissionsTypes.Validators;
using N5WebApi.Application.Abstractions;
using System.Text.Json;

namespace N5WebApi.Application.App.PermissionsTypes.Handlers.CommandHandlers;

internal sealed class UpdatePermissionTypeHandler(
    PermissionTypesService _permissionTypesService,
    IElasticService<PermissionTypes> _persistanceService,
    ILogger<UpdatePermissionTypeHandler> _logger) 
    : IRequestHandler<UpdatePermissionTypeRequest, Result>
{
    public async Task<Result> Handle(UpdatePermissionTypeRequest request, CancellationToken cancellationToken)
    {
        await new UpdatePermissionTypeValidator().ValidateAndThrowAsync(request, cancellationToken);
        _logger.LogInformation("Updating Permission Type Request: {request}", JsonSerializer.Serialize(request));

        PermissionTypes permissions = await _permissionTypesService.GetByIdAsync(request.Id);

        if (permissions is null)
            return Result.Failure(GetFromDbErrors.GetFromDbError);
        permissions.ChangeDescription(request.Description);

        try
        {
            _persistanceService.CreateDocument(permissions);
             Result response = await _permissionTypesService.UpdateAsync(permissions);

            _logger.LogInformation("Response generated: {response}", JsonSerializer.Serialize(response));
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError("Unhandled Error in {handler}. Description: {description}", nameof(UpdatePermissionTypeHandler), ex.Message);

            return Result.Failure(SaveChangesErrors.SaveChangesError);
        }
    }
}
