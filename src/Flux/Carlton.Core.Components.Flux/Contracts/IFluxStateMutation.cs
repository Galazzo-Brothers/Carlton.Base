namespace Carlton.Core.Components.Flux.Contracts;

public interface IFluxStateMutation<TState, in TCommand> 
{
    public string StateEvent { get; }
    public TState Mutate(TState state, TCommand command);
}

