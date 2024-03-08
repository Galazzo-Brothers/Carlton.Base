using Carlton.Core.Flux.Dispatchers.ViewModels;
namespace Carlton.Core.Flux.Contracts;

public interface IViewModelQueryDispatcher<TState>
{
    public sealed async Task<Result<TViewModel, FluxError>> Dispatch<TViewModel>(object sender, CancellationToken cancellation)
        => await Dispatch(sender, new ViewModelQueryContext<TViewModel>(), cancellation);

    internal Task<Result<TViewModel, FluxError>> Dispatch<TViewModel>(object sender, ViewModelQueryContext<TViewModel> context, CancellationToken cancellationToken);
}


