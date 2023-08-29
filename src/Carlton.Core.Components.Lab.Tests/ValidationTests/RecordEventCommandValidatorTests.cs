using Carlton.Core.Components.Lab.Models.Validators.Commands;
using FluentValidation.TestHelper;

namespace Carlton.Core.Components.Lab.Test.ValidationTests;

public class RecordEventCommandValidatorTests
{
    [Fact]
    public void ValidRecordEventsCommand_ShouldPassValidation()
    {
        // Arrange
        var validator = new RecordEventCommandValidator();
        var command = new RecordEventCommand("Test Event", new { Param1 = "Test", Param2 = false});

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
        var command = new RecordEventCommand(null, new { Param1 = "Test", Param2 = false });

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
        var command = new RecordEventCommand(string.Empty, new { Param1 = "Test", Param2 = false });

        // Act
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(_ => _.RecordedEventName);
    }
}
