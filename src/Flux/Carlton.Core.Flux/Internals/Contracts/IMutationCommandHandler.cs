using Carlton.Core.Flux.Internals.Dispatchers.Mutations;

namespace Carlton.Core.Flux.Internals.Contracts;

internal interface IMutationCommandHandler<TState>
{
	public Task<Result<MutationCommandResult, FluxError>> Handle<TCommand>(MutationCommandContext<TCommand> context, CancellationToken cancellationToken);
}


