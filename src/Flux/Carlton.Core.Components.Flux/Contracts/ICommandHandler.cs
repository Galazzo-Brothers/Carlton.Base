namespace Carlton.Core.Components.Flux;

public interface ICommandHandler<TCommand> 
{
    public Task<Unit> Handle(CommandRequest<TCommand> request, CancellationToken cancellationToken);
}
