using Microsoft.EntityFrameworkCore;
using N5WebApi.Application.Abstractions;
using N5WebApi.Application.App.Permissions.Dtos;
using N5WebApi.Application.Extensions;
using N5WebApi.Application.Mappers;
using N5WebApi.src.Infrastructure.Data;
using Permission = N5WebApi.src.Domain.Models.Permissions;

namespace N5WebApi.Application.App.Permissions.Services;

public class PermissionService(IUnitOfWork unitOfWork) : DBService<Permission>(unitOfWork)
{
    public async Task<List<PermissionResponse>> Search(SearchPermissionRequest request, CancellationToken cancellationToken)
        => await
        Filter(request)
        .Page(request)
        .Select((p) => p.ToDto())
        .ToListAsync(cancellationToken);

    public IQueryable<Permission> Filter(SearchPermissionRequest request) =>
        GetAll()
        .AsNoTracking()
        .Where(p => !request.Id.HasValue || request.Id == p.Id)
        .Where(p => string.IsNullOrEmpty(request.EmployeeForename) || p.EmployeeForename.Contains(request.EmployeeForename))
        .Where(p => string.IsNullOrEmpty(request.EmployeeSurename) || p.EmployeeSurename.Contains(request.EmployeeSurename))
        .Where(p => !request.PermisionType.HasValue || request.PermisionType == p.PermissionType)
        .Where(p => !request.PermissionDate.HasValue || request.PermissionDate == p.PermissionDate);
}
