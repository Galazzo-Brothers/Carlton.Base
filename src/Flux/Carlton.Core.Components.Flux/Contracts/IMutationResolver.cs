namespace Carlton.Core.Components.Flux.Contracts;


public interface IMutationResolver<TState>
{
    public IFluxStateMutation<TState, TCommand> Resolve<TCommand>();
}
