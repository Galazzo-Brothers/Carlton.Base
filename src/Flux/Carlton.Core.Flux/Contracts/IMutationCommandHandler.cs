namespace Carlton.Core.Flux.Contracts;

public interface IMutationCommandHandler<TState>
{
    public Task Handle<TCommand>(TCommand command, CancellationToken cancellationToken)
            where TCommand : MutationCommand;
}


