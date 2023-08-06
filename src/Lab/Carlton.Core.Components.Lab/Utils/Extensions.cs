namespace Carlton.Core.Components.Lab;

public static class TestBedUtils
{
    public static IDictionary<string, TestResultsReportModel> ParseXUnitTestResults(string testResults)
    {
        var parser = new XUnitTestResultsParser();
        return parser.ParseTestResultsByGroup(testResults, "Component");
    }

    public static IDictionary<string, TestResultsReportModel> ParseTrxTestResults(string testResults)
    {
        var parser = new TrxTestResultsParser();
        return parser.ParseTestResultsByGroup(testResults);
    }
}


