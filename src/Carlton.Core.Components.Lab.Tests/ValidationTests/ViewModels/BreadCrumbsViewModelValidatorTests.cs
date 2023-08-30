using Carlton.Core.Components.Lab.Models.Validators.Commands;
using Carlton.Core.Components.Lab.Models.Validators.ViewModels;
using Carlton.Core.Components.Lab.Test.Mocks;
using FluentValidation.TestHelper;

namespace Carlton.Core.Components.Lab.Test.ValidationTests.ViewModels;

public class BreadCrumbsViewModelValidatorTests
{
    [Fact]
    public void BreadCrumbsViewModelValidator_ShouldPassValidation()
    {
        // Arrange
        var selectedComponent = nameof(DummyComponent);
        var selectedComponentState = "TestState";
        var validator = new BreadCrumbsViewModelValidator();
        var vm = new BreadCrumbsViewModel(selectedComponent, selectedComponentState);

        // Act
        var result = validator.TestValidate(vm);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void InvalidBreadCrumbsViewModelValidator_NullSelectedComponent_ShouldFailValidation()
    {
        // Arrange
        var selectedComponentState = "TestState";
        var validator = new BreadCrumbsViewModelValidator();
        var vm = new BreadCrumbsViewModel(null, selectedComponentState);

        // Act
        var result = validator.TestValidate(vm);

        // Assert
        result.ShouldHaveValidationErrorFor(_ => _.SelectedComponent);
    }

    [Fact]
    public void InvalidBreadCrumbsViewModelValidator_EmptySelectedComponent_ShouldFailValidation()
    {
        // Arrange
        var selectedComponentState = "TestState";
        var validator = new BreadCrumbsViewModelValidator();
        var vm = new BreadCrumbsViewModel(string.Empty, selectedComponentState);

        // Act
        var result = validator.TestValidate(vm);

        // Assert
        result.ShouldHaveValidationErrorFor(_ => _.SelectedComponent);
    }

    [Fact]
    public void InvalidBreadCrumbsViewModelValidator_NullSelectedComponentState_ShouldFailValidation()
    {
        // Arrange
        var selectedComponent = "TestComponent";
        var validator = new BreadCrumbsViewModelValidator();
        var vm = new BreadCrumbsViewModel(selectedComponent, null);

        // Act
        var result = validator.TestValidate(vm);

        // Assert
        result.ShouldHaveValidationErrorFor(_ => _.SelectedComponentState);
    }

    [Fact]
    public void InvalidBreadCrumbsViewModelValidator_EmptySelectedComponentState_ShouldFailValidation()
    {
        // Arrange
        var selectedComponent = "TestComponent";
        var validator = new BreadCrumbsViewModelValidator();
        var vm = new BreadCrumbsViewModel(selectedComponent, null);

        // Act
        var result = validator.TestValidate(vm);

        // Assert
        result.ShouldHaveValidationErrorFor(_ => _.SelectedComponentState);
    }
}
