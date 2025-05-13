using Microsoft.EntityFrameworkCore;
using N5WebApi.Application.Abstractions;
using N5WebApi.Application.App.PermissionsTypes.Dtos;
using N5WebApi.Application.Extensions;
using N5WebApi.Application.Mappers;
using N5WebApi.src.Domain.Models;
using N5WebApi.src.Infrastructure.Data;

namespace N5WebApi.Application.App.PermissionsTypes.Services;

internal sealed class PermissionTypesService(IUnitOfWork unitOfWork) : DBService<PermissionTypes>(unitOfWork)
{
    internal async Task<List<PermissionTypeResponse>> Search(SearchPermissionTypeRequest request, CancellationToken cancellationToken)
        => await
        Filter(request)
        .Page(request)
        .Select(p => p.ToDto())
        .ToListAsync(cancellationToken);

    internal IQueryable<PermissionTypes> Filter(SearchPermissionTypeRequest request) =>
        GetAll()
        .AsNoTracking()
        .Include(p => p.Permissions)
        .Where(p => !request.Id.HasValue || request.Id == p.Id)
        .Where(p => string.IsNullOrEmpty(request.Description) || p.Description.Contains(request.Description));
}
