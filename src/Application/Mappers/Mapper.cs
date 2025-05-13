using N5WebApi.Application.App.Permissions.Dtos;
using N5WebApi.Application.App.PermissionsTypes.Dtos;
using N5WebApi.src.Domain.Models;

namespace N5WebApi.Application.Mappers;

internal static class Mapper
{
    internal static PermissionResponse ToDto(this Permissions permissions) => new(
        permissions.EmployeeForename,
        permissions.EmployeeSurename,
        permissions.PermissionType,
        permissions.PermissionDate,
        permissions.Id,
        permissions.CreationDate,
        permissions.ModificationDate
    );

    internal static PermissionTypeResponse ToDto(this PermissionTypes permissions) => new(
        permissions.Description,
        permissions.Id,
        permissions.CreationDate,
        permissions.ModificationDate
    );
}
