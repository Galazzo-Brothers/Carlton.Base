namespace Carlton.Base.TestBed;

public interface ITrxParser
{
    public IEnumerable<TestResultItemModel> ParseTrxTestResultsContent(string content);
    public TestResultsSummaryModel ParseTrxSummaryContent(string content);
}
