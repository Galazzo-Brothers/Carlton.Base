namespace Carlton.Core.Flux.Contracts;

public interface IFluxStateMutation<TState, TCommand> 
{
    public string StateEvent { get; }
    public TState Mutate(TState state, TCommand command);
}


internal static class IFluxStateMutationExtensions
{
    public static TState Mutate<TState, TCommand>(this IFluxStateMutation<TState, TCommand> mutation, TState state, object command)
    {
        return mutation.Mutate(state, (TCommand)command);
    }
}
