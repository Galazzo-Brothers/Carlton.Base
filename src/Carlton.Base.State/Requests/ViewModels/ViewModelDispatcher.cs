namespace Carlton.Base.State;

public class ViewModelDispatcher : IViewModelDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public ViewModelDispatcher(IServiceProvider serviceProvider) 
        => _serviceProvider = serviceProvider;

    public async Task<TViewModel> Dispatch<TViewModel>(ViewModelRequest<TViewModel> request, CancellationToken cancellation)
    {
        var handler = _serviceProvider.GetRequiredService<IViewModelHandler<TViewModel>>();
        return await handler.Handle(request, cancellation);
    }
}
