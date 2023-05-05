namespace Carlton.Base.Infrastructure.UnitTesting;

public record TestResultsReport(IEnumerable<TestResult> TestResults, TestResultsSummary Summary);
