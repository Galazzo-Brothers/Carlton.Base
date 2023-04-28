namespace Carlton.Base.TestBed;

public class TestBedViewModelRequestHandler<TViewModel> : ViewModelRequestHandlerBase<TViewModel>
{
    public TestBedViewModelRequestHandler(IViewModelStateFacade state) : base(state)
    {
    }
}
