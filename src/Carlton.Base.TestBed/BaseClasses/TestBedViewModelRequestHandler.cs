namespace Carlton.Base.TestBed;

public class TestBedViewModelRequestHandler<TViewModel> : ViewModelRequestHandlerBase<TViewModel, TestBedState>
{
    public TestBedViewModelRequestHandler(IViewModelStateFacade state) : base(state)
    {
    }
}
