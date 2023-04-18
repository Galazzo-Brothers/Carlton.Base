namespace Carlton.Base.TestBedFramework;

public abstract class TestBedEventRequestHandlerBase<TRequest> : ComponentEventRequestHandlerBase<TRequest, TestBedState>
    where TRequest : IComponentEventRequest
{
    protected TestBedEventRequestHandlerBase(TestBedState state) : base(state)
    {
    }
}
