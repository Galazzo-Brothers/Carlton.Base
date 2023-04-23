namespace Carlton.Base.State;

public abstract class RequestHandlerBase<TState> 
{
    protected TState State { get; init; }

    protected RequestHandlerBase(TState state) => State = state;
}
