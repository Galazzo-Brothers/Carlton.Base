namespace Carlton.Base.TestBed;

public sealed class TestResultsReportViewerViewModelRequestHandler : TestBedRequestHandlerBase, IRequestHandler<ViewModelRequest<TestResultsViewModel>, TestResultsViewModel>
{
    private readonly HttpClient _client;
    private readonly ITrxParser _trxParser;


    public TestResultsReportViewerViewModelRequestHandler(TestBedState state, HttpClient client, ITrxParser trxParser) : base(state)
    {
        _client = client;
        _trxParser = trxParser;
    }

    public async Task<TestResultsViewModel> Handle(ViewModelRequest<TestResultsViewModel> request, CancellationToken cancellationToken)
    {
        var route = "/UnitTestResults/UnitTestResults.trx";
        var xml = await _client.GetStringAsync(route , cancellationToken);

        var summary = _trxParser.ParseTrxSummaryContent(xml);
        var results = _trxParser.ParseTrxTestResultsContent(xml);

        return new TestResultsViewModel(results, summary);
    }
}