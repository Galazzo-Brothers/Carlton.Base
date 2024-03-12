using Carlton.Core.Flux.Internals;
using Carlton.Core.Flux.Internals.Dispatchers.ViewModels;
namespace Carlton.Core.Flux.Contracts;

public interface IViewModelQueryDispatcher<TState>
{
	internal Task<Result<TViewModel, FluxError>> Dispatch<TViewModel>(object sender, ViewModelQueryContext<TViewModel> context, CancellationToken cancellationToken);
}

public static class IViewModelQueryDispatcherExtensions
{
	public static async Task<TViewModel> Dispatch<TState, TViewModel>(
		this IViewModelQueryDispatcher<TState> dispatcher, object sender, CancellationToken cancellation)
	{
		var context = new ViewModelQueryContext<TViewModel>();
		var result = await dispatcher.Dispatch(sender, context, cancellation);
		return result.GetViewModelResultOrThrow(context);
	}
}


