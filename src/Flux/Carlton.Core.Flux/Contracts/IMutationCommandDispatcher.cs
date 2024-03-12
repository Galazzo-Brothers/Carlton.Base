using Carlton.Core.Flux.Dispatchers;
using Carlton.Core.Flux.Internals;
namespace Carlton.Core.Flux.Contracts;

public record MutationCommandResult();

public interface IMutationCommandDispatcher<TState>
{
	internal Task<Result<MutationCommandResult, FluxError>> Dispatch<TCommand>(object sender, MutationCommandContext<TCommand> context, CancellationToken cancellationToken);
}

public static class IMutationCommandDispatcherExtensions
{
	public static async Task<MutationCommandResult> Dispatch<TState, TCommand>(this IMutationCommandDispatcher<TState> dispatcher, object sender, TCommand command, CancellationToken cancellation)
	{
		var context = new MutationCommandContext<TCommand>(command);
		var result = await dispatcher.Dispatch(sender, context, cancellation);
		return result.GetMutationResultOrThrow(context);
	}
}

public abstract class MutationCommandDispatcherMiddlewareBase<TState> : IMutationCommandDispatcher<TState>
{
	public abstract Task<Result<MutationCommandResult, FluxError>> Dispatch<TCommand, FluxError>(object sender, MutationCommandContext<TCommand> context, CancellationToken cancellationToken);

	async Task<Result<MutationCommandResult, FluxError>> IMutationCommandDispatcher<TState>.Dispatch<TCommand>(object sender, MutationCommandContext<TCommand> context, CancellationToken cancellationToken)
	{
		return await Dispatch<TCommand, FluxError>(sender, context, cancellationToken);
	}
}


