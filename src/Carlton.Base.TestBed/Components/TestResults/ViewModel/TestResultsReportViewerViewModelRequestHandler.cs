namespace Carlton.Base.TestBed;

public sealed class TestResultsReportViewerViewModelRequestHandler : TestBedRequestHandlerBase, IRequestHandler<ViewModelRequest<TestResultsReportViewerViewModel>, TestResultsReportViewerViewModel>
{
    public TestResultsReportViewerViewModelRequestHandler(TestBedState state) : base(state)
    {
    }

    public async Task<TestResultsReportViewerViewModel> Handle(ViewModelRequest<TestResultsReportViewerViewModel> request, CancellationToken cancellationToken)
    {
        var typeHasTests = State.ComponentTestResultsReports.TryGetValue(State.SelectedComponentType.FullName, out var value);
        var results = typeHasTests ? value : new TestResultsReport();
        return await Task.FromResult(new TestResultsReportViewerViewModel(results));
    }
}