namespace Carlton.Base.Infrastructure.UnitTesting;

public interface ITrxParser
{
    public IEnumerable<TestResult> ParseTrxTestResultsContent(string content);
    public TestResultsSummary ParseTrxSummaryContent(string content);
}
