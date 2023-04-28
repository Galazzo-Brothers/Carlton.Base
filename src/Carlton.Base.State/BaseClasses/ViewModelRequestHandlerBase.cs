namespace Carlton.Base.State;

public abstract class ViewModelRequestHandlerBase<TViewModel> 
    : IRequestHandler<ViewModelRequest<TViewModel>, TViewModel>
{
    protected IViewModelStateFacade State { get; init; }

    protected ViewModelRequestHandlerBase(IViewModelStateFacade state) => State = state;

    public Task<TViewModel> Handle(ViewModelRequest<TViewModel> request, CancellationToken cancellationToken)
    {
        return Task.FromResult(State.GetViewModel<TViewModel>());
    }
}


