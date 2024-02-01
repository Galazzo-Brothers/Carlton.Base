using Carlton.Core.Flux.Debug.Extensions;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
namespace Carlton.Core.Flux.Debug.State;

public record FluxDebugState
{
    public FluxDebugState()
    {

    }
    public FluxDebugState(object state, ReadOnlyCollection<LogMessage> logMessages)
    {
        State = state;
        LogMessages = logMessages;
    }

    public IReadOnlyList<TraceLogMessageGroup> TraceLogMessageGroups { get; init; } = new List<TraceLogMessageGroup>();

    private IReadOnlyList<LogMessage> _logMessages = new List<LogMessage>();
    public IReadOnlyList<LogMessage> LogMessages 
    {
        get => _logMessages;
        init
        {
            _logMessages = value;
            TraceLogMessageGroups = value.MapLogMessagesToTraceLogMessage().ToList();
        }
    }

    public LogMessage SelectedLogMessage => LogMessages.ElementAt(SelectedLogMessageIndex);
    public TraceLogMessage SelectedTraceLogMessage => TraceLogMessageGroups.GetElementAtIndex(SelectedTraceLogMessageIndex);

    public int SelectedLogMessageIndex { get; init; }
    public int SelectedTraceLogMessageIndex { get; init; }
    public IReadOnlyList<int> ExpandedTraceLogMessageIndexes { get; init; } = [];
    public EventLogViewerFilterState EventLogViewerFilterState { get; init; } = new();

    public object State { get; private set; }
}

public record EventLogViewerFilterState
{
    public IList<LogLevel> IncludedLogLevels { get; init; } = new List<LogLevel> { LogLevel.Trace, LogLevel.Debug, LogLevel.Information, LogLevel.Warning, LogLevel.Error, LogLevel.Critical };
    public string FilterText { get; init; } = string.Empty;
}