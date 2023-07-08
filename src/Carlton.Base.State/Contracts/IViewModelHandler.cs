namespace Carlton.Base.State;

public interface IViewModelHandler<TViewModel>
{
    public Task<TViewModel> Handle(ViewModelRequest<TViewModel> request, CancellationToken cancellationToken);
}





