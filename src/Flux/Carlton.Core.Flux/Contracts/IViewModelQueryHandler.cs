namespace Carlton.Core.Flux.Contracts;

public interface IViewModelQueryHandler<TState> 
{
    public Task<TViewModel> Handle<TViewModel>(ViewModelQueryContext<TViewModel> context, CancellationToken cancellationToken);
}
