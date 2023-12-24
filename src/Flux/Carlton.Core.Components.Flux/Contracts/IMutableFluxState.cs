namespace Carlton.Core.Flux.Contracts;


public interface IMutableFluxState<TState> : IFluxState<TState>
{
    public Task MutateState<TInput>(TInput input);
}
