namespace N5WebApi.Application.App.Permissions.Dtos;

public record PermissionResponse(
    string? EmployeeForename,
    string? EmployeeSurename,
    long? PermissionType,
    DateTime? PermissionDate,
    long? Id,
    DateTime CreateDate,
    DateTime? UpdateDate
    );