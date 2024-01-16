namespace Carlton.Core.Flux.Contracts;

public record FluxStateChangedEventArgs(string StateEvent);

public interface IFluxStateObserver<TState>
{
    public event Func<FluxStateChangedEventArgs, Task> StateChanged;
}

public interface IFluxState<TState> : IFluxStateObserver<TState>
{
    public TState CurrentState { get; }
}

public interface IMutableFluxState<TState> : IFluxState<TState>
{
    public Task<string> ApplyMutationCommand<TCommand>(TCommand command);
}


