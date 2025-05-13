namespace N5WebApi.Domain.Abstractions;

public sealed class Result
{
    public Result(bool isSuccess, ErrorException error, object? data = null)
    {
        if (isSuccess && error != ErrorException.None)
        {
            throw new ArgumentException("Invalid Error", nameof(error));
        }
        IsSuccess = isSuccess;
        Error = error;
        Data = data;
    }
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public ErrorException Error { get; }
    public object? Data { get; }
    public static Result Success(object? data = null) => new(true, ErrorException.None, data);
    public static Result Failure(ErrorException error) => new(false, error);
}
