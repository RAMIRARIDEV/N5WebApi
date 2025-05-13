namespace N5WebApi.Application.App.PermissionsTypes.Dtos;

internal record PermissionTypeResponse(
    string Description,
    long Id,
    DateTime CreateDate,
    DateTime? UpdateDate
    );