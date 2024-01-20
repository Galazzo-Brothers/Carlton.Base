namespace Carlton.Core.Utilities.Logging;

public record LogMessage
{
    public required LogLevel LogLevel { get; init; }
    public required EventId EventId { get; init; }
    public required string Message { get; init; }
    public required Exception Exception { get; init; }
    public required DateTime Timestamp { get; init; }
    public IEnumerable<KeyValuePair<string, object>> Scopes { get; init; } = new List<KeyValuePair<string, object>>();
}

