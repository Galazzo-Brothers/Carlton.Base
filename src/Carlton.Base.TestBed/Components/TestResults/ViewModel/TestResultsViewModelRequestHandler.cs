namespace Carlton.Base.TestBed;

public sealed class TestResultsViewModelRequestHandler : TestBedViewModelRequestHandler<TestResultsViewModel>
{
    public TestResultsViewModelRequestHandler(IViewModelStateFacade state, ILogger<TestBedViewModelRequestHandler<TestResultsViewModel>> logger) : base(state, logger)
    {
    }
}