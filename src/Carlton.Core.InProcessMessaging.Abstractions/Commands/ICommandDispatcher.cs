namespace Carlton.Core.InProcessMessaging.Commands;

public interface ICommandDispatcher
{
    public Task<TResponse> Dispatch<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken);
}