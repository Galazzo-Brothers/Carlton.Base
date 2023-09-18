using Carlton.Core.Components.Lab.Models.Validators.ViewModels;
using FluentValidation.TestHelper;

namespace Carlton.Core.Components.Lab.Test.ValidationTests.ViewModels;

public class EventConsoleViewModelValidatorTests
{
    [Fact]
    public void ValidEventConsoleViewModelValidator_ShouldPassValidation()
    {
        // Arrange
        var vm = new EventConsoleViewModel(new List<ComponentRecordedEvent>());
        var validator = new EventConsoleViewModelValidator();

        // Act
        var result = validator.TestValidate(vm);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void InvalidEventConsoleViewModelValidator_NullRecordedEvents_ShouldFailValidation()
    {
        // Arrange
        var validator = new EventConsoleViewModelValidator();
        var vm = new EventConsoleViewModel(null);

        // Act
        var result = validator.TestValidate(vm);

        // Assert
        result.ShouldHaveValidationErrorFor(_ => _.RecordedEvents);
    }
}
