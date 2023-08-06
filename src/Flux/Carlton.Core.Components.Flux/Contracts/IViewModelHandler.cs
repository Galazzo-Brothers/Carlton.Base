namespace Carlton.Core.Components.Flux;

public interface IViewModelHandler<TViewModel>
{
    public Task<TViewModel> Handle(ViewModelRequest<TViewModel> request, CancellationToken cancellationToken);
}





