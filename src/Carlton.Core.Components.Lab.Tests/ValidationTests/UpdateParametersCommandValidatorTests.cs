using Carlton.Core.Components.Lab.Models.Validators.Commands;
using FluentValidation.TestHelper;


namespace Carlton.Core.Components.Lab.Test.ValidationTests;

public class UpdateParametersCommandValidatorTests
{
    [Fact]
    public void ValidUpdateParametersCommandValidator_ShouldPassValidation()
    {
        // Arrange
        var validator = new UpdateParametersCommandValidator();
        var command = new UpdateParametersCommand(new { Prop1 = "test" });

        // Act
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void InvalidUpdateParametersCommandValidator_NullParameters_ShouldFailValidation()
    {
        // Arrange
        var validator = new UpdateParametersCommandValidator();
        var command = new UpdateParametersCommand(null);

        // Act
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(_ => _.Parameters);
    }
}
