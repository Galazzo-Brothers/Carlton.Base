namespace Carlton.Core.Utilities.UnitTesting;
public record TestResultsSummary
{
    public int Total { get; init; }
    public int Passed { get; init; }
    public int Failed { get; init; }
    public double Duration { get; init; }

    public TestResultsSummary(int total, int passed, int failed, double duration)
    => (Total, Passed, Failed, Duration) = (total, passed, failed, Math.Round(duration / 1000, 2));
}

