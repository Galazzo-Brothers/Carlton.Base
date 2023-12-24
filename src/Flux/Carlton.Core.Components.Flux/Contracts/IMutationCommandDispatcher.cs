namespace Carlton.Core.Flux.Contracts;

public interface IMutationCommandDispatcher<TState>
{
    public Task Dispatch<TCommand>(object sender, TCommand command, CancellationToken cancellationToken)
        where TCommand : MutationCommand;
}