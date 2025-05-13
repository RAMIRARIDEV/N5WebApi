namespace N5WebApi.Domain.Abstractions;

public sealed record ErrorException(string Code, string? Description = null)
{
    public static readonly ErrorException None = new(string.Empty);
    public static implicit operator Result(ErrorException error) => Result.Failure(error);
};