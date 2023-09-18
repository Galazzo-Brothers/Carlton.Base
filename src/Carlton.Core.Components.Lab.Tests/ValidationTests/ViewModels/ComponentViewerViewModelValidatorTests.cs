using AutoFixture;
using Carlton.Core.Components.Lab.Models.Validators.ViewModels;
using Carlton.Core.Components.Lab.Test.Mocks;
using FluentValidation.TestHelper;

namespace Carlton.Core.Components.Lab.Test.ValidationTests.ViewModels;

public class ComponentViewerViewModelValidatorTests
{
    [Fact]
    public void ValidComponentViewerViewModelValidator_ShouldPassValidation()
    {
        // Arrange
        var componentParameters = new ComponentParameters(new object(), ParameterObjectType.ParameterObject);
        var vm = new ComponentViewerViewModel(typeof(DummyComponent), componentParameters);
        var validator = new ComponentViewerViewModelValidator();

        // Act
        var result = validator.TestValidate(vm);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void InvalidComponentViewerViewModelValidator_NullComponentType_ShouldFailValidation()
    {
        // Arrange
        var componentParameters = new ComponentParameters(new object(), ParameterObjectType.ParameterObject);
        var validator = new ComponentViewerViewModelValidator();
        var vm = new ComponentViewerViewModel(null, componentParameters);

        // Act
        var result = validator.TestValidate(vm);

        // Assert
        result.ShouldHaveValidationErrorFor(_ => _.ComponentType);
    }

    [Fact]
    public void InvalidComponentViewerViewModelValidator_NullComponentParameters_ShouldFailValidation()
    {
        // Arrange
        var validator = new ComponentViewerViewModelValidator();
        var vm = new ComponentViewerViewModel(typeof(DummyComponent), null);

        // Act
        var result = validator.TestValidate(vm);

        // Assert
        result.ShouldHaveValidationErrorFor(_ => _.ComponentParameters);
    }

    [Fact]
    public void InvalidComponentViewerViewModelValidator_NullComponentParametersParameterObj_ShouldFailValidation()
    {
        // Arrange
        var componentParameters = new ComponentParameters(null, ParameterObjectType.ParameterObject);
        var validator = new ComponentViewerViewModelValidator();
        var vm = new ComponentViewerViewModel(typeof(DummyComponent), componentParameters);

        // Act
        var result = validator.TestValidate(vm);

        // Assert
        result.ShouldHaveValidationErrorFor(_ => _.ComponentParameters.ParameterObj);
    }
}
