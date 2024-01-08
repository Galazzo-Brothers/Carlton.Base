namespace Carlton.Core.Flux.Dispatchers;

public class MutationCommandDispatcher<TState>(IServiceProvider serviceProvider) : IMutationCommandDispatcher<TState>
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public async Task Dispatch<TCommand>(object sender, MutationCommandContext<TCommand> context, CancellationToken cancellationToken)
    {
        var handler = _serviceProvider.GetRequiredService<IMutationCommandHandler<TState>>();
        await handler.Handle(context, cancellationToken);
    }
}
