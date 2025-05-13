using MediatR;
using Microsoft.EntityFrameworkCore;
using N5WebApi.Application.App.Permissions.Dtos;
using N5WebApi.Application.App.Permissions.Services;
using N5WebApi.Domain.Abstractions;
using System.Text.Json;

namespace N5WebApi.Application.App.Permissions.Handlers.QueryHandlers;

internal sealed class SearchPermissionsHandler(
    PermissionService _permissionService,
    ILogger<SearchPermissionsHandler> _logger)
    : IRequestHandler<SearchPermissionRequest, SearchResult<PermissionResponse>>
{
    public async Task<SearchResult<PermissionResponse>> Handle(SearchPermissionRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Searching Permission Request: {request}", JsonSerializer.Serialize(request));

        var result = await _permissionService.Search(request, cancellationToken);

        var count = await _permissionService.Filter(request).CountAsync();

        _logger.LogInformation("Response generated: {response}", JsonSerializer.Serialize(result));
        return new SearchResult<PermissionResponse>(request, count, result);
    }
}