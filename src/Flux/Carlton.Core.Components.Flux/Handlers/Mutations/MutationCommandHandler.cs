using Carlton.Core.Flux.Contracts;
using Carlton.Core.Flux.Models;

namespace Carlton.Core.Flux.Handlers.Mutations;


public class MutationCommandHandler<TState> : IMutationCommandHandler<TState>
{
    protected IMutableFluxState<TState> State { get; }

    public MutationCommandHandler(IMutableFluxState<TState> state)
        => State = state;

    public async Task Handle<TCommand>(TCommand command, CancellationToken cancellationToken)
        where TCommand : MutationCommand
    {
        await State.MutateState(command);
    }
}

