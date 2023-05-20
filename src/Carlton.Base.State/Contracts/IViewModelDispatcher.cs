namespace Carlton.Base.State;

public interface IViewModelDispatcher
{
    public Task<TViewModel> Dispatch<TViewModel>(ViewModelRequest<TViewModel> request, CancellationToken cancellationToken);
}


