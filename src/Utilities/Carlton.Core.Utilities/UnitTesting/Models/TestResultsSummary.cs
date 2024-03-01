namespace Carlton.Core.Utilities.UnitTesting;

/// <summary>
/// Represents a summary of test results.
/// </summary>
public record TestResultsSummary
{
    /// <summary>
    /// Gets or initializes the total number of test results.
    /// </summary>
    public int Total { get; init; }

    /// <summary>
    /// Gets or initializes the number of passed test results.
    /// </summary>
    public int Passed { get; init; }

    /// <summary>
    /// Gets or initializes the number of failed test results.
    /// </summary>
    public int Failed { get; init; }

    /// <summary>
    /// Gets or initializes the total duration of the test results.
    /// </summary>
    public double Duration { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="TestResultsSummary"/> class.
    /// </summary>
    /// <param name="total">The total number of test results.</param>
    /// <param name="passed">The number of passed test results.</param>
    /// <param name="failed">The number of failed test results.</param>
    /// <param name="duration">The total duration of the test results.</param>
    public TestResultsSummary(int total, int passed, int failed, double duration)
        => (Total, Passed, Failed, Duration) = (total, passed, failed, Math.Round(duration / 1000, 2));
}

