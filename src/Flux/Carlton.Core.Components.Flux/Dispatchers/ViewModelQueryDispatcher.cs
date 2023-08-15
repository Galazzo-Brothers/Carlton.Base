namespace Carlton.Core.Components.Flux.Dispatchers;

public class ViewModelQueryDispatcher<TState> : IViewModelQueryDispatcher<TState>
{
    private readonly IServiceProvider _serviceProvider;

    public ViewModelQueryDispatcher(IServiceProvider serviceProvider)
        => _serviceProvider = serviceProvider;


    public async Task<TViewModel> Dispatch<TViewModel>(ViewModelQuery query, CancellationToken cancellationToken)
    {
        var handler = _serviceProvider.GetRequiredService<IViewModelQueryHandler<TState, TViewModel>>();
        return await handler.Handle(query, cancellationToken);
    }
}
