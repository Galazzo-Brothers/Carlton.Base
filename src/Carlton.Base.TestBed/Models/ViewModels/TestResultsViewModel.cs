namespace Carlton.Base.TestBed.Models;

public record TestResultsViewModel(IEnumerable<TestResult> TestResults, UnitTesting.TestResultsSummary TestResultsSummary);