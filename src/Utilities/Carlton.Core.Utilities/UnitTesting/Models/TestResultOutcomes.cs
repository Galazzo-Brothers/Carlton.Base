namespace Carlton.Core.Utilities.UnitTesting;

/// <summary>
/// Represents the possible outcomes of a test result.
/// </summary>
public enum TestResultOutcomes
{
    /// <summary>
    /// The test passed successfully.
    /// </summary>
    Passed,

    /// <summary>
    /// The test failed.
    /// </summary>
    Failed,

    /// <summary>
    /// The test was not executed.
    /// </summary>
    NotRun
}
