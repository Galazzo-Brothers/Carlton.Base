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

    public IEnumerable<LogMessage> LogMessages { get; init; } = new List<LogMessage>();
    public LogMessage SelectedLogMessage { get; init; }
    public TraceLogMessage SelectedTraceLogMessage { get; init; }
    public object State { get; private set; }
}