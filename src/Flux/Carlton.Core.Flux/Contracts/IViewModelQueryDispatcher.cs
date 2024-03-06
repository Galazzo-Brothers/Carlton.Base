using Carlton.Core.Flux.Dispatchers.ViewModels;

namespace Carlton.Core.Flux.Contracts;

public interface IViewModelQueryDispatcher<TState>
{
    internal Task<Result<TViewModel, FluxError>> Dispatch<TViewModel>(object sender, ViewModelQueryContext<TViewModel> context, CancellationToken cancellationToken);
}


public static class ViewModelQueryDispatcherExtensions
{
    public static Task<Result<TViewModel, FluxError>> Dispatch<TState, TViewModel>(this IViewModelQueryDispatcher<TState> dispatcher, object sender, CancellationToken cancellation)
    {
        return dispatcher.Dispatch(sender, new ViewModelQueryContext<TViewModel>(), cancellation);
    }
}