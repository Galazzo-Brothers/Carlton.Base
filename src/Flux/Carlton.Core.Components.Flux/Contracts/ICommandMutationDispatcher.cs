using Carlton.Core.InProcessMessaging.Commands;

namespace Carlton.Core.Components.Flux.Contracts;

public interface ICommandMutationDispatcher<TState>
{
    public Task<Unit> Dispatch<TCommand>(TCommand command, CancellationToken cancellationToken);
}