namespace Carlton.Core.Utilities.UnitTesting;

/// <summary>
/// Defines methods for parsing test results.
/// </summary>
public interface ITestResultsParser
{
    /// <summary>
    /// Parses the test results from the provided content.
    /// </summary>
    /// <param name="content">The content containing the test results.</param>
    /// <returns>A <see cref="TestResultsReport"/> object representing the parsed test results.</returns>
    public TestResultsReport ParseTestResults(string content);
    /// <summary>
    /// Parses the test results from the provided content and groups them by the specified key.
    /// </summary>
    /// <param name="content">The content containing the test results.</param>
    /// <param name="groupKey">The key used for grouping the test results.</param>
    /// <returns>A dictionary containing test results reports grouped by the specified key.</returns>
    public IDictionary<string, TestResultsReport> ParseTestResultsByGroup(string content, string groupKey);
    /// <summary>
    /// Parses the test results from the provided content and groups them by a default key.
    /// </summary>
    /// <param name="content">The content containing the test results.</param>
    /// <returns>A dictionary containing test results reports grouped by a default key.</returns>
    public IDictionary<string, TestResultsReport> ParseTestResultsByGroup(string content);
}
