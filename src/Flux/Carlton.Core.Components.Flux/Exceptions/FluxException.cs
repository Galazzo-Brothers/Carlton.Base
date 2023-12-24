namespace Carlton.Core.Flux.Exceptions;

public class FluxException : Exception
{
    public int EventID { get; init; }

    public FluxException(int eventID, string message, Exception innerException)
        :base(message, innerException)
    {
        EventID = eventID;
    }
}
