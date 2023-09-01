using AutoFixture;
using Carlton.Core.Components.Lab.Models.Validators.Commands;
using FluentValidation.TestHelper;

namespace Carlton.Core.Components.Lab.Test.ValidationTests.Commands;

public class RecordEventCommandValidatorTests
{
    private readonly IFixture _fixture;

    public RecordEventCommandValidatorTests()
    {
        _fixture = new Fixture();
    }

    [Fact]
    public void ValidRecordEventsCommand_ShouldPassValidation()
    {
        // Arrange
        var validator = new RecordEventCommandValidator();
        var command = _fixture.Create<RecordEventCommand>();

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
