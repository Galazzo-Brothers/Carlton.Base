namespace Carlton.Core.Utilities.UnitTesting;

public record TestResultsReportModel
{
    public IEnumerable<TestResultModel> TestResults { get; init; } = new List<TestResultModel>();
    public TestResultsSummaryModel Summary { get; init; } = new TestResultsSummaryModel(0, 0, 0, 0);

    public TestResultsReportModel()
    {
        //Empty Report
    }

    public TestResultsReportModel(IEnumerable<TestResultModel> testResults)
    {
        TestResults = testResults;

        var totalCount = 0;
        var totalPassedCount = 0;
        var totalFailureCount = 0;
        var totalDuration = 0.0;

        TestResults.ToList().ForEach(_ => CalculateSummary(_, ref totalCount, ref totalPassedCount, ref totalFailureCount, ref totalDuration));
        Summary = new TestResultsSummaryModel(totalCount, totalPassedCount, totalFailureCount, totalDuration);
    }

    private static void CalculateSummary(TestResultModel testResult, ref int totalCount, ref int totalPassedCount, ref int totalFailureCount, ref double totalDuration)
    {
        totalCount++;
        totalDuration += testResult.TestDuration;

        if(testResult.TestResultOutcome == TestResultOutcomes.Passed)
            totalPassedCount++;
        if(testResult.TestResultOutcome == TestResultOutcomes.Failed)
            totalFailureCount++;
    }
}
