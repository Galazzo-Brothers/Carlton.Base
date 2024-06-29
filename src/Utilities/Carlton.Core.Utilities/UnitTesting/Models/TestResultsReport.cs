namespace Carlton.Core.Utilities.UnitTesting;

/// <summary>
/// Represents a report containing test results and a summary of those results.
/// </summary>
public record TestResultsReport
{
    // <summary>
    /// Gets or initializes the collection of test results.
    /// </summary>
    public IEnumerable<TestResult> TestResults { get; init; } = new List<TestResult>();

    /// <summary>
    /// Gets or initializes the summary of the test results.
    /// </summary>
    public TestResultsSummary Summary { get; init; } = new TestResultsSummary(0, 0, 0, 0);

    /// <summary>
    /// Initializes a new instance of the <see cref="TestResultsReport"/> class.
    /// </summary>
    public TestResultsReport()
    {
        //Empty Report
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TestResultsReport"/> class with the specified test results.
    /// </summary>
    /// <param name="testResults">The collection of test results.</param>
    public TestResultsReport(IEnumerable<TestResult> testResults)
    {
        TestResults = testResults;

        var totalCount = 0;
        var totalPassedCount = 0;
        var totalFailureCount = 0;
        var totalDuration = 0.0;

        TestResults.ToList().ForEach(_ => CalculateSummary(_, ref totalCount, ref totalPassedCount, ref totalFailureCount, ref totalDuration));
        Summary = new TestResultsSummary(totalCount, totalPassedCount, totalFailureCount, totalDuration);
    }

    private static void CalculateSummary(TestResult testResult, ref int totalCount, ref int totalPassedCount, ref int totalFailureCount, ref double totalDuration)
    {
        totalCount++;
        totalDuration += testResult.TestDuration;

        if(testResult.TestResultOutcome == TestResultOutcomes.Passed)
            totalPassedCount++;
        if(testResult.TestResultOutcome == TestResultOutcomes.Failed)
            totalFailureCount++;
    }
}
