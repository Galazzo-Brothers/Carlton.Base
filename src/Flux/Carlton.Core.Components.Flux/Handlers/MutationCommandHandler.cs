namespace Carlton.Core.Components.Flux.Handlers;


public class MutationCommandHandler<TState> : IMutationCommandHandler<TState>
{
    protected IMutableFluxState<TState> State { get; }

    public MutationCommandHandler(IMutableFluxState<TState> state)
        => State = state;

    public async Task<Unit> Handle<TCommand>(TCommand command, CancellationToken cancellationToken)
        where TCommand : MutationCommand
    {
        await State.MutateState(command);
        return Unit.Value;
    }
}

