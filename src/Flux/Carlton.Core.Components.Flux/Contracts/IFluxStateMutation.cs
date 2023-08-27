namespace Carlton.Core.Components.Flux.Contracts;

public interface IFluxStateMutation<TState, in TInput> 
{
    public bool IsRefreshMutation { get; }
    public string StateEvent { get; }
    public TState Mutate(TState state, TInput input);
}

