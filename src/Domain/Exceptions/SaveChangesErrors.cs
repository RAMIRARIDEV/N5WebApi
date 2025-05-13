using N5WebApi.Domain.Abstractions;

namespace N5WebApi.Domain.Exceptions;
public class SaveChangesErrors
{
    public static readonly ErrorException SaveChangesError = new("SaveChangesError", "Error when saving.");
}
