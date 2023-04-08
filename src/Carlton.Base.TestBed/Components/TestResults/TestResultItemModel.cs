namespace Carlton.Base.TestBedFramework;

public record TestResultItemModel(string TestName, TestResult TestResultStatus, double TestDuration);

public record TestResultsSummaryModel(int Total, int Passed, int Failed, double Duration);

public enum TestResult
{
    Passed,
    Failed,
    NotRun
}
