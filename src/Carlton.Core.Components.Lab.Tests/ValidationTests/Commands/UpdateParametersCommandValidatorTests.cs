using AutoFixture;
using Carlton.Core.Components.Lab.Models.Validators.Commands;
using FluentValidation.TestHelper;


namespace Carlton.Core.Components.Lab.Test.ValidationTests.Commands;

public class UpdateParametersCommandValidatorTests
{
    private readonly IFixture _fixture;

    public UpdateParametersCommandValidatorTests()
    {
        _fixture = new Fixture();
    }

    [Fact]
    public void ValidUpdateParametersCommandValidator_ShouldPassValidation()
    {
        // Arrange
        var validator = new UpdateParametersCommandValidator();
        var command = _fixture.Create<UpdateParametersCommand>();

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
