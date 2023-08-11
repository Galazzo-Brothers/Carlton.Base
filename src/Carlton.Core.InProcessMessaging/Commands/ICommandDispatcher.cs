namespace Carlton.Core.InProcessMessaging.Commands;

public interface ICommandDispatcher
{
    public Task<TCommandResult> Dispatch<TCommand, TCommandResult>(TCommand command, CancellationToken cancellationToken);
    public Task<TCommandResult> Dispatch<TCommandResult>(object command, CancellationToken cancellationToken);
    public Task<Unit> Dispatch(object command, CancellationToken cancellationToken);
}