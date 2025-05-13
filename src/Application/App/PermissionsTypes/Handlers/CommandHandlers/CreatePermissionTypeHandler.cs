using FluentValidation;
using MediatR;
using N5WebApi.Application.Abstractions;
using N5WebApi.Application.App.PermissionsTypes.Dtos;
using N5WebApi.Application.App.PermissionsTypes.Services;
using N5WebApi.Application.App.PermissionsTypes.Validators;
using N5WebApi.Domain.Abstractions;
using N5WebApi.Domain.Exceptions;
using N5WebApi.src.Domain.Models;
using System.Text.Json;

namespace N5WebApi.Application.App.PermissionsTypes.Handlers.CommandHandlers;

internal sealed class CreatePermissionTypeHandler(
        PermissionTypesService _permissionTypesService,
        IElasticService<PermissionTypes> _persistanceService,
        ILogger<CreatePermissionTypeHandler> _logger) 
        : IRequestHandler<CreatePermissionTypeRequest, Result>
{
    public async Task<Result> Handle(CreatePermissionTypeRequest request, CancellationToken cancellationToken)
    {
        new CreatePermissionTypeValidator().ValidateAndThrow(request);
        _logger.LogInformation("Create Permission Type Request: {request}", JsonSerializer.Serialize(request));

        PermissionTypes permissionTypes = new(request.Description);

		try
        {
            _persistanceService.CreateDocument(permissionTypes);
             Result response = await _permissionTypesService.CreateAsync(permissionTypes);

            _logger.LogInformation("Response generated: {response}", JsonSerializer.Serialize(response));
            return response;

        }
        catch (Exception ex)
		{
            _logger.LogError("Unhandled Error in {handler}. Description: {description}", nameof(CreatePermissionTypeHandler), ex.Message);
            return Result.Failure(SaveChangesErrors.SaveChangesError);
        }
    }
}
