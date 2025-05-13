using MediatR;
using N5WebApi.Domain.Abstractions;

namespace N5WebApi.Application.App.Permissions.Dtos;

public record CreatePermissionRequest(
    string EmployeeForename,
    string EmployeeSurename,
    long PermisionType,
    DateTime PermissionDate

    ) : IRequest<Result>;
