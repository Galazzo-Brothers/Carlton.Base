using AutoFixture.Xunit2;
using Carlton.Core.Components.Lab.Models.Validators.Commands;
using FluentValidation.TestHelper;

namespace Carlton.Core.Components.Lab.Test.ValidationTests.Commands;

public class RecordEventCommandValidatorTests
{
    [Theory, AutoData]
    public void ValidRecordEventsCommand_ShouldPassValidation(string recordedEventName, object eventArgs)
    {
        // Arrange
        var validator = new RecordEventCommandValidator();
        var command = new RecordEventCommand(recordedEventName, eventArgs);

        // Act
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void InvalidRecordEventsCommand_NullEventName_ShouldFailValidation()
    {
        // Arrange
        var validator = new RecordEventCommandValidator();
        var command = new RecordEventCommand(null, new object());

        // Act
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(_ => _.RecordedEventName);
    }

    [Fact]
    public void InvalidRecordEventsCommand_EmptyEventName_ShouldFailValidation()
    {
        // Arrange
        var validator = new RecordEventCommandValidator();
        var command = new RecordEventCommand(string.Empty, new object());

        // Act
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(_ => _.RecordedEventName);
    }
}
