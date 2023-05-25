namespace Carlton.Base.State;

public interface ICommandHandler<TCommand> 
{
    public Task<Unit> Handle(CommandRequest<TCommand> request, CancellationToken cancellationToken);
}
