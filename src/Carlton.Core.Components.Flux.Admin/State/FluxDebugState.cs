using Carlton.Core.Utilities.Logging;

namespace Carlton.Core.Components.Flux.Admin.State;

public class FluxDebugState<TState>
{
    public FluxDebugState(TState state, IEnumerable<LogMessage> logMessages)
    {
        State = state;
        LogMessages = logMessages;
    }

    public IEnumerable<LogMessage> LogMessages { get; private set; }
    public TState State { get; private set; }
}