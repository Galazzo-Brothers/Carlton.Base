namespace Carlton.Core.Flux.Contracts;

public interface IMutationCommandDispatcher<TState>
{
    internal Task Dispatch<TCommand>(object sender, MutationCommandContext<TCommand> context, CancellationToken cancellationToken);
}


public static class MutationCommandDispatcherExtensions
{
    public static async Task Dispatch<TState, TCommand>(this IMutationCommandDispatcher<TState> dispatcher, object sender, TCommand command, CancellationToken cancellation)
    {
        await dispatcher.Dispatch(sender, new MutationCommandContext<TCommand>(command), cancellation);
    }
}