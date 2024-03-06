using Carlton.Core.Flux.Dispatchers.Mutations;
namespace Carlton.Core.Flux.Contracts;

public interface IMutationCommandHandler<TState>
{
    public Task<Result<MutationCommandResult, FluxError>> Handle<TCommand>(MutationCommandContext<TCommand> context, CancellationToken cancellationToken);
}


