using Carlton.Core.Utilities.JsonConverters;
using Microsoft.Extensions.Logging;
using System.Text.Json.Serialization;

namespace Carlton.Core.Flux.Debug.Storage;

public class IndexedLogMessages
{
    public IndexedLogMessages(string key, string indexDate, IEnumerable<LogMessage> logMessage)
    {
        Key = key;
        IndexDate = indexDate;
        LogMessages = logMessage;
    }

    [JsonConstructor]
    private IndexedLogMessages()
    {
    }

    public string Key { get; set; }
    public string IndexDate { get; set; }
    public IEnumerable<LogMessage> LogMessages { get; set; }
}

public class IndexLogMessage
{
    public LogLevel LogLevel { get; set; }
    public int EventId { get; set; }
    public string EventName { get; set; }
    public string Message { get; set; }
    [JsonConverter(typeof(LogMessageConverter))]
    public Exception Exception { get; set; }
    public DateTime Timestamp { get; set; }
    public string Scopes { get; set; }
}

