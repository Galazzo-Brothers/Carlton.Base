namespace Carlton.Core.Utilities.UnitTesting;

/// <summary>
/// Represents the result of a test.
/// </summary>
public record TestResult
{
    /// <summary>
    /// Gets or initializes the name of the test.
    /// </summary>
    public string TestName { get; init; }

    /// <summary>
    /// Gets or initializes the outcome of the test.
    /// </summary>
    public TestResultOutcomes TestResultOutcome { get; init; }

    /// <summary>
    /// Gets or initializes the duration of the test in milliseconds.
    /// </summary>
    public double TestDuration { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="TestResult"/> class.
    /// </summary>
    /// <param name="testName">The name of the test.</param>
    /// <param name="testOutcome">The outcome of the test.</param>
    /// <param name="testDuration">The duration of the test in seconds.</param>
    public TestResult(string testName, TestResultOutcomes testResultOutcome, double testDuration)
        => (TestName, TestResultOutcome, TestDuration) = (testName, testResultOutcome, Math.Round(testDuration * 1000, 2));    
}




