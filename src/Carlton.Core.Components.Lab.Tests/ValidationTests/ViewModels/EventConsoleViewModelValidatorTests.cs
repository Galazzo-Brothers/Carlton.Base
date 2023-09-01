using AutoFixture;
using Carlton.Core.Components.Lab.Models.Validators.ViewModels;
using FluentValidation.TestHelper;

namespace Carlton.Core.Components.Lab.Test.ValidationTests.ViewModels;

public class EventConsoleViewModelValidatorTests
{
    private readonly IFixture _fixture;

    public EventConsoleViewModelValidatorTests()
    {
        _fixture = new Fixture();
    }

    [Fact]
    public void EventConsoleViewModelValidator_ShouldPassValidation()
    {
        // Arrange
        var vm = _fixture.Create<EventConsoleViewModel>();
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
