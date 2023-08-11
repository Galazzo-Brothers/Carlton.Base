using Carlton.Core.Components.Flux.State;

namespace Carlton.Core.Components.Flux.Contracts;

public interface IFluxStateObserver<TState>
{
    public StateMutationEvents<TState> StateMutationEvents { get; }
    public event Func<string, Task> StateChanged;
}

