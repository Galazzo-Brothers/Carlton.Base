namespace Carlton.Core.Lab.Test.ValidationTests.Commands;

public class SelectMenuItemCommandValidationTests
{
	[Theory, AutoData]
	public void SelectMenuItemCommandCommand_ShouldPassValidation(
		int componentIndex,
		int stateIndex)
	{
		// Arrange
		var sut = new SelectMenuItemCommand
		{
			ComponentIndex = componentIndex,
			ComponentStateIndex = stateIndex
		};

		// Act
		var result = sut.TryValidate(out var validationResults);

		// Assert
		result.ShouldBeTrue();
	}

	[Theory]
	[InlineAutoData(-5, 0)]
	public void SelectMenuItemCommand_NegativeComponentIndex_ShouldFailValidation(
		int componentIndex,
		int stateIndex)
	{
		// Arrange
		var sut = new SelectMenuItemCommand
		{
			ComponentIndex = componentIndex,
			ComponentStateIndex = stateIndex
		};

		// Act
		var result = sut.TryValidate(out var validationResults);

		// Assert
		result.ShouldBeFalse();
	}

	[Theory]
	[InlineAutoData(0, -5)]
	public void SelectMenuItemCommand_NegativeComponentStateIndex_ShouldFailValidation(
		int componentIndex,
		int stateIndex)
	{
		// Arrange
		var sut = new SelectMenuItemCommand
		{
			ComponentIndex = componentIndex,
			ComponentStateIndex = stateIndex
		};

		// Act
		var result = sut.TryValidate(out var validationResults);

		// Assert
		result.ShouldBeFalse();
	}
}
