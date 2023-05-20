namespace Carlton.Base.State;

public interface ICommandHandler<TCommand> 
    where TCommand : ICommand
{
    public Task<Unit> Handle(CommandRequest<TCommand> request, CancellationToken cancellationToken);
}
