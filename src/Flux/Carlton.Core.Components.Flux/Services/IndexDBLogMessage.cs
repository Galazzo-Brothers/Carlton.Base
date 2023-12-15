using Carlton.Core.Utilities.Logging;
using System.Text.Json.Serialization;

namespace Carlton.Core.Components.Flux.Services;
public class IndexDBLogMessage
{
    public LogLevel LogLevel { get; set; }
    public int EventId { get; set; }
    public string EventName { get; set; }
    public string Message { get; set; }
    public string Exception { get; set; }
    public DateTime Timestamp { get; set; }
    public string Scopes { get; set; }

    [JsonConstructor]
    public IndexDBLogMessage()
    {
    }

    public IndexDBLogMessage(LogMessage log)
    {
        LogLevel = log.LogLevel;
        EventId = log.EventId;
        EventName = log.EventName;
        Message = log.Message;
        Exception = log?.Exception?.ToString();
        Timestamp = log.Timestamp;
        Scopes = log.Scopes;
    }
}
