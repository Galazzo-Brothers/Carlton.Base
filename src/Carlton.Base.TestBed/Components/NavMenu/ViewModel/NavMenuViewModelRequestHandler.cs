namespace Carlton.Base.TestBed;

public sealed class NavMenuViewModelRequestHandler : TestBedViewModelRequestHandler<NavMenuViewModel>
{
    public NavMenuViewModelRequestHandler(IViewModelStateFacade state, ILogger<TestBedViewModelRequestHandler<NavMenuViewModel>> logger) : base(state, logger)
    {
    }
}