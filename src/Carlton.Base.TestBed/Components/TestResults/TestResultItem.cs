namespace Carlton.Base.TestBedFramework;

public record TestResultItem(string TestName, TestResultStatus TestResultStatus, double TestDuration);

public enum TestResultStatus
{
    Passed,
    Failed,
    NotRun
}
