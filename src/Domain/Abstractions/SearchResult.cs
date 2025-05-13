namespace N5WebApi.Domain.Abstractions;
public sealed class SearchResult<T> where T : class
{
    public SearchResult()
    {
        Values = Enumerable.Empty<T>();
    }
    public SearchResult(SearchRequest request, long count, List<T> values)
    {
        PageNumber = request.IsExport ? 1 : request.PageNumber;
        PageSize = request.IsExport ? 1 : request.PageSize;
        TotalPages = request.IsExport ? 1 : (int)Math.Ceiling(Convert.ToDecimal(count) / Convert.ToDecimal(request.PageSize));
        TotalResults = request.IsExport ? values.Count : count;
        Values = values;
    }
    public int PageNumber { get; private set; }
    public long PageSize { get; private set; }
    public int TotalPages { get; private set; }
    public long TotalResults { get; private set; }
    public IEnumerable<T> Values { get; private set; }
}