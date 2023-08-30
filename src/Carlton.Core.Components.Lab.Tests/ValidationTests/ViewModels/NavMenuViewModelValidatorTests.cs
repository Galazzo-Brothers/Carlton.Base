using Carlton.Core.Components.Lab.Models.Validators.ViewModels;
using Carlton.Core.Components.Lab.Test.Mocks;
using FluentValidation.TestHelper;

namespace Carlton.Core.Components.Lab.Test.ValidationTests.ViewModels;

public class NavMenuViewModelValidatorTests
{
    [Fact]
    public void NavMenuViewModelValidatorTests_ShouldPassValidation()
    {
        // Arrange
        var menuItems = new List<ComponentState>
        {
            new ComponentState("Component 1", typeof(DummyComponent),
                new ComponentParameters(new { Prop1 = "Testing", Prop2 = false}, ParameterObjectType.ParameterObject))
        };
        var validator = new NavMenuViewModelValidator();
        var vm = new NavMenuViewModel(menuItems, 1);

        // Act
        var result = validator.TestValidate(vm);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void InvalidNavMenuViewModelValidatorTests_NullComponentStates_ShouldFailValidation()
    {
        // Arrange
        var validator = new NavMenuViewModelValidator();
        var vm = new NavMenuViewModel(null);

        // Act
        var result = validator.TestValidate(vm);

        // Assert
        result.ShouldHaveValidationErrorFor(_ => _.MenuItems);
    }

    [Fact]
    public void InvalidNavMenuViewModelValidatorTests_EmptyComponentStates_ShouldFailValidation()
    {
        // Arrange
        var validator = new NavMenuViewModelValidator();
        var vm = new NavMenuViewModel(new List<ComponentState>());

        // Act
        var result = validator.TestValidate(vm);

        // Assert
        result.ShouldHaveValidationErrorFor(_ => _.MenuItems);
    }

    [Fact]
    public void InvalidNavMenuViewModelValidatorTests_NullComponentStatesDisplayName_ShouldFailValidation()
    {
        // Arrange
        var menuItems = new List<ComponentState>
        {
            new ComponentState(null, typeof(DummyComponent),
                new ComponentParameters(new { Prop1 = "Testing", Prop2 = false}, ParameterObjectType.ParameterObject))
        };
        var validator = new NavMenuViewModelValidator();
        var vm = new NavMenuViewModel(menuItems);

        // Act
        var result = validator.TestValidate(vm);

        // Assert
        result.ShouldHaveValidationErrorFor("MenuItems[0].DisplayName");
    }

    [Fact]
    public void InvalidNavMenuViewModelValidatorTests_EmptyComponentStatesDisplayName_ShouldFailValidation()
    {
        //Arrange
        var menuItems = new List<ComponentState>
       {
            new ComponentState(string.Empty, typeof(DummyComponent),
                new ComponentParameters(new { Prop1 = "Testing", Prop2 = false}, ParameterObjectType.ParameterObject))
       };
        var validator = new NavMenuViewModelValidator();
        var vm = new NavMenuViewModel(menuItems);

        //Act
        var result = validator.TestValidate(vm);

        //Assert
        result.ShouldHaveValidationErrorFor("MenuItems[0].DisplayName");
    }

    [Fact]
    public void InvalidNavMenuViewModelValidatorTests_NullComponentStatesType_ShouldFailValidation()
    {
        //Arrange
        var menuItems = new List<ComponentState>
       {
            new ComponentState("Test State", null,
                new ComponentParameters(new { Prop1 = "Testing", Prop2 = false}, ParameterObjectType.ParameterObject))
       };
        var validator = new NavMenuViewModelValidator();
        var vm = new NavMenuViewModel(menuItems);

        //Act
        var result = validator.TestValidate(vm);

        //Assert
        result.ShouldHaveValidationErrorFor("MenuItems[0].Type");
    }

    [Fact]
    public void InvalidNavMenuViewModelValidatorTests_NullComponentStatesComponentParameter_ShouldFailValidation()
    {
        //Arrange
        var menuItems = new List<ComponentState>
       {
            new ComponentState("Test State", typeof(DummyComponent), null)
       };
        var validator = new NavMenuViewModelValidator();
        var vm = new NavMenuViewModel(menuItems);

        //Act
        var result = validator.TestValidate(vm);

        //Assert
        result.ShouldHaveValidationErrorFor("MenuItems[0].ComponentParameters");
    }

    [Fact]
    public void InvalidNavMenuViewModelValidatorTests_NullComponentStatesComponentParameterObject_ShouldFailValidation()
    {
        //Arrange
        var menuItems = new List<ComponentState>
       {
            new ComponentState("Test State", typeof(DummyComponent),
                new ComponentParameters(null, ParameterObjectType.ParameterObject))
       };
        var validator = new NavMenuViewModelValidator();
        var vm = new NavMenuViewModel(menuItems);

        //Act
        var result = validator.TestValidate(vm);

        //Assert
        result.ShouldHaveValidationErrorFor("MenuItems[0].ComponentParameters.ParameterObj");
    }
}
