using Carlton.Core.Flux.Internals.Logging;
namespace Carlton.Core.Flux.Internals.Dispatchers.Mutations;

internal sealed class MutationCommandDispatcher<TState>(IServiceProvider serviceProvider) : IMutationCommandDispatcher<TState>
{
	private readonly IServiceProvider _serviceProvider = serviceProvider;

	public async Task<Result<MutationCommandResult, FluxError>> Dispatch<TCommand>(object sender, MutationCommandContext<TCommand> context, CancellationToken cancellationToken)
	{
		var handler = _serviceProvider.GetRequiredService<IMutationCommandHandler<TState>>();
		return await handler.Handle(context, cancellationToken);
	}
}

internal sealed class MutationCommandHandler<TState>(
	IMutableFluxState<TState> _state,
	ILogger<MutationCommandHandler<TState>> _logger)
	: IMutationCommandHandler<TState>
{
	public async Task<Result<MutationCommandResult, FluxError>> Handle<TCommand>(MutationCommandContext<TCommand> context, CancellationToken cancellationToken)
	{
		using (_logger.BeginFluxComponentChildRequestLoggingScopes(context.RequestId))
		{
			var stateEventResult = await _state.ApplyMutationCommand(context.MutationCommand);

			return stateEventResult.Match<Result<MutationCommandResult, FluxError>>
			(
				success => new MutationCommandResult(),
				error => error
			);
		}
	}
}

public abstract class MutationCommandDispatcherMiddlewareBase<TState>(IMutationCommandDispatcher<TState> _decorated) : IMutationCommandDispatcher<TState>
{
	public abstract Task<MutationCommandResult> Dispatch<TCommand>(object sender, CancellationToken cancellationToken, Func<Task<MutationCommandResult>> next);

	async Task<Result<MutationCommandResult, FluxError>> IMutationCommandDispatcher<TState>.Dispatch<TCommand>(object sender, MutationCommandContext<TCommand> context, CancellationToken cancellationToken)
	{
		async Task<MutationCommandResult> next() => (await _decorated.Dispatch(sender, context, cancellationToken))
			.GetMutationResultOrThrow(context);
		return await Dispatch<TCommand>(sender, cancellationToken, next);
	}
}



