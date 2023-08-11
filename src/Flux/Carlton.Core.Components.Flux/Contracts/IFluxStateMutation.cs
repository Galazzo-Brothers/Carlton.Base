namespace Carlton.Core.Components.Flux.Contracts;

public interface IFluxStateMutation<TState, TCommand> 
{
    public string StateEvent { get; }
    public TState Mutate(TState state, TCommand command);
}

