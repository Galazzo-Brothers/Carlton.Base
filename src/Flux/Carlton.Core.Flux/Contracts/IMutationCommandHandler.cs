namespace Carlton.Core.Flux.Contracts;

public interface IMutationCommandHandler<TState>
{
    public Task<Result<MutationCommandResult, MutationCommandFluxError>> Handle<TCommand>(MutationCommandContext<TCommand> context, CancellationToken cancellationToken);
}


