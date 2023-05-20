namespace Carlton.Base.State;

public interface IStateMemento<TState>
{
    public void RestoreState(Memento<TState> memento);
    public Memento<TState> SaveState();
}

public class Memento<TState>
{
    public TState State { get; init; }

    public Memento(TState state)
    {
        State = state;
    }
}
