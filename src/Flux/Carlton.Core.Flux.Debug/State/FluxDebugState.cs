namespace Carlton.Core.Flux.Debug.State;

public record FluxDebugState
{
    public FluxDebugState(object state, IEnumerable<LogEntry> logMessages)
    {
        State = state;
        LogEntries = logMessages;
    }

    public FluxDebugState(object state)
        : this(state, new List<LogEntry>())
    {
    }

    public IEnumerable<LogEntry> LogEntries { get; init; } = new List<LogEntry>();
    public LogEntry SelectedLogEntry { get; init; } 
    public object State { get; private set; }
}