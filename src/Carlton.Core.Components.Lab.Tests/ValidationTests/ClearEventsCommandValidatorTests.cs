using Carlton.Core.Components.Lab.Models.Validators.Commands;
using FluentValidation.TestHelper;

namespace Carlton.Core.Components.Lab.Test.ValidationTests;

public class ClearEventsCommandValidatorTests
{
    [Fact]
    public void ValidClearEventsCommand_ShouldPassValidation()
    {
        // Arrange
        var validator = new ClearEventsCommandValidator();
        var command = new ClearEventsCommand();

        // Act
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}
