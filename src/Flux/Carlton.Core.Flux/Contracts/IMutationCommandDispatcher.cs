using Carlton.Core.Flux.Dispatchers.Mutations;
namespace Carlton.Core.Flux.Contracts;

public record MutationCommandResult();

public interface IMutationCommandDispatcher<TState>
{
	internal Task<Result<MutationCommandResult, FluxError>> Dispatch<TCommand>(object sender, MutationCommandContext<TCommand> context, CancellationToken cancellationToken);
}

public static class IMutationCommandDispatcherExtensions
{
	public static async Task<Result<MutationCommandResult, FluxError>> Dispatch<TState, TCommand>(this IMutationCommandDispatcher<TState> dispatcher, object sender, TCommand command, CancellationToken cancellation)
	   => await dispatcher.Dispatch(sender, new MutationCommandContext<TCommand>(command), cancellation);

	public static async Task<Result<MutationCommandResult, FluxError>> Dispatch<TState, TCommand, TContext>(this IMutationCommandDispatcher<TState> dispatcher, object sender, TContext context, CancellationToken cancellation)
		where TContext : MutationCommandContext<TCommand>
	   => await dispatcher.Dispatch(sender, context, cancellation);
}

public abstract class MutationCommandDispatcherMiddlewareBase<TState> : IMutationCommandDispatcher<TState>
{
	public abstract Task<Result<MutationCommandResult, FluxError>> Dispatch<TCommand, FluxError>(object sender, MutationCommandContext<TCommand> context, CancellationToken cancellationToken);

	async Task<Result<MutationCommandResult, FluxError>> IMutationCommandDispatcher<TState>.Dispatch<TCommand>(object sender, MutationCommandContext<TCommand> context, CancellationToken cancellationToken)
	{
		return await Dispatch<TCommand, FluxError>(sender, context, cancellationToken);
	}
}


