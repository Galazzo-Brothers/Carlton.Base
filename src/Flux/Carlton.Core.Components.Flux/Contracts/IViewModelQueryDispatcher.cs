namespace Carlton.Core.Components.Flux.Contracts;

public interface IViewModelQueryDispatcher<TState>
{
    public Task<TViewModel> Dispatch<TViewModel>(ViewModelQuery query, CancellationToken cancellationToken);
}