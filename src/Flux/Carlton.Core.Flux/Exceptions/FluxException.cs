namespace Carlton.Core.Flux.Exceptions;

public class FluxException(int eventId, BaseRequestContext context, string message, Exception innerException) : Exception(message, innerException)
{
    public int EventId { get; init; } = eventId;
    public BaseRequestContext Context { get; init; } = context;
}
