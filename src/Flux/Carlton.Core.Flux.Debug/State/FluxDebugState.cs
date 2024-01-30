using Carlton.Core.Flux.Debug.Extensions;

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

    private IEnumerable<LogMessage> _logMessages = new List<LogMessage>();
    public IEnumerable<LogMessage> LogMessages 
    {
        get => _logMessages;
        init
        {
            _logMessages = value;
            TraceLogMessages = value.MapLogMessagesToTraceLogMessage();
        }
    }
    public IEnumerable<TraceLogMessageGroup> TraceLogMessages { get; init; } = new List<TraceLogMessageGroup>();
    public LogMessage SelectedLogMessage { get; init; } = null;
    public TraceLogMessage? SelectedTraceLogMessage { get; init; }
    public object State { get; private set; }
}