using Carlton.Core.Flux.Internals;
using Carlton.Core.Flux.Internals.Dispatchers.Mutations;
namespace Carlton.Core.Flux.Contracts;

/// <summary>
/// Represents the result of a mutation command.
/// </summary>
public record MutationCommandResult();

/// <summary>
/// Defines a dispatcher for mutation commands in the Flux framework.
/// </summary>
/// <typeparam name="TState">The type of the Flux state.</typeparam>
public interface IMutationCommandDispatcher<TState>
{
	internal Task<Result<MutationCommandResult, FluxError>> Dispatch<TCommand>(object sender, MutationCommandContext<TCommand> context, CancellationToken cancellationToken);
}

/// <summary>
/// Provides extension methods for <see cref="IMutationCommandDispatcher{TState}"/>.
/// </summary>
public static class IMutationCommandDispatcherExtensions
{
	/// <summary>
	/// Dispatches a mutation command with the specified context.
	/// </summary>
	/// <typeparam name="TCommand">The type of the mutation command.</typeparam>
	/// <param name="sender">The sender of the mutation command.</param>
	/// <param name="context">The context of the mutation command.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A task representing the asynchronous operation, returning the result of the mutation command.</returns>
	public static async Task<MutationCommandResult> Dispatch<TState, TCommand>(this IMutationCommandDispatcher<TState> dispatcher, object sender, TCommand command, CancellationToken cancellation)
	{
		var context = new MutationCommandContext<TCommand>(command);
		var result = await dispatcher.Dispatch(sender, context, cancellation);
		return result.GetMutationResultOrThrow(context);
	}
}

/// <summary>
/// Base class for implementing middleware for mutation command dispatching in the Flux framework.
/// </summary>
/// <typeparam name="TState">The type of the state managed by the Flux framework.</typeparam>
public abstract class MutationCommandDispatcherMiddlewareBase<TState>(IMutationCommandDispatcher<TState> _decorated) : IMutationCommandDispatcher<TState>
{
	/// <summary>
	/// Dispatches a mutation command with the specified context.
	/// </summary>
	/// <typeparam name="TCommand">The type of the mutation command.</typeparam>
	/// <param name="sender">The sender of the mutation command.</param>
	/// <param name="context">The context of the mutation command.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A task representing the asynchronous operation, returning the result of the mutation command.</returns>
	public abstract Task<MutationCommandResult> Dispatch<TCommand>(object sender, CancellationToken cancellationToken, Func<Task<MutationCommandResult>> next);

	async Task<Result<MutationCommandResult, FluxError>> IMutationCommandDispatcher<TState>.Dispatch<TCommand>(object sender, MutationCommandContext<TCommand> context, CancellationToken cancellationToken)
	{
		async Task<MutationCommandResult> next() => (await _decorated.Dispatch(sender, context, cancellationToken))
			.GetMutationResultOrThrow(context);
		return await Dispatch<TCommand>(sender, cancellationToken, next);
	}
}
