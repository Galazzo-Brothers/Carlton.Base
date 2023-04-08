namespace Carlton.Base.TestBedFramework;

public interface ITrxParser
{
    public IEnumerable<TestResultItemModel> ParseTrxTestResultsContent(string content);
    public TestResultsSummaryModel ParseTrxSummaryContent(string content);
}
