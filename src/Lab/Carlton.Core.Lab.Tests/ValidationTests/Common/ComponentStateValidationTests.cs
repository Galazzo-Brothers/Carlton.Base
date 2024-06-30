using Carlton.Core.Lab.Models.Common;
namespace Carlton.Core.Lab.Test.ValidationTests.Common;

public class ComponentStateValidationTests
{
	[Theory, AutoData]
	public void ComponentState_ShouldPassValidation(
		string displayName,
		object componentParameters)
	{
		// Arrange
		var sut = new ComponentState
		{
			DisplayName = displayName,
			ComponentParameters = componentParameters
		};

		// Act
		var isValid = sut.TryValidate(out var validationErrors);

		// Assert
		isValid.ShouldBeTrue();
	}

	[Theory, AutoData]
	public void ComponentState_NullDisplayName_ShouldFailValidation(
		object componentParameters)
	{
		// Arrange
		var sut = new ComponentState
		{
			DisplayName = null,
			ComponentParameters = componentParameters
		};

		// Act
		var isValid = sut.TryValidate(out var validationErrors);

		// Assert
		isValid.ShouldBeFalse();
	}

	[Theory, AutoData]
	public void ComponentState_EmptyDisplayName_ShouldFailValidation(
		object componentParameters)
	{
		// Arrange
		var sut = new ComponentState
		{
			DisplayName = string.Empty,
			ComponentParameters = componentParameters
		};

		// Act
		var isValid = sut.TryValidate(out var validationErrors);

		// Assert
		isValid.ShouldBeFalse();
	}

	[Theory, AutoData]
	public void ComponentState_NullParameters_ShouldFailValidation(
		string displayName)
	{
		// Arrange
		var sut = new ComponentState
		{
			DisplayName = displayName,
			ComponentParameters = null
		};

		// Act
		var isValid = sut.TryValidate(out var validationErrors);

		// Assert
		isValid.ShouldBeFalse();
	}
}
