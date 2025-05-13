namespace N5WebApi.Domain.Exceptions;

internal static class ValidatorException<T> where T : class
{
    public static string NotEmptyMessage(string variable) => string.Concat("Value cannot be empty: ", variable, ". Entity: ", typeof(T));

    public static string NotFoundMessage(string variable) => string.Concat("Value not found: ", variable, ". Entity: ", typeof(T));
}
