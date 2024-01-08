namespace Carlton.Core.Flux.Contracts;

public interface IMutationCommandHandler<TState>
{
    public Task Handle<TCommand>(MutationCommandContext<TCommand> context, CancellationToken cancellationToken);
}


