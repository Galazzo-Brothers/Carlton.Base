namespace Carlton.Core.Components.Flux.Mutations;

public class Memento<TState>
{
    public TState State { get; init; }

    public Memento(TState state)
    {
        State = state;
    }
}