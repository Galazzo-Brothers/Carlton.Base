namespace Carlton.Base.TestBed;

public static class TestBedUtils
{
    public static IDictionary<string, TestResultsReport> ParseXUnitTestResults(string testResults)
    {
        var parser = new XUnitTestResultsParser();
        return parser.ParseTestResultsByGroup(testResults, "Component");
    }

    public static IDictionary<string, TestResultsReport> ParseTrxTestResults(string testResults)
    {
        var parser = new TrxTestResultsParser();
        return parser.ParseTestResultsByGroup(testResults);
    }
}


