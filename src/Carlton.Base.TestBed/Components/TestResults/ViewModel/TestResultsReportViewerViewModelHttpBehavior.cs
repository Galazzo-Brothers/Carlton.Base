using System.Text.Json;

namespace Carlton.Base.TestBed;

public class TestResultsReportViewerViewModelHttpBehavior : IPipelineBehavior<ViewModelRequest<TestResultsReportViewerViewModel>, TestResultsReportViewerViewModel>
{
    private readonly TestBedState _state;
    private readonly HttpClient _client;
    private readonly ITrxParser _trxParser;

    public TestResultsReportViewerViewModelHttpBehavior(TestBedState state, HttpClient client, ITrxParser trxParser)
    {
        _state = state;
        _client = client;
        _trxParser = trxParser;
    }

    public async Task<TestResultsReportViewerViewModel> Handle(ViewModelRequest<TestResultsReportViewerViewModel> request, RequestHandlerDelegate<TestResultsReportViewerViewModel> next, CancellationToken cancellationToken)
    {
        System.Console.WriteLine($"Middleware");

        var refreshRequired = !_state.ComponentTestResultsReports.Any();

        if(refreshRequired)
        {
            var typeNamespace = _state.SelectedComponentType.Namespace;
            var results = await GetTestData(cancellationToken);
            var groups = results.GroupBy(_ => $"{typeNamespace}.{_.TestName.Split('_').First()}");
            SaveResults(groups);
        }


        var x = await next();

        System.Console.WriteLine($"Middleware ViewModel: {JsonSerializer.Serialize(x)}");
        System.Console.WriteLine($"Middleware ViewModel: {x == null}");


        return x;
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

            _state.ComponentTestResultsReports.Add(group.Key, new TestResultsReport
                (
                    group,
                    summary
                ));
        }
    }
}
