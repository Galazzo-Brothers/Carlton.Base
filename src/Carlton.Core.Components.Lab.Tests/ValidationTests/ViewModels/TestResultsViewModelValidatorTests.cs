using Carlton.Core.Components.Lab.Models.Validators.ViewModels;
using Carlton.Core.Utilities.UnitTesting;
using FluentValidation.TestHelper;

namespace Carlton.Core.Components.Lab.Test.ValidationTests.ViewModels;

public class TestResultsViewModelValidationTests
{
    [Fact]
    public void ValidTestResultsViewModelValidationTests_ShouldPassValidation()
    {
        // Arrange
        var tests = new List<TestResult>
        {
            new TestResult("Test 1", TestResultOutcomes.Passed, 1.1),
            new TestResult("Test 2", TestResultOutcomes.Failed, 2.2)
        };
        var validator = new TestResultsViewModelValidator();
        var vm = new TestResultsViewModel(tests);

        // Act
        var result = validator.TestValidate(vm);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void InvalidTestResultsViewModelValidationTests_NullTests_ShouldFailValidation()
    {
        // Arrange
        var validator = new TestResultsViewModelValidator();
        var vm = new TestResultsViewModel(null);

        // Act
        var result = validator.TestValidate(vm);

        //Assert
        result.ShouldHaveValidationErrorFor(_ => _.TestResults);
    }
}
