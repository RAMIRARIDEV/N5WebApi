using N5WebApi.Domain.Abstractions;

namespace N5WebApi.Domain.Exceptions;

internal class GenerateEventErrors
{
    public static readonly ErrorException CreateEventError = new("CreateEventError", "Error when creating task.");
    public static readonly ErrorException ConsumerEventError = new("ConsumerEventError", "Error when consume task.");
}
