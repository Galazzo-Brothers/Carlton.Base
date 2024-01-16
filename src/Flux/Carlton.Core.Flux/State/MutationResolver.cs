namespace Carlton.Core.Flux.State;


internal class MutationResolver<TState>(IServiceProvider serviceProvider) : IMutationResolver<TState>
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public IFluxStateMutation<TState, TCommand> Resolve<TCommand>()
    {
        return _serviceProvider.GetService<IFluxStateMutation<TState, TCommand>>();
    }

    public IFluxStateMutation<TState> Resolve(Type commandType)
    {
        var type = typeof(IFluxStateMutation<,>).MakeGenericType(typeof(TState), commandType);
        return (IFluxStateMutation<TState>) _serviceProvider.GetService(type);
    }
}
