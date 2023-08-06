namespace Carlton.Core.Components.Flux;
public abstract class StateProcessorBase<TState> : IStateProcessor<TState>
{
    protected TState State { get; init; }

    protected StateProcessorBase(TState state)
        => (State) = (state);

    public abstract Task ProcessCommand<TCommand>(object sender, TCommand command);

    public void RestoreState(Memento<TState> memento)
    {
        memento.State.Adapt(State);
    }

    public Memento<TState> SaveState()
    {
        return new Memento<TState>(State);
    }
}