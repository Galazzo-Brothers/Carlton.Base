using Carlton.Core.Components.Lab.Models.Validators.Commands;
using FluentValidation.TestHelper;

namespace Carlton.Core.Components.Lab.Test.ValidationTests.Commands;

public class SelectMenuExpandedCommandValidatorTests
{
    [Fact]
    public void ValidSelectMenuExpandedCommand_ShouldPassValidation()
    {
        // Arrange
        var validator = new SelectMenuExpandedCommandValidator();
        var command = new SelectMenuExpandedCommand(0, false);

        // Act
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void InvalidSelectMenuExpandedCommand_NegativeSelectedComponentIndex_ShouldFailValidation()
    {
        // Arrange
        var validator = new SelectMenuExpandedCommandValidator();
        var command = new SelectMenuExpandedCommand(-5, false);

        // Act
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(_ => _.SelectedComponentIndex);
    }
}
