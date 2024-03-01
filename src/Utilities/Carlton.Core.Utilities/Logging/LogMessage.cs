using Microsoft.Extensions.Logging;
namespace Carlton.Core.Utilities.Logging;

/// <summary>
/// Represents a log message entity.
/// </summary>
public record LogMessage
{
    /// <summary>
    /// Gets the log level of the message.
    /// </summary>
    public required LogLevel LogLevel { get; init; }

    /// <summary>
    /// Gets the event ID associated with the message.
    /// </summary>
    public required EventId EventId { get; init; }

    /// <summary>
    /// Gets the textual content of the message.
    /// </summary>
    public required string Message { get; init; }

    /// <summary>
    /// Gets the exception associated with the message.
    /// </summary>
    public required Exception Exception { get; init; }

    /// <summary>
    /// Gets the timestamp when the message was logged.
    /// </summary>
    public required DateTime Timestamp { get; init; }

    /// <summary>
    /// Gets the dictionary of scopes associated with the message.
    /// </summary>
    public Dictionary<string, object> Scopes { get; init; } = [];
}

