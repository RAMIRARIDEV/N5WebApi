namespace N5WebApi.Domain.Abstractions;
public record SearchRequest
(
    int PageSize = 10,
    int PageNumber = 1,
    bool IsExport = false
);