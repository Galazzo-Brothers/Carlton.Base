//using AutoFixture;
//using AutoFixture.Xunit2;
//using Carlton.Core.Components.Lab.Models.Validators.ViewModels;
//using Carlton.Core.Components.Lab.Test.Mocks;
//using FluentValidation.TestHelper;

//namespace Carlton.Core.Components.Lab.Test.ValidationTests.ViewModels;

//public class NavMenuViewModelValidatorTests
//{
//    private readonly IFixture _fixture;

//    public NavMenuViewModelValidatorTests()
//    {
//        _fixture = new Fixture();
//    }

//    [Fact]
//    public void NavMenuViewModelValidatorTests_ShouldPassValidation()
//    {
//        // Arrange
//        var vm = _fixture.Create<NavMenuViewModel>();
//        var validator = new NavMenuViewModelValidator();


//        // Act
//        var result = validator.TestValidate(vm);

//        // Assert
//        result.ShouldNotHaveAnyValidationErrors();
//    }

//    [Fact]
//    public void InvalidNavMenuViewModelValidatorTests_NullComponentStates_ShouldFailValidation()
//    {
//        // Arrange
//        var validator = new NavMenuViewModelValidator();
//        var vm = new NavMenuViewModel(null);

//        // Act
//        var result = validator.TestValidate(vm);

//        // Assert
//        result.ShouldHaveValidationErrorFor(_ => _.MenuItems);
//    }

//    [Fact]
//    public void InvalidNavMenuViewModelValidatorTests_EmptyComponentStates_ShouldFailValidation()
//    {
//        // Arrange
//        var validator = new NavMenuViewModelValidator();
//        var vm = new NavMenuViewModel(new List<ComponentState>());

//        // Act
//        var result = validator.TestValidate(vm);

//        // Assert
//        result.ShouldHaveValidationErrorFor(_ => _.MenuItems);
//    }

//    [Fact]
//    public void InvalidNavMenuViewModelValidatorTests_NullComponentStatesDisplayName_ShouldFailValidation()
//    {
//        // Arrange
//        var menuItems = new List<ComponentState>
//        {
//                new ComponentState(null, typeof(DummyComponent), _fixture.Create<ComponentParameters>())
//        };
//        var validator = new NavMenuViewModelValidator();
//        var vm = new NavMenuViewModel(menuItems);

//        // Act
//        var result = validator.TestValidate(vm);

//        // Assert
//        result.ShouldHaveValidationErrorFor("MenuItems[0].DisplayName");
//    }

//    [Fact]
//    public void InvalidNavMenuViewModelValidatorTests_EmptyComponentStatesDisplayName_ShouldFailValidation()
//    {
//        //Arrange
//        var menuItems = new List<ComponentState>
//        {
//            new ComponentState(string.Empty, typeof(DummyComponent), _fixture.Create<ComponentParameters>())
//        };
//        var validator = new NavMenuViewModelValidator();
//        var vm = new NavMenuViewModel(menuItems);

//        //Act
//        var result = validator.TestValidate(vm);

//        //Assert
//        result.ShouldHaveValidationErrorFor("MenuItems[0].DisplayName");
//    }

//    [Theory, AutoData]
//    public void InvalidNavMenuViewModelValidatorTests_NullComponentStatesType_ShouldFailValidation(string displayName)
//    {
//        //Arrange
//        var menuItems = new List<ComponentState>
//        {
//            new ComponentState(displayName, null, _fixture.Create<ComponentParameters>())
//        };
//        var validator = new NavMenuViewModelValidator();
//        var vm = new NavMenuViewModel(menuItems);

//        //Act
//        var result = validator.TestValidate(vm);

//        //Assert
//        result.ShouldHaveValidationErrorFor("MenuItems[0].Type");
//    }

//    [Theory, AutoData]
//    public void InvalidNavMenuViewModelValidatorTests_NullComponentStatesComponentParameter_ShouldFailValidation(string displayName)
//    {
//        //Arrange
//        var menuItems = new List<ComponentState>
//        {
//            new ComponentState(displayName, typeof(DummyComponent), null)
//        };
//        var validator = new NavMenuViewModelValidator();
//        var vm = new NavMenuViewModel(menuItems);

//        //Act
//        var result = validator.TestValidate(vm);

//        //Assert
//        result.ShouldHaveValidationErrorFor("MenuItems[0].ComponentParameters");
//    }

//    [Theory, AutoData]
//    public void InvalidNavMenuViewModelValidatorTests_NullComponentStatesComponentParameterObject_ShouldFailValidation(string displayName)
//    {
//        //Arrange
//        var menuItems = new List<ComponentState>
//       {
//            new ComponentState(displayName, typeof(DummyComponent),
//                new ComponentParameters(null, ParameterObjectType.ParameterObject))
//       };
//        var validator = new NavMenuViewModelValidator();
//        var vm = new NavMenuViewModel(menuItems);

//        //Act
//        var result = validator.TestValidate(vm);

//        //Assert
//        result.ShouldHaveValidationErrorFor("MenuItems[0].ComponentParameters.ParameterObj");
//    }
//}
