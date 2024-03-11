namespace Carlton.Core.Flux.Internals.Contracts;

internal interface IMutableFluxState<TState> : IFluxState<TState>
{
	internal Task<Result<TCommand, FluxError>> ApplyMutationCommand<TCommand>(TCommand command);
}

