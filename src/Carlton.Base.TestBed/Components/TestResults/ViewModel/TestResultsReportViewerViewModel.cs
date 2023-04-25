namespace Carlton.Base.TestBed;


[ObserveStateEvents<TestBedStateEvents>(TestBedStateEvents.ComponentStateSelected)]
//[RefreshViewModel("/UnitTestResults/UnitTestResults.trx")]
public record TestResultsReportViewerViewModel(TestResultsReport TestResultsReport);