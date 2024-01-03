namespace Carlton.Core.Flux.Contracts;


public interface IMutationResolver<TState>
{
    public IFluxStateMutation<TState, TCommand> Resolve<TCommand>()
        where TCommand: MutationCommand;
}
