namespace Carlton.Base.TestBed;

public class BreadCrumbsViewModelRequestHandler : TestBedViewModelRequestHandler<BreadCrumbsViewModel>
{
    public BreadCrumbsViewModelRequestHandler(IViewModelStateFacade state, ILogger<TestBedViewModelRequestHandler<BreadCrumbsViewModel>> logger) : base(state, logger)
    {
    }
}

