namespace Carlton.Base.TestBed;

public sealed class TestResultsReportViewerViewModelRequestHandler : TestBedRequestHandlerViewModelBase<TestResultsReportViewerViewModelRequest, TestResultsViewModel>
{
    public TestResultsReportViewerViewModelRequestHandler(TestBedState state)
        : base(state)
    {
    }

    public override Task<TestResultsViewModel> Handle(TestResultsReportViewerViewModelRequest request, CancellationToken cancellationToken)
    {
        var x = new List<TestResult>
        {
            new TestResult("Test 1", TestResultOutcomes.Passed, 2.2),
            new TestResult("Test 2", TestResultOutcomes.Passed, 2.2),
            new TestResult("Test 3", TestResultOutcomes.Failed, 2.5)
        };

        var y = new TestResultsSummary(10, 8, 2, .05);

        return Task.FromResult(new TestResultsViewModel(x, y));
    }
}