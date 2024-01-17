namespace Carlton.Core.Flux.Handlers.Mutations;

public class MutationCommandHandler<TState>(
    IMutableFluxState<TState> _state,
    ILogger<MutationCommandHandler<TState>> _logger)
    : IMutationCommandHandler<TState>
{
    public async Task Handle<TCommand>(MutationCommandContext<TCommand> context, CancellationToken cancellationToken)
    {
        using (_logger.BeginFluxComponentChildRequestLoggingScopes(context.RequestId))
        {
            var stateEvent = await _state.ApplyMutationCommand(context.MutationCommand);
            context.MarkAsSucceeded(stateEvent);
        }
    }
}

