using Carlton.Core.Flux.Debug.Extensions;
using Microsoft.Extensions.Logging;
namespace Carlton.Core.Flux.Debug.State;

public record FluxDebugState
{
    public FluxDebugState(object state, IEnumerable<LogMessage> logMessages)
    {
        State = state;
        LogMessages = logMessages;
    }

    public FluxDebugState(object state)
        : this(state, new List<LogMessage>())
    {
    }

    public IEnumerable<TraceLogMessageGroup> TraceLogMessageGroups { get; init; } = new List<TraceLogMessageGroup>();

    private IEnumerable<LogMessage> _logMessages = new List<LogMessage>();
    public IEnumerable<LogMessage> LogMessages 
    {
        get => _logMessages;
        init
        {
            _logMessages = value;
            TraceLogMessageGroups = value.MapLogMessagesToTraceLogMessage();
        }
    }

    public LogMessage SelectedLogMessage => LogMessages.ElementAt(SelectedLogMessageIndex);
    public TraceLogMessage SelectedTraceLogMessage => TraceLogMessageGroups.GetElementAtIndex(SelectedLogMessageIndex);

    public int SelectedLogMessageIndex { get; init; }
    public int SelectedTraceLogMessageIndex { get; init; }

    public EventLogViewerFilterState EventLogViewerFilterState { get; init; } = new();

    public object State { get; private set; }
}

public record EventLogViewerFilterState
{
    public IList<LogLevel> IncludedLogLevels { get; init; } = new List<LogLevel> { LogLevel.Trace, LogLevel.Debug, LogLevel.Information, LogLevel.Warning, LogLevel.Error, LogLevel.Critical };
    public string FilterText { get; init; } = string.Empty;
}