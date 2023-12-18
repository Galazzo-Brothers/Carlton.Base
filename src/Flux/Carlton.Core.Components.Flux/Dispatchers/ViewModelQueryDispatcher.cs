namespace Carlton.Core.Components.Flux.Dispatchers;

public class ViewModelQueryDispatcher<TState> : IViewModelQueryDispatcher<TState>
{
    private readonly IServiceProvider _serviceProvider;

    public ViewModelQueryDispatcher(IServiceProvider serviceProvider)
        => _serviceProvider = serviceProvider;


    public async Task<TViewModel> Dispatch<TViewModel>(object sender, ViewModelQuery query, CancellationToken cancellationToken)
    {
        var handler = _serviceProvider.GetRequiredService<IViewModelQueryHandler<TState>>();
        return await handler.Handle<TViewModel>(query, cancellationToken);
    }
}
