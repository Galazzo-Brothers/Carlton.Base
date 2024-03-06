namespace Carlton.Core.Flux.Dispatchers.ViewModels;

public class ViewModelQueryDispatcher<TState>(IServiceProvider serviceProvider) : IViewModelQueryDispatcher<TState>
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public async Task<Result<TViewModel, FluxError>> Dispatch<TViewModel>(object sender, ViewModelQueryContext<TViewModel> context, CancellationToken cancellationToken)
    {
        var handler = _serviceProvider.GetRequiredService<IViewModelQueryHandler<TState>>();
        return await handler.Handle(context, cancellationToken);
    }
}
