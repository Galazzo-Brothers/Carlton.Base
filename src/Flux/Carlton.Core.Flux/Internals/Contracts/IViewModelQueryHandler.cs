using Carlton.Core.Flux.Internals.Dispatchers.ViewModels;

namespace Carlton.Core.Flux.Internals.Contracts;

internal interface IViewModelQueryHandler<TState>
{
	public Task<Result<TViewModel, FluxError>> Handle<TViewModel>(ViewModelQueryContext<TViewModel> context, CancellationToken cancellationToken);
}
