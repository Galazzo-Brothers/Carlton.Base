using Carlton.Core.Flux.Debug.Extensions;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using Carlton.Core.Utilities.Extensions;
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

    //Logs
    public IEnumerable<TraceLogMessageGroup> TraceLogMessageGroups { get; init; } = new List<TraceLogMessageGroup>();

    private IEnumerable<LogMessage> _logMessages = new List<LogMessage>();
    public IEnumerable<LogMessage> LogMessages 
    {
        get => _logMessages;
        init
        {
            _logMessages = value;
            TraceLogMessageGroups = value.MapLogMessagesToTraceLogMessage().ToList();
        }
    }

    //Selected Log Messages
    public int SelectedLogMessageIndex { get; init; }
    public int SelectedTraceLogMessageIndex { get; init; }
    public LogMessage? SelectedLogMessage => LogMessages.SafeGetAtIndex(SelectedLogMessageIndex);
    public TraceLogMessage? SelectedTraceLogMessage => TraceLogMessageGroups.GetElementAtIndex(SelectedTraceLogMessageIndex);

    //Table States
    public EventLogViewerFilterState EventLogViewerFilterState { get; init; } = new();
    public IEnumerable<int> ExpandedTraceLogMessageIndexes { get; init; } = [];
    public TableState EventLogTableState { get; init; } = new();
    public TableState TraceLogTableState { get; init; } = new();

    //Main Applicaiton State
    public object State { get; private set; }
}

public record TableState
{
    public int CurrentPage { get; init; } = 1;
    public int SelectedRowsPerPageOptsIndex { get; init; } = 0;
    public IEnumerable<int> RowsPerPageOpts { get; init; } = new List<int> { 5, 10, 25 };
    public string OrderByColum { get; init; } = string.Empty;
    public bool OrderAscending { get; init; } = true;
}

public record EventLogViewerFilterState
{
    public IEnumerable<LogLevel> IncludedLogLevels { get; init; } = new List<LogLevel> { LogLevel.Trace, LogLevel.Debug, LogLevel.Information, LogLevel.Warning, LogLevel.Error, LogLevel.Critical };
    public string FilterText { get; init; } = string.Empty;
}