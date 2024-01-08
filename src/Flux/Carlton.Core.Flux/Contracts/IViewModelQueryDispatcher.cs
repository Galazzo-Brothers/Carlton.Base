namespace Carlton.Core.Flux.Contracts;

public interface IViewModelQueryDispatcher<TState>
{
    public Task<TViewModel> Dispatch<TViewModel>(object sender, ViewModelQueryContext<TViewModel> context, CancellationToken cancellationToken);
}