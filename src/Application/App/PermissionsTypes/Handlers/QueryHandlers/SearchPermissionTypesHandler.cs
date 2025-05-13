using MediatR;
using Microsoft.EntityFrameworkCore;
using N5WebApi.Application.App.PermissionsTypes.Dtos;
using N5WebApi.Application.App.PermissionsTypes.Services;
using N5WebApi.Domain.Abstractions;
using System.Text.Json;

namespace N5WebApi.Application.App.PermissionsTypes.Handlers.QueryHandlers;

internal sealed class SearchPermissionTypesHandler(
    PermissionTypesService _permissionTypesService,
    ILogger<SearchPermissionTypesHandler> _logger)
    : IRequestHandler<SearchPermissionTypeRequest, SearchResult<PermissionTypeResponse>>
{
    public async Task<SearchResult<PermissionTypeResponse>> Handle(SearchPermissionTypeRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Searching Permission Type Request: {request}", JsonSerializer.Serialize(request));

        var result = await _permissionTypesService.Search(request, cancellationToken);

        var count = await _permissionTypesService.Filter(request).CountAsync();

        _logger.LogInformation("Response generated: {response}", JsonSerializer.Serialize(result));

        return new SearchResult<PermissionTypeResponse>(request, count, result);
    }
}
