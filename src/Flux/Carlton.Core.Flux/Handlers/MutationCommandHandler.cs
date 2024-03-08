using Carlton.Core.Flux.Dispatchers.Mutations;
namespace Carlton.Core.Flux.Handlers;

public class MutationCommandHandler<TState>(
    IMutableFluxState<TState> _state,
    ILogger<MutationCommandHandler<TState>> _logger)
    : IMutationCommandHandler<TState>
{
    public async Task<Result<MutationCommandResult, FluxError>> Handle<TCommand>(MutationCommandContext<TCommand> context, CancellationToken cancellationToken)
    {
        using (_logger.BeginFluxComponentChildRequestLoggingScopes(context.RequestId))
        {
            var stateEventResult = await _state.ApplyMutationCommand(context.MutationCommand);

            if(stateEventResult.IsSuccess)
                context.MarkAsSucceeded();

            return stateEventResult.Match<Result<MutationCommandResult, FluxError>>
            (
                success => new MutationCommandResult(),
                error => error
            );
        }
    }
}

