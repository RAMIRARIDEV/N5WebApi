using MediatR;
using N5WebApi.Domain.Abstractions;

namespace N5WebApi.Application.App.Permissions.Dtos;

public record SearchPermissionRequest (
    string? EmployeeForename,
    string? EmployeeSurename,
    long? PermisionType,
    DateTime? PermissionDate,
    long? Id
) : SearchRequest, IRequest<SearchResult<PermissionResponse>>;
