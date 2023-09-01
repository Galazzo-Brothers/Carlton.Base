using AutoFixture;
using AutoFixture.Xunit2;
using Carlton.Core.Components.Lab.Models.Validators.ViewModels;
using FluentValidation.TestHelper;

namespace Carlton.Core.Components.Lab.Test.ValidationTests.ViewModels;

public class BreadCrumbsViewModelValidatorTests
{
    private readonly IFixture _fixture;

    public BreadCrumbsViewModelValidatorTests()
    {
        _fixture = new Fixture();
    }

    [Fact]
    public void BreadCrumbsViewModelValidator_ShouldPassValidation()
    {
        // Arrange
        var validator = new BreadCrumbsViewModelValidator();
        var vm = _fixture.Create<BreadCrumbsViewModel>();

        // Act
        var result = validator.TestValidate(vm);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory, AutoData]
    public void InvalidBreadCrumbsViewModelValidator_NullSelectedComponent_ShouldFailValidation(string selectedComponentState)
    {
        // Arrange
        var validator = new BreadCrumbsViewModelValidator();
        var vm = new BreadCrumbsViewModel(null, selectedComponentState);

        // Act
        var result = validator.TestValidate(vm);

        // Assert
        result.ShouldHaveValidationErrorFor(_ => _.SelectedComponent);
    }

    [Theory, AutoData]
    public void InvalidBreadCrumbsViewModelValidator_EmptySelectedComponent_ShouldFailValidation(string selectedComponentState)
    {
        // Arrange
        var validator = new BreadCrumbsViewModelValidator();
        var vm = new BreadCrumbsViewModel(string.Empty, selectedComponentState);

        // Act
        var result = validator.TestValidate(vm);

        // Assert
        result.ShouldHaveValidationErrorFor(_ => _.SelectedComponent);
    }

    [Theory, AutoData]
    public void InvalidBreadCrumbsViewModelValidator_NullSelectedComponentState_ShouldFailValidation(string selectedComponent)
    {
        // Arrange
        var validator = new BreadCrumbsViewModelValidator();
        var vm = new BreadCrumbsViewModel(selectedComponent, null);

        // Act
        var result = validator.TestValidate(vm);

        // Assert
        result.ShouldHaveValidationErrorFor(_ => _.SelectedComponentState);
    }

    [Theory, AutoData]
    public void InvalidBreadCrumbsViewModelValidator_EmptySelectedComponentState_ShouldFailValidation(string selectedComponent)
    {
        // Arrange
        var validator = new BreadCrumbsViewModelValidator();
        var vm = new BreadCrumbsViewModel(selectedComponent, null);

        // Act
        var result = validator.TestValidate(vm);

        // Assert
        result.ShouldHaveValidationErrorFor(_ => _.SelectedComponentState);
    }
}
