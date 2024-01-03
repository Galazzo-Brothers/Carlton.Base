namespace Carlton.Core.Flux.Contracts;


public interface IMutableFluxState<TState> : IFluxState<TState>
{
    public Task MutateState<TCommand>(TCommand command)
        where TCommand : MutationCommand;
}
