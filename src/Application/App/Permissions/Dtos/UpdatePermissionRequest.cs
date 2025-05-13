using MediatR;
using N5WebApi.Domain.Abstractions;

namespace N5WebApi.Application.App.Permissions.Dtos;

internal record UpdatePermissionRequest (   
    string? EmployeeForename,
    string? EmployeeSurename,
    long? PermisionType,
    DateTime? PermissionDate,
    long Id
    ): IRequest<Result>;
