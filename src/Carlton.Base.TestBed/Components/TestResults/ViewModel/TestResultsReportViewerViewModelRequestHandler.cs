namespace Carlton.Base.TestBed;

public sealed class TestResultsReportViewerViewModelRequestHandler : TestBedRequestHandlerBase, IRequestHandler<ViewModelRequest<TestResultsReportViewerViewModel>, TestResultsReportViewerViewModel>
{
    private readonly HttpClient _client;
    private readonly ITrxParser _trxParser;


    public TestResultsReportViewerViewModelRequestHandler(TestBedState state, HttpClient client, ITrxParser trxParser) : base(state)
    {
        _client = client;
        _trxParser = trxParser;
    }

    public async Task<TestResultsReportViewerViewModel> Handle(ViewModelRequest<TestResultsReportViewerViewModel> request, CancellationToken cancellationToken)
    {
        var selectedComponentType = State.SelectedComponentType;

        if(!State.ComponentTestResultsReports.Any())
        {
            var typeNamespace = selectedComponentType.Namespace;
            var results = await GetTestData(cancellationToken);
            var groups = results.GroupBy(_ => $"{typeNamespace}.{_.TestName.Split('_').First()}");
            SaveResults(groups);
        }

        var valueExists = State.ComponentTestResultsReports.TryGetValue(selectedComponentType.FullName, out var value);
        var result = valueExists ? value : new TestResultsReport();
        return new TestResultsReportViewerViewModel(result);
    }

    private async Task<IEnumerable<TestResult>> GetTestData(CancellationToken cancellationToken)
    {
        var route = "/UnitTestResults/UnitTestResults.trx";
        var xml = await _client.GetStringAsync(route, cancellationToken);
        var results = _trxParser.ParseTrxTestResultsContent(xml);
        return results;
    }

    private void SaveResults(IEnumerable<IGrouping<string, TestResult>> groups)
    {
        foreach(var group in groups)
        {
            var total = group.Count();
            var passed = group.Count(_ => _.TestResultOutcome == TestResultOutcomes.Passed);
            var failed = group.Count(_ => _.TestResultOutcome == TestResultOutcomes.Failed);
            var duration = group.Sum(_ => Math.Round(_.TestDuration));
            var summary = new TestResultsSummary(total, passed, failed, duration);

            State.ComponentTestResultsReports.Add(group.Key, new TestResultsReport
                (
                    group,
                    summary
                ));
        }
    }
}