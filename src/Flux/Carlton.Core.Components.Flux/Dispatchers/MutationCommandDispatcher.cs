namespace Carlton.Core.Components.Flux.Dispatchers;

public class MutationCommandDispatcher<TState> : IMutationCommandDispatcher<TState>
{
    private readonly IServiceProvider _serviceProvider;

    public MutationCommandDispatcher(IServiceProvider serviceProvider)
        => _serviceProvider = serviceProvider;


    public async Task<Unit> Dispatch<TCommand>(object sender, TCommand command, CancellationToken cancellationToken)
        where TCommand : MutationCommand
    {
        var handler = _serviceProvider.GetRequiredService<IMutationCommandHandler<TState, TCommand>>();
        return await handler.Handle(command, cancellationToken);
    }
}
