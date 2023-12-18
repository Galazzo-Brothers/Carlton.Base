namespace Carlton.Core.Components.Flux.Contracts;

public interface IMutationCommandHandler<TState>
{
    public Task<Unit> Handle<TCommand>(TCommand command, CancellationToken cancellationToken)
            where TCommand : MutationCommand;
}


