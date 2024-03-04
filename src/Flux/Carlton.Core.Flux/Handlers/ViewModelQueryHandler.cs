using Carlton.Core.Flux.Dispatchers.ViewModels;

namespace Carlton.Core.Flux.Handlers;

public class ViewModelQueryHandler<TState>(IFluxState<TState> _state, IViewModelMapper<TState> _mapper) : IViewModelQueryHandler<TState>
{
    public Task<Result<TViewModel, ViewModelFluxError>> Handle<TViewModel>(ViewModelQueryContext<TViewModel> context, CancellationToken cancellationToken)
    {
        var vm = _mapper.Map<TViewModel>(_state.CurrentState);
        context.MarkAsSucceeded(vm);
        return Task.FromResult((Result<TViewModel, ViewModelFluxError>)vm);
    }
}


