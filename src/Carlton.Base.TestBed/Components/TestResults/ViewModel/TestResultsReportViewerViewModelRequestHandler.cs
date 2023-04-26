namespace Carlton.Base.TestBed;

public sealed class TestResultsReportViewerViewModelRequestHandler : TestBedRequestHandlerBase, IRequestHandler<ViewModelRequest<TestResultsReportViewerViewModel>, TestResultsReportViewerViewModel>
{
    public TestResultsReportViewerViewModelRequestHandler(TestBedState state) : base(state)
    {
    }

    public async Task<TestResultsReportViewerViewModel> Handle(ViewModelRequest<TestResultsReportViewerViewModel> request, CancellationToken cancellationToken)
    {
        State.ComponentTestResultsReports.TryGetValue(State.SelectedComponentType.FullName, out var value);
        return await Task.FromResult(new TestResultsReportViewerViewModel(value));
    }
}