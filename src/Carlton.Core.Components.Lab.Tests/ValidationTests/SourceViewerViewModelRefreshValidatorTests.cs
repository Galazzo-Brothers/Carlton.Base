using Carlton.Core.Components.Lab.Models.Validators.Commands;
using FluentValidation.TestHelper;

namespace Carlton.Core.Components.Lab.Test.ValidationTests;

public class SourceViewerViewModelRefreshValidatorTests
{
    [Fact]
    public void ValidSourceViewerViewModelRefresh_ShouldPassValidation()
    {
        // Arrange
        var validator = new SourceViewerViewModelRefreshValidator();
        var vm = new SourceViewerViewModel("<div class='test'>Hello World!</div>");

        // Act
        var result = validator.TestValidate(vm);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void InvalidSourceViewerViewModelRefresh_NullSource_ShouldFailValidation()
    {
        // Arrange
        var validator = new SourceViewerViewModelRefreshValidator();
        var vm = new SourceViewerViewModel(null);

        // Act
        var result = validator.TestValidate(vm);

        // Assert
        result.ShouldHaveValidationErrorFor(_ => _.ComponentSource);
    }

    [Fact]
    public void InvalidSourceViewerViewModelRefresh_EmptySource_ShouldFailValidation()
    {
        // Arrange
        var validator = new SourceViewerViewModelRefreshValidator();
        var vm = new SourceViewerViewModel(string.Empty);

        // Act
        var result = validator.TestValidate(vm);

        // Assert
        result.ShouldHaveValidationErrorFor(_ => _.ComponentSource);
    }
}
