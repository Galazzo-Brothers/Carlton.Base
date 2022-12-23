namespace Carlton.Base.TestBedFramework;

public abstract class TestBedRequestHandlerViewModelBase<TRequest, TViewModel> : ViewModelRequestHandlerBase<TRequest, TViewModel, TestBedState>
    where TRequest : IRequest<TViewModel>
{
    public TestBedRequestHandlerViewModelBase(TestBedState state) : base(state)
    {
    }
}
