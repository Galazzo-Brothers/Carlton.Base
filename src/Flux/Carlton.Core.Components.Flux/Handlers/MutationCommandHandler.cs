namespace Carlton.Core.Components.Flux.Handlers;


public class MutationCommandHandler<TState, TCommand> : IMutationCommandHandler<TState, TCommand>
    where TCommand : MutationCommand
{
    protected IMutableFluxState<TState> State { get; }

    public MutationCommandHandler(IMutableFluxState<TState> state)
        => State = state;

    public async Task<Unit> Handle(TCommand command, CancellationToken cancellationToken)
    {
        await State.MutateState(command);
        return Unit.Value;
    }
}

