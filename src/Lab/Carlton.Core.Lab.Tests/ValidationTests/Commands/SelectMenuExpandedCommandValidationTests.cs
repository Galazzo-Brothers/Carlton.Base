namespace Carlton.Core.Lab.Test.ValidationTests.Commands;

public class SelectMenuExpandedCommandValidationTests
{
	[Theory, AutoData]
	public void SelectMenuExpandedCommand_ShouldPassValidation(int index, bool isExpanded)
	{
		// Arrange
		var sut = new SelectMenuExpandedCommand
		{
			SelectedComponentIndex = index,
			IsExpanded = isExpanded
		};

		// Act
		var result = sut.TryValidate(out var validationErrors);

		// Assert
		result.ShouldBeTrue();
	}

	[Theory]
	[InlineAutoData(-5)]
	public void SelectMenuExpandedCommand_NegativeSelectedComponentIndex_ShouldFailValidation(int index, bool isExpanded)
	{
		// Arrange
		var sut = new SelectMenuExpandedCommand
		{
			SelectedComponentIndex = index,
			IsExpanded = isExpanded
		};

		// Act
		var result = sut.TryValidate(out var validationErrors);

		// Assert
		result.ShouldBeFalse();
	}
}
