using Carlton.Core.Flux.Debug.Extensions;
using Carlton.Core.Utilities.Extensions;
namespace Carlton.Core.Flux.Debug.State;

internal sealed record FluxDebugState
{
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

	//Main Application State
	public object State { get; private set; }
}

