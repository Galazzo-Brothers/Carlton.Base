using Carlton.Core.Components.Flux.Models;
using Carlton.Core.Utilities.Logging;


namespace Carlton.Core.Components.Flux.Debug.State;

public class FluxDebugState
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

    public IEnumerable<LogMessage> LogMessages { get; private set; } = new List<LogMessage>();
    public object State { get; private set; }
}