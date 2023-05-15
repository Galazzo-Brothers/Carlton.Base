namespace Carlton.Base.TestBed;

public class TestBedViewModelRequestHandler<TViewModel> : ViewModelRequestHandlerBase<TViewModel>
{
    public TestBedViewModelRequestHandler(IViewModelStateFacade state,
        ILogger<TestBedViewModelRequestHandler<TViewModel>> logger) : base(state, logger)
    {
    }
}
