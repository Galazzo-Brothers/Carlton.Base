using Carlton.Core.Flux.Debug.Extensions;
namespace Carlton.Core.Flux.Debug.State;

internal sealed record FluxDebugState
{
	//Logs
	public List<TraceLogMessageGroup> TraceLogMessageGroups { get; init; } = [];

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

	//Selected Log Messages
	public LogMessage? SelectedLogMessage { get; init; }
	public TraceLogMessage? SelectedTraceLogMessage { get; init; }

	//Table States
	public EventLogViewerFilterState EventLogViewerFilterState { get; init; } = new();

	//Main Application State
	public object State { get; private set; }
}

