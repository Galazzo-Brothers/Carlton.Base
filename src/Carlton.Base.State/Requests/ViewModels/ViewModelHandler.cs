namespace Carlton.Base.State;


public class ViewModelHandler<TViewModel> : IViewModelHandler<TViewModel>
{
    private readonly IViewModelStateFacade _state;

    protected ViewModelHandler(IViewModelStateFacade state)
        => _state = state;

    public Task<TViewModel> Handle(ViewModelRequest<TViewModel> request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_state.GetViewModel<TViewModel>());
    }
}


