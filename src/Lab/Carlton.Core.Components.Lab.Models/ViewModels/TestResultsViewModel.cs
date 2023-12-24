using Carlton.Core.Utilities.UnitTesting;

namespace Carlton.Core.Lab.Models.ViewModels;

public record TestResultsViewModel(IEnumerable<TestResult> TestResults);