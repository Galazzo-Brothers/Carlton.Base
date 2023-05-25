namespace Carlton.Base.State;


public class ViewModelHandler<TViewModel> : IViewModelHandler<TViewModel>
{
    private readonly IViewModelStateFacade _state;

    public ViewModelHandler(IViewModelStateFacade state)
        => _state = state;

    public virtual Task<TViewModel> Handle(ViewModelRequest<TViewModel> request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_state.GetViewModel<TViewModel>());
    }
}


