using Carlton.Core.Flux.Internals;
using Carlton.Core.Flux.Internals.Dispatchers.Mutations;
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
