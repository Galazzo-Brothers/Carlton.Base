namespace Carlton.Core.Flux.Contracts;


public interface IMutationResolver<TState>
{
    public IFluxStateMutation<TState, TCommand> Resolve<TCommand>();
    public IFluxStateMutation<TState> Resolve(Type commandType);
}
