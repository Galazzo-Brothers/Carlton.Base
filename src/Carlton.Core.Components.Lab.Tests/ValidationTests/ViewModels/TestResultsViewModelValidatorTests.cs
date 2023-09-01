using AutoFixture;
using Carlton.Core.Components.Lab.Models.Validators.ViewModels;
using Carlton.Core.Utilities.UnitTesting;
using FluentValidation.TestHelper;

namespace Carlton.Core.Components.Lab.Test.ValidationTests.ViewModels;

public class TestResultsViewModelValidationTests
{
    private readonly IFixture _fixture;

    public TestResultsViewModelValidationTests()
    {
        _fixture = new Fixture();
    }

    [Fact]
    public void ValidTestResultsViewModelValidationTests_ShouldPassValidation()
    {
        // Arrange
        var tests = _fixture.CreateMany<TestResult>();
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
