namespace Carlton.Core.Components.Flux.Contracts;

public interface IViewModelQueryHandler<TState, TViewModel> 
{
    public Task<TViewModel> Handle(ViewModelQuery query, CancellationToken cancellationToken);
}
