using Carlton.Core.Flux.Dispatchers.Mutations;
namespace Carlton.Core.Flux.Contracts;

public record MutationCommandResult();

public interface IMutationCommandDispatcher<TState>
{
    public sealed async Task<Result<MutationCommandResult, FluxError>> Dispatch<TCommand>(object sender, TCommand command, CancellationToken cancellation)
        => await Dispatch(sender, new MutationCommandContext<TCommand>(command), cancellation);
    
    internal Task<Result<MutationCommandResult, FluxError>> Dispatch<TCommand>(object sender, MutationCommandContext<TCommand> context, CancellationToken cancellationToken);
}





