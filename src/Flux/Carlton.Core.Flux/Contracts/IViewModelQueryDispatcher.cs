using Carlton.Core.Flux.Internals;
using Carlton.Core.Flux.Internals.Dispatchers.ViewModels;
namespace Carlton.Core.Flux.Contracts;

/// <summary>
/// Represents a dispatcher for view model queries in the Flux framework.
/// </summary>
/// <typeparam name="TState">The type of the state managed by the Flux framework.</typeparam>
public interface IViewModelQueryDispatcher<TState>
{
	internal Task<Result<TViewModel, FluxError>> Dispatch<TViewModel>(object sender, ViewModelQueryContext<TViewModel> context, CancellationToken cancellationToken);
}

/// <summary>
/// Provides extension methods for <see cref="IViewModelQueryDispatcher{TState}"/>.
/// </summary>
public static class IViewModelQueryDispatcherExtensions
{
	/// <summary>
	/// Dispatches a view model query using the specified dispatcher and returns the result view model.
	/// </summary>
	/// <typeparam name="TState">The type of the state managed by the Flux framework.</typeparam>
	/// <typeparam name="TViewModel">The type of the view model.</typeparam>
	/// <param name="dispatcher">The view model query dispatcher.</param>
	/// <param name="sender">The sender of the query.</param>
	/// <param name="cancellation">The cancellation token.</param>
	/// <returns>A task representing the asynchronous operation, containing the result view model.</returns>
	public static async Task<TViewModel> Dispatch<TState, TViewModel>(
		this IViewModelQueryDispatcher<TState> dispatcher, object sender, CancellationToken cancellation)
	{
		var context = new ViewModelQueryContext<TViewModel>();
		var result = await dispatcher.Dispatch(sender, context, cancellation);
		return result.GetViewModelResultOrThrow(context);
	}
}

/// <summary>
/// Base class for implementing middleware for view model query dispatching in the Flux framework.
/// </summary>
/// <typeparam name="TState">The type of the state managed by the Flux framework.</typeparam>
public abstract class ViewModelQueryDispatcherMiddlewareBase<TState>(IViewModelQueryDispatcher<TState> _decorated) : IViewModelQueryDispatcher<TState>
{
	/// <summary>
	/// Initializes a new instance of the <see cref="ViewModelQueryDispatcherMiddlewareBase{TState}"/> class with the specified decorated dispatcher.
	/// </summary>
	/// <param name="_decorated">The decorated view model query dispatcher.</param>
	public abstract Task<TViewModel> Dispatch<TViewModel>(object sender, CancellationToken cancellationToken, Func<Task<TViewModel>> next);

	async Task<Result<TViewModel, FluxError>> IViewModelQueryDispatcher<TState>.Dispatch<TViewModel>(object sender, ViewModelQueryContext<TViewModel> context, CancellationToken cancellationToken)
	{
		async Task<TViewModel> next() => (await _decorated.Dispatch(sender, context, cancellationToken))
			.GetViewModelResultOrThrow(context);
		return await Dispatch(sender, cancellationToken, next);
	}
}
