namespace Carlton.Base.TestBed;

public record TestResultsReport(IEnumerable<TestResult> TestResults, TestResultsSummary TestResultsSummary)
{
    public TestResultsReport() : this(new List<TestResult>(), new TestResultsSummary(0, 0, 0, 0))
    {
    }
}

