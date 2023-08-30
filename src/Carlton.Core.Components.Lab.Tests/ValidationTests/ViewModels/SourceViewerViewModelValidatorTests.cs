using Carlton.Core.Components.Lab.Models.Validators.ViewModels;
using FluentValidation.TestHelper;

namespace Carlton.Core.Components.Lab.Test.ValidationTests.ViewModels;

public class SourceViewerViewModelValidatorTests
{
    [Fact]
    public void ValidSourceViewerViewModel_ShouldPassValidation()
    {
        // Arrange
        var validator = new SourceViewerViewModelValidator();
        var vm = new SourceViewerViewModel("<div class='test'>Hello World!</div>");

        // Act
        var result = validator.TestValidate(vm);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void InvalidSourceViewerViewModel_NullSource_ShouldFailValidation()
    {
        // Arrange
        var validator = new SourceViewerViewModelValidator();
        var vm = new SourceViewerViewModel(null);

        // Act
        var result = validator.TestValidate(vm);

        // Assert
        result.ShouldHaveValidationErrorFor(_ => _.ComponentSource);
    }

    [Fact]
    public void InvalidSourceViewerViewModel_EmptySource_ShouldFailValidation()
    {
        // Arrange
        var validator = new SourceViewerViewModelValidator();
        var vm = new SourceViewerViewModel(string.Empty);

        // Act
        var result = validator.TestValidate(vm);

        // Assert
        result.ShouldHaveValidationErrorFor(_ => _.ComponentSource);
    }
}
