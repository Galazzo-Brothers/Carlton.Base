namespace Carlton.Core.Components.Flux.Contracts;

public interface IMutationCommandDispatcher<TState>
{
    public Task<Unit> Dispatch<TCommand>(object sender, TCommand command, CancellationToken cancellationToken)
        where TCommand : MutationCommand;
}