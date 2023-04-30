//using System.Text.Json;

//namespace Carlton.Base.TestBed;

//public class TestResultsViewModelHttpBehavior : IPipelineBehavior<ViewModelRequest<TestResultsViewModel>, TestResultsViewModel>
//{
//    private readonly TestBedState _state;
//    private readonly HttpClient _client;
//    private readonly ITrxParser _trxParser;

//    public TestResultsViewModelHttpBehavior(TestBedState state, HttpClient client, ITrxParser trxParser)
//    {
//        _state = state;
//        _client = client;
//        _trxParser = trxParser;
//    }

//    public async Task<TestResultsViewModel> Handle(ViewModelRequest<TestResultsViewModel> request, RequestHandlerDelegate<TestResultsViewModel> next, CancellationToken cancellationToken)
//    {
//        var refreshRequired = !_state.ComponentTestResults.Any();

//        if(refreshRequired)
//        {

//            var typeNamespace = _state.SelectedComponentType.Namespace;
//            var results = await GetTestData(cancellationToken);
//            var groups = results.GroupBy(_ => $"{typeNamespace}.{_.TestName.Split('_').First()}");
//            SaveResults(groups);
//        }


//        return await next();
//    }

//    private async Task<IEnumerable<UnitTesting.TestResult>> GetTestData(CancellationToken cancellationToken)
//    {
//        var route = "/UnitTestResults/UnitTestResults.trx";
//        var xml = await _client.GetStringAsync(route, cancellationToken);
//        var results = _trxParser.ParseTrxTestResultsContent(xml);

//        //System.Console.WriteLine(JsonSerializer.Serialize(results));

//        return results;
//    }

//    private void SaveResults(IEnumerable<IGrouping<string, TestResult>> groups)
//    {
//        var results = new Dictionary<string, ComponentTestReport>();

//        foreach(var group in groups)
//        {
//            var total = group.Count();
//            var passed = group.Count(_ => _.TestResultOutcome == TestResultOutcomes.Passed);
//            var failed = group.Count(_ => _.TestResultOutcome == TestResultOutcomes.Failed);
//            var duration = group.Sum(_ => Math.Round(_.TestDuration));
//            var summary = new UnitTesting.TestResultsSummary(total, passed, failed, duration);

//            results.Add(group.Key, new ComponentTestReport
//                (
//                    group,
//                    summary
//                ));
//        }

//        System.Console.WriteLine(JsonSerializer.Serialize(results));

//        //  _state.InitComponentTestResultsReports(results);
//    }
//}
