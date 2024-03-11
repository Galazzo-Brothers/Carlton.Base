using Carlton.Core.Flux.Dispatchers;

namespace Carlton.Core.Flux.Internals.Contracts;

internal interface IViewModelQueryHandler<TState>
{
	public Task<Result<TViewModel, FluxError>> Handle<TViewModel>(ViewModelQueryContext<TViewModel> context, CancellationToken cancellationToken);
}
