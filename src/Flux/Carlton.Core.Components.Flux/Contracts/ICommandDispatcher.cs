namespace Carlton.Core.Components.Flux;

public interface ICommandDispatcher
{
    public Task<Unit> Dispatch<TCommand>(CommandRequest<TCommand> request, CancellationToken cancellationToken);
}