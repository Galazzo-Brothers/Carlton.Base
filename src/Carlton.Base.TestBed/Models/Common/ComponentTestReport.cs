
namespace Carlton.Base.TestBed.Models;

public record ComponentTestReport(IEnumerable<TestResult> TestResults, UnitTesting.TestResultsSummary TestResultsSummary);
