namespace Carlton.Core.Lab.Test.ValidationTests.ViewModels;

public class ParameterViewerViewModelValidationTests
{
	[Theory, AutoData]
	public void ParameterViewerViewModel_ShouldPassValidation(
		object parameters)
	{
		// Arrange
		var sut = new ParametersViewerViewModel
		{
			ComponentParameters = parameters
		};

		// Act
		var result = sut.TryValidate(out var validationErrors);

		// Assert
		result.ShouldBeTrue();
	}

	[Fact]
	public void ParameterViewerViewModel_NullParameter_ShouldFailValidation()
	{
		// Arrange
		var sut = new ParametersViewerViewModel
		{
			ComponentParameters = null
		};

		// Act
		var result = sut.TryValidate(out var validationErrors);

		// Assert
		result.ShouldBeFalse();
	}
}
