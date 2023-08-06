namespace Carlton.Core.Components.Flux;

public interface IViewModelDispatcher
{
    public Task<TViewModel> Dispatch<TViewModel>(ViewModelRequest<TViewModel> request, CancellationToken cancellationToken);
}


