using Carlton.Core.Components.Flux.Contracts;

namespace Carlton.Core.Components.Flux.State;


public class MutationResolver<TState>
{
    private readonly IServiceProvider _serviceProvider;

    public MutationResolver(IServiceProvider serviceProvider)
        => _serviceProvider = serviceProvider;


    public IFluxStateMutation<TState, TCommand> Resolve<TCommand>()
    {
        return _serviceProvider.GetService<IFluxStateMutation<TState, TCommand>>();
    }
}
