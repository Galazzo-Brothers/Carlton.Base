using Carlton.Core.Components.Lab.Models.Validators.Commands;
using Carlton.Core.Components.Lab.Test.Mocks;
using FluentValidation.TestHelper;

namespace Carlton.Core.Components.Lab.Test.ValidationTests.Commands;

public class SelectMenuItemCommandValidatorTests
{
    [Fact]
    public void ValidSelectMenuItemCommandCommand_ShouldPassValidation()
    {
        // Arrange
        var componentState = new ComponentState("Test Display Name", typeof(DummyComponent),
            new ComponentParameters(new { Param1 = "Testing" }, ParameterObjectType.ParameterObject));
        var validator = new SelectMenuItemCommandValidator();
        var command = new SelectMenuItemCommand(componentState);

        // Act
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void InvalidSelectMenuItemCommand_NullComponentState_ShouldFailValidation()
    {
        // Arrange
        var validator = new SelectMenuItemCommandValidator();
        var command = new SelectMenuItemCommand(null);

        // Act
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(_ => _.ComponentState);
    }

    [Fact]
    public void InvalidSelectMenuItemCommand_NullComponentStateDisplayName_ShouldFailValidation()
    {
        // Arrange
        var componentState = new ComponentState(null, typeof(DummyComponent),
            new ComponentParameters(new { Param1 = "Testing" }, ParameterObjectType.ParameterObject));
        var validator = new SelectMenuItemCommandValidator();
        var command = new SelectMenuItemCommand(componentState);

        // Act
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(_ => _.ComponentState.DisplayName);
    }

    [Fact]
    public void InvalidSelectMenuItemCommand_EmptyComponentStateDisplayName_ShouldFailValidation()
    {
        // Arrange
        var componentState = new ComponentState(string.Empty, typeof(DummyComponent),
            new ComponentParameters(new { Param1 = "Testing" }, ParameterObjectType.ParameterObject));
        var validator = new SelectMenuItemCommandValidator();
        var command = new SelectMenuItemCommand(componentState);

        // Act
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(_ => _.ComponentState.DisplayName);
    }

    [Fact]
    public void InvalidSelectMenuItemCommand_NullComponentStateType_ShouldFailValidation()
    {
        // Arrange
        var componentState = new ComponentState("Testing", null,
            new ComponentParameters(new { Param1 = "Testing" }, ParameterObjectType.ParameterObject));
        var validator = new SelectMenuItemCommandValidator();
        var command = new SelectMenuItemCommand(componentState);

        // Act
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(_ => _.ComponentState.Type);
    }

    [Fact]
    public void InvalidSelectMenuItemCommand_NullComponentStateComponentParameters_ShouldFailValidation()
    {
        // Arrange
        var componentState = new ComponentState("Testing", typeof(DummyComponent), null);
        var validator = new SelectMenuItemCommandValidator();
        var command = new SelectMenuItemCommand(componentState);

        // Act
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(_ => _.ComponentState.ComponentParameters);
    }
}
