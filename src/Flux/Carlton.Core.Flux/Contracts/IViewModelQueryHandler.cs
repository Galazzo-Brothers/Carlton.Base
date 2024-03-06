using Carlton.Core.Flux.Dispatchers.ViewModels;

namespace Carlton.Core.Flux.Contracts;

public interface IViewModelQueryHandler<TState> 
{
    public Task<Result<TViewModel, FluxError>> Handle<TViewModel>(ViewModelQueryContext<TViewModel> context, CancellationToken cancellationToken);
}
