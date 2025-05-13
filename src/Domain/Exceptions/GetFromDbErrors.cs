using N5WebApi.Domain.Abstractions;

namespace N5WebApi.Domain.Exceptions;

internal class GetFromDbErrors
{
    public static readonly ErrorException GetFromDbError = new("GetFromDbError", "Error when searching.");
}
