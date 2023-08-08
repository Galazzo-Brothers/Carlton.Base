namespace Carlton.Core.Components.Flux.Mutations;

public class MutationProvider
{
    private readonly IServiceProvider _serviceProvider;

    public MutationProvider(IServiceProvider serviceProvider)
        => _serviceProvider = serviceProvider;

    public IStateMutation<> ResolveMutation<TStateEvent>()
    {

    }
}
