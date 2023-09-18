using AutoFixture.Xunit2;
using Carlton.Core.Components.Lab.Models.Common;
using Carlton.Core.Components.Lab.Models.Validators.ViewModels;
using Carlton.Core.Components.Lab.Test.Mocks;
using FluentValidation.TestHelper;

namespace Carlton.Core.Components.Lab.Test.ValidationTests.ViewModels;

public class NavMenuViewModelValidatorTests
{
    [Theory, AutoData]
    public void ValidNavMenuViewModelValidator_ShouldPassValidation(
        int selectedComponentIndex,
        int selectedStateIndex,
        IEnumerable<ComponentAvailableStates> availableStates)
    {
        // Arrange
        var vm = new NavMenuViewModel(availableStates, selectedComponentIndex, selectedStateIndex);
        var validator = new NavMenuViewModelValidator();

        // Act
        var result = validator.TestValidate(vm);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineAutoData(-5, 2)]
    public void InvalidNavMenuViewModelValidator_NegativeSelectedComponentIndex_ShouldPassValidation(
        int selectedComponentIndex,
        int selectedStateIndex,
        IEnumerable<ComponentAvailableStates> availableStates)
    {
        // Arrange
        var vm = new NavMenuViewModel(availableStates, selectedComponentIndex, selectedStateIndex);
        var validator = new NavMenuViewModelValidator();

        // Act
        var result = validator.TestValidate(vm);

        // Assert
        result.ShouldHaveValidationErrorFor(_ => _.SelectedComponentIndex);
    }

    [Theory]
    [InlineAutoData(2, -5)]
    public void InvalidNavMenuViewModelValidator_NegativeSelectedStateIndex_ShouldPassValidation(
        int selectedComponentIndex,
        int selectedStateIndex,
        IEnumerable<ComponentAvailableStates> availableStates)
    {
        // Arrange
        var vm = new NavMenuViewModel(availableStates, selectedComponentIndex, selectedStateIndex);
        var validator = new NavMenuViewModelValidator();

        // Act
        var result = validator.TestValidate(vm);

        // Assert
        result.ShouldHaveValidationErrorFor(_ => _.SelectedStateIndex);
    }

    [Theory, AutoData]
    public void InvalidNavMenuViewModelValidatorTests_NullComponentStates_ShouldFailValidation(
        int selectedComponentIndex,
        int selectedStateIndex)
    {
        // Arrange
        var validator = new NavMenuViewModelValidator();
        var vm = new NavMenuViewModel(null, selectedComponentIndex, selectedStateIndex);

        // Act
        var result = validator.TestValidate(vm);

        // Assert
        result.ShouldHaveValidationErrorFor(_ => _.MenuItems);
    }

    [Theory, AutoData]
    public void InvalidNavMenuViewModelValidatorTests_EmptyComponentStates_ShouldFailValidation(
        int selectedComponentIndex,
        int selectedStateIndex)
    {
        // Arrange
        var validator = new NavMenuViewModelValidator();
        var vm = new NavMenuViewModel(new List<ComponentAvailableStates>(), selectedComponentIndex, selectedStateIndex);

        // Act
        var result = validator.TestValidate(vm);

        // Assert
        result.ShouldHaveValidationErrorFor(_ => _.MenuItems);
    }

    [Theory, AutoData]
    public void InvalidNavMenuViewModelValidatorTests_NullComponentStatesDisplayName_ShouldFailValidation(
        bool isExpanded,
        int selectedComponentIndex,
        int selectedStateIndex)
    {
        // Arrange
        var menuItems = new List<ComponentAvailableStates>
        {
                new ComponentAvailableStates(typeof(DummyComponent), isExpanded,
                    new List<ComponentState>
                    {
                        new ComponentState(null,
                            new ComponentParameters(new object(), ParameterObjectType.ParameterObject))
                    })
        };
        var validator = new NavMenuViewModelValidator();
        var vm = new NavMenuViewModel(menuItems, selectedComponentIndex, selectedStateIndex);

        // Act
        var result = validator.TestValidate(vm);

        // Assert
        result.ShouldHaveValidationErrorFor("MenuItems[0].ComponentStates[0].DisplayName");
    }

    [Theory, AutoData]
    public void InvalidNavMenuViewModelValidatorTests_EmptyComponentStatesDisplayName_ShouldFailValidation(
        bool isExpanded,
        int selectedComponentIndex,
        int selectedStateIndex)
    {
        // Arrange
        var menuItems = new List<ComponentAvailableStates>
        {
                new ComponentAvailableStates(typeof(DummyComponent), isExpanded,
                    new List<ComponentState>
                    {
                        new ComponentState(string.Empty,
                            new ComponentParameters(new object(), ParameterObjectType.ParameterObject))
                    })
        };
        var validator = new NavMenuViewModelValidator();
        var vm = new NavMenuViewModel(menuItems, selectedComponentIndex, selectedStateIndex);

        // Act
        var result = validator.TestValidate(vm);

        // Assert
        result.ShouldHaveValidationErrorFor("MenuItems[0].ComponentStates[0].DisplayName");
    }

    [Theory, AutoData]
    public void InvalidNavMenuViewModelValidatorTests_NullComponentStatesComponentParameter_ShouldFailValidation(
        bool isExpanded,
        int selectedComponentIndex,
        int selectedStateIndex,
        string displayName)
    {
        // Arrange
        var menuItems = new List<ComponentAvailableStates>
        {
                new ComponentAvailableStates(typeof(DummyComponent), isExpanded,
                    new List<ComponentState>
                    {
                        new ComponentState(displayName, null)
                    })
        };
        var validator = new NavMenuViewModelValidator();
        var vm = new NavMenuViewModel(menuItems, selectedComponentIndex, selectedStateIndex);

        // Act
        var result = validator.TestValidate(vm);

        // Assert
        result.ShouldHaveValidationErrorFor("MenuItems[0].ComponentStates[0].ComponentParameters");
    }
}
