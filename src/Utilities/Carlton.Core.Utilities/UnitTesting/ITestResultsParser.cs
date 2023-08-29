namespace Carlton.Core.Utilities.UnitTesting;

public interface ITestResultsParser
{
    public TestResultsReport ParseTestResults(string content);
    public IDictionary<string, TestResultsReport> ParseTestResultsByGroup(string content, string groupKey);
    public IDictionary<string, TestResultsReport> ParseTestResultsByGroup(string content);
}
