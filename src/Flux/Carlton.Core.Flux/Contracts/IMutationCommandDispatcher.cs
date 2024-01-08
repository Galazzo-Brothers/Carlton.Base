namespace Carlton.Core.Flux.Contracts;

public interface IMutationCommandDispatcher<TState>
{
    public Task Dispatch<TCommand>(object sender, MutationCommandContext<TCommand> context, CancellationToken cancellationToken);
}