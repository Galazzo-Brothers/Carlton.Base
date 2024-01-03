namespace Carlton.Core.Flux.State;


public class MutationResolver<TState>(IServiceProvider serviceProvider) : IMutationResolver<TState>
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public IFluxStateMutation<TState, TCommand> Resolve<TCommand>()
        where TCommand : MutationCommand
    {
        return _serviceProvider.GetService<IFluxStateMutation<TState, TCommand>>();
    }
}
