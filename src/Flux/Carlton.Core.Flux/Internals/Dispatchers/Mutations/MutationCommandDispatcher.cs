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



