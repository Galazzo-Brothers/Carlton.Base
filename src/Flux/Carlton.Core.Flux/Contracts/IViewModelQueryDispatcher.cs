namespace Carlton.Core.Flux.Contracts;

public interface IViewModelQueryDispatcher<TState>
{
    internal Task<Result<TViewModel, ViewModelFluxError>> Dispatch<TViewModel>(object sender, ViewModelQueryContext<TViewModel> context, CancellationToken cancellationToken);
}


public static class ViewModelQueryDispatcherExtensions
{
    public static Task<Result<TViewModel, ViewModelFluxError>> Dispatch<TState, TViewModel>(this IViewModelQueryDispatcher<TState> dispatcher, object sender, CancellationToken cancellation)
    {
        return dispatcher.Dispatch(sender, new ViewModelQueryContext<TViewModel>(), cancellation);
    }
}