namespace Carlton.Core.Components.Flux.Contracts;

public interface IViewModelQueryHandler<TState> 
{
    public Task<TViewModel> Handle<TViewModel>(ViewModelQuery query, CancellationToken cancellationToken);
}
