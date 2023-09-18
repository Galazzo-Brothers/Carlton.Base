using AutoFixture.Xunit2;
using Carlton.Core.Components.Lab.Models.Validators.Commands;
using FluentValidation.TestHelper;

namespace Carlton.Core.Components.Lab.Test.ValidationTests.Commands;

public class SelectMenuExpandedCommandValidatorTests
{
    [Theory, AutoData]
    public void ValidSelectMenuExpandedCommand_ShouldPassValidation(int index, bool isExpanded)
    {
        // Arrange
        var validator = new SelectMenuExpandedCommandValidator();
        var command = new SelectMenuExpandedCommand(index, isExpanded);

        // Act
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineAutoData(-5)]
    public void InvalidSelectMenuExpandedCommand_NegativeSelectedComponentIndex_ShouldFailValidation(int index, bool isExpanded)
    {
        // Arrange
        var validator = new SelectMenuExpandedCommandValidator();
        var command = new SelectMenuExpandedCommand(index, isExpanded);

        // Act
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(_ => _.SelectedComponentIndex);
    }
}
