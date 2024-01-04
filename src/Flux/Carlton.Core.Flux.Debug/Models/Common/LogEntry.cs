using Microsoft.Extensions.Logging;

namespace Carlton.Core.Flux.Debug.Models.Common;

public record LogEntry
{
    public LogLevel LogLevel { get; init; }
    public int EventId { get; init; }
    public string EventName { get; init; }
    public string Message { get; init; }
    public ExceptionEntry  Exception { get; init; }
    public DateTime Timestamp { get; init; }
    public string Scopes { get; init; }
}
