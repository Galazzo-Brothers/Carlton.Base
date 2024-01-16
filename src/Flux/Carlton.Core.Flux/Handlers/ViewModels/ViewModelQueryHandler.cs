namespace Carlton.Core.Flux.Handlers.ViewModels;

public class ViewModelQueryHandler<TState>(IFluxState<TState> _state, IViewModelMapper<TState> _mapper) : IViewModelQueryHandler<TState>
{
    public Task<TViewModel> Handle<TViewModel>(ViewModelQueryContext<TViewModel> context, CancellationToken cancellationToken)
    {
        var vm = _mapper.Map<TViewModel>(_state.CurrentState);
        context.MarkAsSucceeded(vm);
        return Task.FromResult(vm);
    }
}


