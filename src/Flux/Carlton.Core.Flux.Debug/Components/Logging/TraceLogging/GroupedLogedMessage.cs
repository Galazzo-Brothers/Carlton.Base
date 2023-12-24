using Carlton.Core.Utilities.Logging;

namespace Carlton.Core.Flux.Debug.Components.Logging.TraceLogging;

public class GroupedLogMessage
{
    public required LogMessage StartingMessage { get; set; }
    public required LogMessage EndingMessage { get; set; }
    public IEnumerable<LogMessage> ChildMessages { get; set; } = new List<LogMessage>();
}
