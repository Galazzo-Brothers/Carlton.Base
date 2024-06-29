namespace Carlton.Core.Flux.Contracts;

/// <summary>
/// Represents a mutation operation on the Flux state.
/// </summary>
/// <typeparam name="TState">The type of the Flux state.</typeparam>
/// <typeparam name="TCommand">The type of the command used for mutation.</typeparam>
public interface IFluxStateMutation<TState, TCommand>
{
	/// <summary>
	/// Gets the name of the state event associated with this mutation.
	/// </summary>
	public string StateEvent { get; }

	/// <summary>
	/// Mutates the Flux state based on the provided command.
	/// </summary>
	/// <param name="state">The current state of the Flux.</param>
	/// <param name="command">The command to apply to the state.</param>
	/// <returns>The mutated state.</returns>
	public TState Mutate(TState state, TCommand command);
}

/// <summary>
/// Represents a ViewModelRemoteCommunication mutation on the Flux state.
/// </summary>
/// <typeparam name="TState">The type of the Flux state.</typeparam>
/// <typeparam name="TCommand">The type of the command used for mutation.</typeparam>
public abstract class IFluxStateViewModelReplacementMutation<TState, TViewModel>
	: IFluxStateMutation<TState, TViewModel>
{
	/// <inheritdoc/>
	public string StateEvent { get => $"{typeof(TViewModel).GetDisplayName()}RemoteCommunication"; }

	/// <inheritdoc/>
	public abstract TState Mutate(TState state, TViewModel viewModel);
}


internal static class IFluxStateMutationExtensions
{
	//used for an event sourced audit trail of state store changes
	public static TState Mutate<TState, TCommand>(this IFluxStateMutation<TState, TCommand> mutation, TState state, object command)
	{
		return mutation.Mutate(state, (TCommand)command);
	}
}
