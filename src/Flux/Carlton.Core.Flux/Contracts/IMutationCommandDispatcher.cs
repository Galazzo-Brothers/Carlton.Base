using Carlton.Core.Flux.Dispatchers.Mutations;

namespace Carlton.Core.Flux.Contracts;

public record MutationCommandResult();

public interface IMutationCommandDispatcher<TState>
{
    internal Task<Result<MutationCommandResult, MutationCommandFluxError>> Dispatch<TCommand>(object sender, MutationCommandContext<TCommand> context, CancellationToken cancellationToken);
}


public static class MutationCommandDispatcherExtensions
{
    public static async Task<Result<MutationCommandResult, MutationCommandFluxError>> Dispatch<TState, TCommand>(this IMutationCommandDispatcher<TState> dispatcher, object sender, TCommand command, CancellationToken cancellation)
    {
        return await dispatcher.Dispatch(sender, new MutationCommandContext<TCommand>(command), cancellation);
    }
}