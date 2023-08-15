using Carlton.Core.InProcessMessaging.Commands;

namespace Carlton.Core.Components.Flux.Dispatchers;

public class CommandMutationDispatcher<TState> : ICommandMutationDispatcher<TState>
{
    private readonly IServiceProvider _serviceProvider;

    public CommandMutationDispatcher(IServiceProvider serviceProvider)
        => _serviceProvider = serviceProvider;


    public async Task<Unit> Dispatch<TCommand>(TCommand command, CancellationToken cancellationToken)
    {
        var handler = _serviceProvider.GetRequiredService<IMutationCommandHandler<TState, TCommand>>();
        return await handler.Handle(command, cancellationToken);
    }
}
