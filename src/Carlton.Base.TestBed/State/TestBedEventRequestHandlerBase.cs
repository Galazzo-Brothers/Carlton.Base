namespace Carlton.Base.TestBed;

public abstract class TestBedEventRequestHandlerBase<TRequest> : ComponentEventRequestHandlerBase<TRequest, TestBedState>
    where TRequest : IComponentEventRequest
{
    protected TestBedEventRequestHandlerBase(TestBedState state) : base(state)
    {
    }
}
