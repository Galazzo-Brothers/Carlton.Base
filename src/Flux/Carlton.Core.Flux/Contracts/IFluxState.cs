namespace Carlton.Core.Flux.Contracts;

public record FluxStateChangedEventArgs(string StateEvent);

public interface IFluxStateObserver<TState>
{
	public event Func<FluxStateChangedEventArgs, Task> StateChanged;
}

internal interface IFluxState<TState> : IFluxStateObserver<TState>
{
	public TState CurrentState { get; }
}

internal interface IMutableFluxState<TState> : IFluxState<TState>
{
	internal Task<Result<TCommand, FluxError>> ApplyMutationCommand<TCommand>(TCommand command);
}


