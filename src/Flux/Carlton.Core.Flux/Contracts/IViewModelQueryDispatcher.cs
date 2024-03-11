using Carlton.Core.Flux.Dispatchers;

namespace Carlton.Core.Flux.Contracts;

public interface IViewModelQueryDispatcher<TState>
{
	internal Task<Result<TViewModel, FluxError>> Dispatch<TViewModel>(object sender, ViewModelQueryContext<TViewModel> context, CancellationToken cancellationToken);
}

public static class IViewModelQueryDispatcherExtensions
{
	public static async Task<Result<TViewModel, FluxError>> Dispatch<TState, TViewModel>(
		this IViewModelQueryDispatcher<TState> dispatcher, object sender, CancellationToken cancellation)
	   => await dispatcher.Dispatch(sender, new ViewModelQueryContext<TViewModel>(), cancellation);

	public static async Task<Result<TViewModel, FluxError>> Dispatch<TState, TViewModel, TContext>(
		this IViewModelQueryDispatcher<TState> dispatcher, object sender, TContext context, CancellationToken cancellation)
		where TContext : ViewModelQueryContext<TViewModel>
	   => await dispatcher.Dispatch(sender, context, cancellation);
}

public abstract class ViewModelQueryDispatcherMiddlewareBase<TState> : IViewModelQueryDispatcher<TState>
{
	public abstract Task<Result<TViewModel, FluxError>> Dispatch<TViewModel, FluxError>(object sender, ViewModelQueryContext<TViewModel> context, CancellationToken cancellationToken);

	async Task<Result<TViewModel, FluxError>> IViewModelQueryDispatcher<TState>.Dispatch<TViewModel>(object sender, ViewModelQueryContext<TViewModel> context, CancellationToken cancellationToken)
	{
		return await Dispatch<TViewModel, FluxError>(sender, context, cancellationToken);
	}
}

