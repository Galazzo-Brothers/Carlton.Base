using Carlton.Core.Utilities.UnitTesting;

namespace Carlton.Core.Lab.Models.ViewModels;

public record TestResultsViewModel
{
    public IEnumerable<TestResult> TestResults = new List<TestResult>();
};