using MediatR;
using N5WebApi.Domain.Abstractions;

namespace N5WebApi.Application.App.PermissionsTypes.Dtos;

internal record UpdatePermissionTypeRequest(
       string Description,
       long Id
    ) : IRequest<Result>;