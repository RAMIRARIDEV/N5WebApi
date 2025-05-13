using N5WebApi.Domain.Abstractions;

namespace N5WebApi.src.Domain.Models;

public partial class Permissions(
    string EmployeeForename,
    string EmployeeSurename,
    long PermisionType,
    DateTime PermissionDate) : Entity
{

    public Permissions() : this(string.Empty, string.Empty, default, default) { }
    public string EmployeeForename { get; private set; } = EmployeeForename;
    public string EmployeeSurename { get; private set; } = EmployeeSurename;
    public long PermissionType { get; private set; } = PermisionType;
    public DateTime PermissionDate { get; private set; } = PermissionDate;
    public virtual PermissionTypes PermissionsTypes { get; private set; } = null!;

    public void UpdatePermission(
        string? EmployeeForename,
        string? EmployeeSurename,
        long? PermisionType,
        DateTime? PermissionDate)
    {
        if (!string.IsNullOrEmpty(EmployeeForename)) this.EmployeeForename = EmployeeForename;
        if (!string.IsNullOrEmpty(EmployeeSurename)) this.EmployeeSurename = EmployeeSurename;
        if (PermisionType.HasValue) PermissionType = PermisionType.Value;
        if (PermissionDate.HasValue) this.PermissionDate = PermissionDate.Value;

        SetModificationDate();
    }
}