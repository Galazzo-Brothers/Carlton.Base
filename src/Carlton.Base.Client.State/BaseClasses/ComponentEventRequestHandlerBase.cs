namespace Carlton.Base.State;

public abstract class ComponentEventRequestHandlerBase<TRequest, TState> : IRequestHandler<TRequest, Unit>
    where TRequest : IRequest<Unit>
{
    protected TState State { get; init; }
    
    protected ComponentEventRequestHandlerBase(TState state) => State = state;

    public abstract Task<Unit> Handle(TRequest request, CancellationToken cancellationToken);
}
