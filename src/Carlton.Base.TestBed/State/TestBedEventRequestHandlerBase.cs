namespace Carlton.Base.TestBedFramework;

public abstract class TestBedEventRequestHandlerBase<TRequest> : ComponentEventRequestHandlerBase<TRequest, TestBedState>
    where TRequest : IRequest<Unit>
{
    protected TestBedEventRequestHandlerBase(TestBedState state) : base(state)
    {
    }
}
