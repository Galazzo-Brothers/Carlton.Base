namespace Carlton.Core.Flux.Handlers.Mutations;


public class MutationCommandHandler<TState>(IMutableFluxState<TState> state) : IMutationCommandHandler<TState>
{
    private readonly IMutableFluxState<TState> _state = state;

    public async Task Handle<TCommand>(TCommand command, CancellationToken cancellationToken)
        where TCommand : MutationCommand
    {
        await _state.MutateState(command);
    }
}

