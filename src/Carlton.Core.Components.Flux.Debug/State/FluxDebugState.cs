using Carlton.Core.Utilities.Logging;

namespace Carlton.Core.Components.Flux.Debug.State;

public class FluxDebugState
{
    public FluxDebugState(object state, IEnumerable<LogMessage> logMessages)
    {
        State = state;
        LogMessages = logMessages;
    }

    public IEnumerable<LogMessage> LogMessages { get; private set; }
    public object State { get; private set; }
}