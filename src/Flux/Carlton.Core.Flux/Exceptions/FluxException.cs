namespace Carlton.Core.Flux.Exceptions;

public class FluxException(int eventID, string message, Exception innerException) : Exception(message, innerException)
{
    public int EventID { get; init; } = eventID;
}
