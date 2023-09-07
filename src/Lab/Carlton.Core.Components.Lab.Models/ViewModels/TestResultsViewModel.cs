using Carlton.Core.Utilities.UnitTesting;

namespace Carlton.Core.Components.Lab.Models.ViewModels;

public record TestResultsViewModel(IEnumerable<TestResult> TestResults);