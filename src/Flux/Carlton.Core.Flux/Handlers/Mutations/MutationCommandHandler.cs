namespace Carlton.Core.Flux.Handlers.Mutations;


public class MutationCommandHandler<TState>(IMutableFluxState<TState> state) : IMutationCommandHandler<TState>
{
    private readonly IMutableFluxState<TState> _state = state;

    public async Task Handle<TCommand>(MutationCommandContext<TCommand> context, CancellationToken cancellationToken)
    {
        await _state.MutateState(context.MutationCommand);
    }
}

