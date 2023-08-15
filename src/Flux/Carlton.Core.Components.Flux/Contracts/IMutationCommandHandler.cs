namespace Carlton.Core.Components.Flux.Contracts;

public interface IMutationCommandHandler<TState, TCommand> 
    where TCommand : MutationCommand
{
    public Task<Unit> Handle(TCommand command, CancellationToken cancellationToken);
}
