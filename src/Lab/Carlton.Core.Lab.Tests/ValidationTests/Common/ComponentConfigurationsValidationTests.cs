using Carlton.Core.Lab.Models.Common;
namespace Carlton.Core.Lab.Test.ValidationTests.Common;

public class ComponentConfigurationsValidationTests
{
	[Theory, AutoData]
	public void ComponentConfiguration_ShouldPassValidation(
		Type type,
		IEnumerable<ComponentState> componentStates)
	{
		// Arrange
		var sut = new ComponentConfigurations
		{
			ComponentType = type,
			ComponentStates = componentStates
		};

		// Act
		var isValid = sut.TryValidate(out var validationErrors);

		// Assert
		isValid.ShouldBeTrue();
	}

	[Theory, AutoData]
	public void ComponentConfiguration_NullComponentType_ShouldFailValidation(
		IEnumerable<ComponentState> componentStates)
	{
		// Arrange
		var sut = new ComponentConfigurations
		{
			ComponentType = null,
			ComponentStates = componentStates
		};

		// Act
		var isValid = sut.TryValidate(out var validationErrors);

		// Assert
		isValid.ShouldBeFalse();
	}

	[Theory, AutoData]
	public void ComponentConfiguration_NullComponentStates_ShouldFailValidation(
		Type type)
	{
		// Arrange
		var sut = new ComponentConfigurations
		{
			ComponentType = type,
			ComponentStates = null
		};

		// Act
		var isValid = sut.TryValidate(out var validationErrors);

		// Assert
		isValid.ShouldBeFalse();
	}
}
