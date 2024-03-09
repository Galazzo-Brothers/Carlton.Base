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


public class ViewModelQueryHandler<TState>(IFluxState<TState> _state, IViewModelMapper<TState> _mapper) : IViewModelQueryHandler<TState>
{
    public Task<Result<TViewModel, FluxError>> Handle<TViewModel>(ViewModelQueryContext<TViewModel> context, CancellationToken cancellationToken)
    {
        var vm = _mapper.Map<TViewModel>(_state.CurrentState);
        context.MarkAsSucceeded(vm);
        return Task.FromResult((Result<TViewModel, FluxError>)vm);
    }
}
