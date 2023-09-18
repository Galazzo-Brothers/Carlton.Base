using AutoFixture.Xunit2;
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
        var validator = new SelectMenuItemCommandValidator();
        var command = new SelectMenuItemCommand(0, 0,
            new ComponentState("Display Name",
            new ComponentParameters(new object(), ParameterObjectType.ViewModel)));

        // Act
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void InvalidSelectMenuItemCommand_NegativeComponentIndex_ShouldFailValidation()
    {
        // Arrange
        var validator = new SelectMenuItemCommandValidator();
        var command = new SelectMenuItemCommand(-5, 0,
                  new ComponentState("Display Name",
                  new ComponentParameters(new object(), ParameterObjectType.ViewModel)));

        // Act
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(_ => _.ComponentIndex);
    }

    [Fact]
    public void InvalidSelectMenuItemCommand_NegativeComponentStateIndex_ShouldFailValidation()
    {
        // Arrange
        var validator = new SelectMenuItemCommandValidator();
        var command = new SelectMenuItemCommand(1, -5,
                  new ComponentState("Display Name",
                  new ComponentParameters(new object(), ParameterObjectType.ViewModel)));

        // Act
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(_ => _.ComponentStateIndex);
    }

    [Fact]
    public void InvalidSelectMenuItemCommand_NullComponentState_ShouldFailValidation()
    {
        // Arrange
        var validator = new SelectMenuItemCommandValidator();
        var command = new SelectMenuItemCommand(0, 0, null);

        // Act
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(_ => _.SelectedComponentState);
    }

    [Fact]
    public void InvalidSelectMenuItemCommand_NullComponentStateDisplayName_ShouldFailValidation()
    {
        // Arrange
        var componentParameters = new ComponentParameters(new object(), ParameterObjectType.ViewModel);
        var componentState = new ComponentState(null, componentParameters);
        var validator = new SelectMenuItemCommandValidator();
        var command = new SelectMenuItemCommand(0, 0, componentState);

        // Act
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(_ => _.SelectedComponentState.DisplayName);
    }

    [Fact]
    public void InvalidSelectMenuItemCommand_EmptyComponentStateDisplayName_ShouldFailValidation()
    {
        // Arrange
        var componentParameters = new ComponentParameters(new object(), ParameterObjectType.ViewModel);
        var componentState = new ComponentState(string.Empty, componentParameters);
        var validator = new SelectMenuItemCommandValidator();
        var command = new SelectMenuItemCommand(0, 0, componentState);

        // Act
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(_ => _.SelectedComponentState.DisplayName);
    }

    [Fact]
    public void InvalidSelectMenuItemCommand_NullComponentStateComponentParameters_ShouldFailValidation()
    {
        // Arrange
        var componentState = new ComponentState("Display Name", null);
        var validator = new SelectMenuItemCommandValidator();
        var command = new SelectMenuItemCommand(0, 0, componentState);

        // Act
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(_ => _.SelectedComponentState.ComponentParameters);
    }
}
