namespace Carlton.Core.Utilities.UnitTesting;

public record TestResultModel
{
    public string TestName { get; init; }
    public TestResultOutcomes TestResultOutcome { get; init; }
    public double TestDuration { get; init; }

    public TestResultModel(string testName, TestResultOutcomes testResultOutcome, double testDuration)
     => (TestName, TestResultOutcome, TestDuration) = (testName, testResultOutcome, Math.Round(testDuration * 1000, 2));    
}




