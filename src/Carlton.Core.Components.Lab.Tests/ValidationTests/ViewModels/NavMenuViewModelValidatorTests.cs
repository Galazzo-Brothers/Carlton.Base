using AutoFixture.Xunit2;
using Carlton.Core.Components.Lab.Models.Common;
using Carlton.Core.Components.Lab.Models.Validators.ViewModels;
using Carlton.Core.Components.Lab.Test.Mocks;
using FluentValidation.TestHelper;

namespace Carlton.Core.Components.Lab.Test.ValidationTests.ViewModels;

public class NavMenuViewModelValidatorTests
{
    [Theory, AutoData]
    public void ValidNavMenuViewModelValidator_ShouldPassValidation(IEnumerable<ComponentAvailableStates> availableStates)
    {
        // Arrange
        var vm = new NavMenuViewModel(availableStates, 0, 0);
        var validator = new NavMenuViewModelValidator();

        // Act
        var result = validator.TestValidate(vm);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory, AutoData]
    public void InvalidNavMenuViewModelValidator_NegativeSelectedComponentIndex_ShouldPassValidation(IEnumerable<ComponentAvailableStates> availableStates)
    {
        // Arrange
        var vm = new NavMenuViewModel(availableStates, -5, 0);
        var validator = new NavMenuViewModelValidator();

        // Act
        var result = validator.TestValidate(vm);

        // Assert
        result.ShouldHaveValidationErrorFor(_ => _.SelectedComponentIndex);
    }

    [Theory, AutoData]
    public void InvalidNavMenuViewModelValidator_NegativeSelectedStateIndex_ShouldPassValidation(IEnumerable<ComponentAvailableStates> availableStates)
    {
        // Arrange
        var vm = new NavMenuViewModel(availableStates, 0, -5);
        var validator = new NavMenuViewModelValidator();

        // Act
        var result = validator.TestValidate(vm);

        // Assert
        result.ShouldHaveValidationErrorFor(_ => _.SelectedStateIndex);
    }

    [Fact]
    public void InvalidNavMenuViewModelValidatorTests_NullComponentStates_ShouldFailValidation()
    {
        // Arrange
        var validator = new NavMenuViewModelValidator();
        var vm = new NavMenuViewModel(null, 0, 0);

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
        var vm = new NavMenuViewModel(new List<ComponentAvailableStates>(), 0, 0);

        // Act
        var result = validator.TestValidate(vm);

        // Assert
        result.ShouldHaveValidationErrorFor(_ => _.MenuItems);
    }

    [Fact]
    public void InvalidNavMenuViewModelValidatorTests_NullComponentStatesDisplayName_ShouldFailValidation()
    {
        // Arrange
        var menuItems = new List<ComponentAvailableStates>
        {
                new ComponentAvailableStates(typeof(DummyComponent), true,
                    new List<ComponentState>
                    {
                        new ComponentState(null, typeof(DummyComponent),
                            new ComponentParameters(new object(), ParameterObjectType.ParameterObject))
                    })
        };
        var validator = new NavMenuViewModelValidator();
        var vm = new NavMenuViewModel(menuItems, 0, 0);

        // Act
        var result = validator.TestValidate(vm);

        // Assert
        result.ShouldHaveValidationErrorFor("MenuItems[0].ComponentStates[0].DisplayName");
    }

    [Fact]
    public void InvalidNavMenuViewModelValidatorTests_EmptyComponentStatesDisplayName_ShouldFailValidation()
    {
        // Arrange
        var menuItems = new List<ComponentAvailableStates>
        {
                new ComponentAvailableStates(typeof(DummyComponent), true,
                    new List<ComponentState>
                    {
                        new ComponentState(string.Empty, typeof(DummyComponent),
                            new ComponentParameters(new object(), ParameterObjectType.ParameterObject))
                    })
        };
        var validator = new NavMenuViewModelValidator();
        var vm = new NavMenuViewModel(menuItems, 0, 0);

        // Act
        var result = validator.TestValidate(vm);

        // Assert
        result.ShouldHaveValidationErrorFor("MenuItems[0].ComponentStates[0].DisplayName");
    }

    [Fact]
    public void InvalidNavMenuViewModelValidatorTests_NullComponentStatesType_ShouldFailValidation()
    {
        // Arrange
        var menuItems = new List<ComponentAvailableStates>
        {
                new ComponentAvailableStates(typeof(DummyComponent), true,
                    new List<ComponentState>
                    {
                        new ComponentState("Display Name", null,
                            new ComponentParameters(new object(), ParameterObjectType.ParameterObject))
                    })
        };
        var validator = new NavMenuViewModelValidator();
        var vm = new NavMenuViewModel(menuItems, 0, 0);

        // Act
        var result = validator.TestValidate(vm);

        // Assert
        result.ShouldHaveValidationErrorFor("MenuItems[0].ComponentStates[0].Type");
    }

    [Fact]
    public void InvalidNavMenuViewModelValidatorTests_NullComponentStatesComponentParameter_ShouldFailValidation()
    {
        // Arrange
        var menuItems = new List<ComponentAvailableStates>
        {
                new ComponentAvailableStates(typeof(DummyComponent), true,
                    new List<ComponentState>
                    {
                        new ComponentState("Display Name", typeof(DummyComponent), null)
                    })
        };
        var validator = new NavMenuViewModelValidator();
        var vm = new NavMenuViewModel(menuItems, 0, 0);

        // Act
        var result = validator.TestValidate(vm);

        // Assert
        result.ShouldHaveValidationErrorFor("MenuItems[0].ComponentStates[0].ComponentParameters");
    }
}
