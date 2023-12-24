using AutoFixture.Xunit2;
using Carlton.Core.Components.Lab.Models.Validators.ViewModels;
using Carlton.Core.Utilities.UnitTesting;
using FluentValidation.TestHelper;

namespace Carlton.Core.Components.Lab.Test.ValidationTests.ViewModels;

public class TestResultsViewModelValidationTests
{
    [Theory, AutoData]
    public void ValidTestResultsViewModelValidationTests_ShouldPassValidation(IEnumerable<TestResult> testResults)
    {
        // Arrange
        var validator = new TestResultsViewModelValidator();
        var vm = new TestResultsViewModel(testResults);

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
