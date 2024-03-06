namespace Carlton.Core.Flux.Dispatchers.Mutations;

public class MutationCommandDispatcher<TState>(IServiceProvider serviceProvider) : IMutationCommandDispatcher<TState>
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public async Task<Result<MutationCommandResult, FluxError>> Dispatch<TCommand>(object sender, MutationCommandContext<TCommand> context, CancellationToken cancellationToken)
    {
        var handler = _serviceProvider.GetRequiredService<IMutationCommandHandler<TState>>();
        return await handler.Handle(context, cancellationToken);
    }
}
