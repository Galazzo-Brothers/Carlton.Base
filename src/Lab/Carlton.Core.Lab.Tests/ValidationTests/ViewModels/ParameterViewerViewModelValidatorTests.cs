using Carlton.Core.Components.Lab.Models.Validators.ViewModels;
using FluentValidation.TestHelper;

namespace Carlton.Core.Components.Lab.Test.ValidationTests.ViewModels;

public class ParameterViewerViewModelValidatorTests
{
    [Fact]
    public void ParameterViewerViewModelValidator_ShouldPassValidation()
    {
        // Arrange
        var parameter = new ComponentParameters(new object(), ParameterObjectType.ParameterObject);
        var validator = new ParametersViewerViewModelValidator();
        var vm = new ParametersViewerViewModel(parameter);

        // Act
        var result = validator.TestValidate(vm);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void InvalidParameterViewerViewModelValidator_NullParameter_ShouldFailValidation()
    {
        // Arrange
        var validator = new ParametersViewerViewModelValidator();
        var vm = new ParametersViewerViewModel(null);

        // Act
        var result = validator.TestValidate(vm);

        // Assert
        result.ShouldHaveValidationErrorFor(_ => _.ComponentParameters);
    }

    [Fact]
    public void InvalidParameterViewerViewModelValidator_NullParameterObj_ShouldFailValidation()
    {
        // Arrange
        var parameter = new ComponentParameters(null, ParameterObjectType.ParameterObject);
        var validator = new ParametersViewerViewModelValidator();
        var vm = new ParametersViewerViewModel(parameter);

        // Act
        var result = validator.TestValidate(vm);

        // Assert
        result.ShouldHaveValidationErrorFor(_ => _.ComponentParameters.ParameterObj);
    }
}
