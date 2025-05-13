using MediatR;
using N5WebApi.Domain.Abstractions;

namespace N5WebApi.Application.App.PermissionsTypes.Dtos;

internal record SearchPermissionTypeRequest(
    string? Description,
    long? Id
    ) : SearchRequest, IRequest<SearchResult<PermissionTypeResponse>>;
