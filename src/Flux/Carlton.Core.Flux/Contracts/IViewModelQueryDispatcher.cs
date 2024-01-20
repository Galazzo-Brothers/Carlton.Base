namespace Carlton.Core.Flux.Contracts;

public interface IViewModelQueryDispatcher<TState>
{
    internal Task<TViewModel> Dispatch<TViewModel>(object sender, ViewModelQueryContext<TViewModel> context, CancellationToken cancellationToken);
}


public static class ViewModelQueryDispatcherExtensions
{
    public static async Task<TViewModel> Dispatch<TState, TViewModel>(this IViewModelQueryDispatcher<TState> dispatcher, object sender, CancellationToken cancellation)
    {
        return await dispatcher.Dispatch(sender, new ViewModelQueryContext<TViewModel>(), cancellation);
    }
}