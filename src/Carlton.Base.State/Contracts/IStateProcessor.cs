namespace Carlton.Base.State;

public interface IStateProcessor 
{
    public Task ProcessCommand<TCommand>(object sender, TCommand command)
        where TCommand : ICommand;
}

public interface IStateProcessor<TState> : IStateMemento<TState>
{
    public Task ProcessCommand<TCommand>(object sender, TCommand command)
        where TCommand : ICommand;
}