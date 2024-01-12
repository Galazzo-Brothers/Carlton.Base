namespace Carlton.Core.Utilities.Logging;

public record LogMessage
{
    public LogLevel LogLevel { get; init; }
    public EventId EventId { get; init; }
    public string Message { get; init; }
    public Exception Exception { get; init; }
    public DateTime Timestamp { get; init; }
    public IEnumerable<KeyValuePair<string, object>> Scopes { get; init; } = new List<KeyValuePair<string, object>>();
}

