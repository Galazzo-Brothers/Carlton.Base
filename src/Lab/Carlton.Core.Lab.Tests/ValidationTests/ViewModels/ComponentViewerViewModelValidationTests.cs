namespace Carlton.Core.Lab.Test.ValidationTests.ViewModels;

public class ComponentViewerViewModelValidationTests
{
	[Theory, AutoData]
	public void ComponentViewerViewModel_ShouldPassValidation(
		Type componentType,
		object componentParameters)
	{
		// Arrange
		var sut = new ComponentViewerViewModel
		{
			ComponentType = componentType,
			ComponentParameters = componentParameters
		};

		// Act
		var result = sut.TryValidate(out var validationErrors);

		// Assert
		result.ShouldBeTrue();
	}

	[Theory, AutoData]
	public void ComponentViewerViewModel_NullComponentType_ShouldFailValidation(
		object componentParameters)
	{
		// Arrange
		var sut = new ComponentViewerViewModel
		{
			ComponentType = null,
			ComponentParameters = componentParameters
		};

		// Act
		var result = sut.TryValidate(out var validationErrors);

		// Assert
		result.ShouldBeFalse();
	}

	[Theory, AutoData]
	public void ComponentViewerViewModel_NullComponentParameters_ShouldFailValidation(
		Type componentType)
	{
		// Arrange
		var sut = new ComponentViewerViewModel
		{
			ComponentType = componentType,
			ComponentParameters = null
		};

		// Act
		var result = sut.TryValidate(out var validationErrors);

		// Assert
		result.ShouldBeFalse();
	}
}
