namespace Carlton.Base.State;

public abstract class ComponentViewModelRequestHandlerBase<TRequest, TViewModel, TState> : IRequestHandler<TRequest, TViewModel>
    where TRequest : IRequest<TViewModel>
{
    protected TState State { get; private set; }

    protected ComponentViewModelRequestHandlerBase(TState state) => State = state;

    public abstract Task<TViewModel> Handle(TRequest request, CancellationToken cancellationToken);
}
