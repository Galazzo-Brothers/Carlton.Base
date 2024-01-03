namespace Carlton.Core.Utilities.Logging;

public record LogMessage
{
    public LogLevel LogLevel { get; init; }
    public int EventId { get; init; }
    public string EventName { get; init; }
    public string Message { get; init; }
    public LoggedException LoggedException { get; init; }
    public DateTime Timestamp { get; init; }
    public string Scopes { get; init; }
}

public record LoggedException
{
    public string ExceptionType { get; init; }
    public string Message { get; init; }    
    public string StackTrace { get; init; }

    public LoggedException()
    {
    }

    public LoggedException(Exception exception)
        => (ExceptionType, Message, StackTrace) = (exception?.GetType()?.ToString(), exception?.Message, exception?.StackTrace);  
}