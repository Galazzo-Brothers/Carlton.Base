namespace Carlton.Core.Flux.Internals.Contracts;

internal interface IMutableFluxState<TState> : IFluxState<TState>
{
	internal Task<Result<string, FluxError>> ApplyMutationCommand<TCommand>(TCommand command);
}

