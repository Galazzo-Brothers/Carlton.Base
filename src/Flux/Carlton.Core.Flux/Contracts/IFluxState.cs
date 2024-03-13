namespace Carlton.Core.Flux.Contracts;
public record FluxStateChangedEventArgs(string StateEvent);

/// <summary>
/// Represents a subscriber to changes in the Flux state.
/// </summary>
/// <typeparam name="TState">The type of the Flux state.</typeparam>
public interface IFluxStateObserver<TState>
{
	/// <summary>
	/// Event raised when the Flux state changes.
	/// </summary>
	public event Func<FluxStateChangedEventArgs, Task> StateChanged;
}

/// <summary>
/// Represents the Flux state.
/// </summary>
/// <typeparam name="TState">The type of the Flux state.</typeparam>
public interface IFluxState<TState> : IFluxStateObserver<TState>
{
	/// <summary>
	/// Gets the current state of the Flux.
	/// </summary>
	public TState CurrentState { get; }
}


