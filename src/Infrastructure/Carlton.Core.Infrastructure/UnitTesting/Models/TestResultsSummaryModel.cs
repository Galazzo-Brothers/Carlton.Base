namespace Carlton.Core.Infrastructure.UnitTesting;
public record TestResultsSummaryModel
{
    public int Total { get; init; }
    public int Passed { get; init; }
    public int Failed { get; init; }
    public double Duration { get; init; }

    public TestResultsSummaryModel(int total, int passed, int failed, double duration)
    => (Total, Passed, Failed, Duration) = (total, passed, failed, Math.Round(duration / 1000, 2));
}

