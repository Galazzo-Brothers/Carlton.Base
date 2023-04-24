namespace Carlton.Base.TestBed;


[ObserveStateEvents<TestBedStateEvents>(TestBedStateEvents.ComponentStateSelected)]
//[RefreshViewModel("/UnitTestResults/UnitTestResults.trx")]
public record TestResultsViewModel(IEnumerable<TestResult> TestResults, TestResultsSummary TestResultsSummary);