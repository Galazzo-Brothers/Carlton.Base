namespace Carlton.Core.Components.Flux.Services;


public class MutationResolver<TState> : IMutationResolver<TState>
{
    private readonly IServiceProvider _serviceProvider;

    public MutationResolver(IServiceProvider serviceProvider)
        => _serviceProvider = serviceProvider;


    public IFluxStateMutation<TState, TCommand> Resolve<TCommand>()
    {
        return _serviceProvider.GetService<IFluxStateMutation<TState, TCommand>>();
    }
}
