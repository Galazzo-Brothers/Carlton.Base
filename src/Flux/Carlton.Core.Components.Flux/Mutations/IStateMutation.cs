namespace Carlton.Core.Components.Flux.Mutations;

public interface IStateMutation<TState, TStateEvents, TCommand>
    where TState : IStateStoreEventDispatcher<TStateEvents>
    where TStateEvents : Enum
{
    public TStateEvents StateEvent { get; }
    public IStateStore<TStateEvents> Mutate(TState currentState, object sender, TCommand command);
}

