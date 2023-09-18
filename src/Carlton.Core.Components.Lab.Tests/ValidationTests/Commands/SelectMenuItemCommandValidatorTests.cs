using AutoFixture.Xunit2;
using Carlton.Core.Components.Lab.Models.Validators.Commands;
using FluentValidation.TestHelper;

namespace Carlton.Core.Components.Lab.Test.ValidationTests.Commands;

public class SelectMenuItemCommandValidatorTests
{
    [Theory, AutoData]
    public void ValidSelectMenuItemCommandCommand_ShouldPassValidation(
        int componentIndex,
        int stateIndex,
        string displayName,
        object parameterObj)
    {
        // Arrange
        var validator = new SelectMenuItemCommandValidator();
        var command = new SelectMenuItemCommand(componentIndex, stateIndex,
            new ComponentState(displayName,
            new ComponentParameters(parameterObj, ParameterObjectType.ViewModel)));

        // Act
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineAutoData(-5, 0)]
    public void InvalidSelectMenuItemCommand_NegativeComponentIndex_ShouldFailValidation(
        int componentIndex,
        int stateIndex,
        string displayName,
        object parameterObj)
    {
        // Arrange
        var validator = new SelectMenuItemCommandValidator();
        var command = new SelectMenuItemCommand(componentIndex, stateIndex,
                  new ComponentState(displayName,
                  new ComponentParameters(parameterObj, ParameterObjectType.ViewModel)));

        // Act
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(_ => _.ComponentIndex);
    }

    [Theory]
    [InlineAutoData(0, -5)]
    public void InvalidSelectMenuItemCommand_NegativeComponentStateIndex_ShouldFailValidation(
        int componentIndex,
        int stateIndex,
        string displayName,
        object parameterObj)
    {
        // Arrange
        var validator = new SelectMenuItemCommandValidator();
        var command = new SelectMenuItemCommand(componentIndex, stateIndex,
                  new ComponentState(displayName,
                  new ComponentParameters(parameterObj, ParameterObjectType.ViewModel)));

        // Act
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(_ => _.ComponentStateIndex);
    }

    [Theory, AutoData]
    public void InvalidSelectMenuItemCommand_NullComponentState_ShouldFailValidation(
        int componentIndex, int stateIndex)
    {
        // Arrange
        var validator = new SelectMenuItemCommandValidator();
        var command = new SelectMenuItemCommand(componentIndex, stateIndex, null);

        // Act
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(_ => _.SelectedComponentState);
    }

    [Theory, AutoData]
    public void InvalidSelectMenuItemCommand_NullComponentStateDisplayName_ShouldFailValidation(
        int componentIndex, int stateIndex, object parameterObject)
    {
        // Arrange
        var componentParameters = new ComponentParameters(parameterObject, ParameterObjectType.ViewModel);
        var componentState = new ComponentState(null, componentParameters);
        var validator = new SelectMenuItemCommandValidator();
        var command = new SelectMenuItemCommand(componentIndex, stateIndex, componentState);

        // Act
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(_ => _.SelectedComponentState.DisplayName);
    }

    [Theory, AutoData]
    public void InvalidSelectMenuItemCommand_EmptyComponentStateDisplayName_ShouldFailValidation(
        int componentIndex, int stateIndex, object parameterObject)
    {
        // Arrange
        var componentParameters = new ComponentParameters(parameterObject, ParameterObjectType.ViewModel);
        var componentState = new ComponentState(string.Empty, componentParameters);
        var validator = new SelectMenuItemCommandValidator();
        var command = new SelectMenuItemCommand(componentIndex, stateIndex, componentState);

        // Act
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(_ => _.SelectedComponentState.DisplayName);
    }

    [Theory, AutoData]
    public void InvalidSelectMenuItemCommand_NullComponentStateComponentParameters_ShouldFailValidation(
        string displayName,
        int componentIndex,
        int stateIndex)
    {
        // Arrange
        var componentState = new ComponentState(displayName, null);
        var validator = new SelectMenuItemCommandValidator();
        var command = new SelectMenuItemCommand(componentIndex, stateIndex, componentState);

        // Act
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(_ => _.SelectedComponentState.ComponentParameters);
    }
}
