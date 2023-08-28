namespace Carlton.Core.Components.Flux.Contracts;

public interface IViewModelQueryDispatcher<TState>
{
    public Task<TViewModel> Dispatch<TViewModel>(object sender, ViewModelQuery query, CancellationToken cancellationToken);
}