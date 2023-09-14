//using AutoFixture;
//using AutoFixture.Xunit2;
//using Carlton.Core.Components.Lab.Models.Validators.Commands;
//using Carlton.Core.Components.Lab.Test.Mocks;
//using FluentValidation.TestHelper;

//namespace Carlton.Core.Components.Lab.Test.ValidationTests.Commands;

//public class SelectMenuItemCommandValidatorTests
//{
//    private readonly IFixture _fixture;

//    public SelectMenuItemCommandValidatorTests()
//    {
//        _fixture = new Fixture();
//    }

//    [Fact]
//    public void ValidSelectMenuItemCommandCommand_ShouldPassValidation()
//    {
//        // Arrange
//        var validator = new SelectMenuItemCommandValidator();
//        var command = _fixture.Create<SelectMenuItemCommand>();

//        // Act
//        var result = validator.TestValidate(command);

//        // Assert
//        result.ShouldNotHaveAnyValidationErrors();
//    }

//    [Fact]
//    public void InvalidSelectMenuItemCommand_NullComponentState_ShouldFailValidation()
//    {
//        // Arrange
//        var validator = new SelectMenuItemCommandValidator();
//        var command = new SelectMenuItemCommand(null);

//        // Act
//        var result = validator.TestValidate(command);

//        // Assert
//        result.ShouldHaveValidationErrorFor(_ => _.ComponentState);
//    }

//    [Fact]
//    public void InvalidSelectMenuItemCommand_NullComponentStateDisplayName_ShouldFailValidation()
//    {
//        // Arrange
//        var componentState = new ComponentState(null, typeof(DummyComponent), _fixture.Create<ComponentParameters>());
//        var validator = new SelectMenuItemCommandValidator();
//        var command = new SelectMenuItemCommand(componentState);

//        // Act
//        var result = validator.TestValidate(command);

//        // Assert
//        result.ShouldHaveValidationErrorFor(_ => _.ComponentState.DisplayName);
//    }

//    [Fact]
//    public void InvalidSelectMenuItemCommand_EmptyComponentStateDisplayName_ShouldFailValidation()
//    {
//        // Arrange
//        var componentState = new ComponentState(string.Empty, typeof(DummyComponent), _fixture.Create<ComponentParameters>());
//        var validator = new SelectMenuItemCommandValidator();
//        var command = new SelectMenuItemCommand(componentState);

//        // Act
//        var result = validator.TestValidate(command);

//        // Assert
//        result.ShouldHaveValidationErrorFor(_ => _.ComponentState.DisplayName);
//    }

//    [Theory, AutoData]
//    public void InvalidSelectMenuItemCommand_NullComponentStateType_ShouldFailValidation(string displayName)
//    {
//        // Arrange
//        var componentState = new ComponentState(displayName, null, _fixture.Create<ComponentParameters>());
//        var validator = new SelectMenuItemCommandValidator();
//        var command = new SelectMenuItemCommand(componentState);

//        // Act
//        var result = validator.TestValidate(command);

//        // Assert
//        result.ShouldHaveValidationErrorFor(_ => _.ComponentState.Type);
//    }

//    [Theory, AutoData]
//    public void InvalidSelectMenuItemCommand_NullComponentStateComponentParameters_ShouldFailValidation(string displayName)
//    {
//        // Arrange
//        var componentState = new ComponentState(displayName, typeof(DummyComponent), null);
//        var validator = new SelectMenuItemCommandValidator();
//        var command = new SelectMenuItemCommand(componentState);

//        // Act
//        var result = validator.TestValidate(command);

//        // Assert
//        result.ShouldHaveValidationErrorFor(_ => _.ComponentState.ComponentParameters);
//    }
//}
