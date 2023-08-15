namespace Carlton.Core.Components.Flux.Contracts;

public interface IMutationCommandDispatcher<TState>
{
    public Task<Unit> Dispatch<TCommand>(TCommand command, CancellationToken cancellationToken)
        where TCommand : MutationCommand;
}