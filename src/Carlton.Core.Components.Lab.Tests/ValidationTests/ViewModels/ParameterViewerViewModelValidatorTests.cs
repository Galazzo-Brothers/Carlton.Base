using AutoFixture;
using Carlton.Core.Components.Lab.Models.Validators.ViewModels;
using FluentValidation.TestHelper;

namespace Carlton.Core.Components.Lab.Test.ValidationTests.ViewModels;

public class ParameterViewerViewModelValidatorTests
{
    private readonly IFixture _fixture;

    public ParameterViewerViewModelValidatorTests()
    {
        _fixture = new Fixture();
    }

    [Fact]
    public void ParameterViewerViewModelValidator_ShouldPassValidation()
    {
        // Arrange
        var paramter = _fixture.Create<ComponentParameters>();
        var validator = new ParametersViewerViewModelValidator();
        var vm = new ParametersViewerViewModel(paramter);

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
        var paramter = new ComponentParameters(null, ParameterObjectType.ParameterObject);
        var validator = new ParametersViewerViewModelValidator();
        var vm = new ParametersViewerViewModel(paramter);

        // Act
        var result = validator.TestValidate(vm);

        // Assert
        result.ShouldHaveValidationErrorFor(_ => _.ComponentParameters.ParameterObj);
    }
}
