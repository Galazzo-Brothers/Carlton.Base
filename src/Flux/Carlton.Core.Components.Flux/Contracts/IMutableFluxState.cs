namespace Carlton.Core.Components.Flux.Contracts;


public interface IMutableFluxState<TState> : IFluxState<TState>
{
    public Task MutateState<TCommand>(TCommand command);
}
