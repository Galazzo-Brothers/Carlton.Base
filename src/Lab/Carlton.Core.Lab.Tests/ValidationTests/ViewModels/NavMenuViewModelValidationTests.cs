using Carlton.Core.Lab.Models.Common;
namespace Carlton.Core.Lab.Test.ValidationTests.ViewModels;

public class NavMenuViewModelValidationTests
{
	[Theory, AutoData]
	public void NavMenuViewModel_ShouldPassValidation(
		int selectedComponentIndex,
		int selectedStateIndex,
		IEnumerable<ComponentConfigurations> componentConfigurations)
	{
		// Arrange
		var sut = new NavMenuViewModel
		{
			MenuItems = componentConfigurations,
			SelectedComponentIndex = selectedComponentIndex,
			SelectedStateIndex = selectedStateIndex
		};

		// Act
		var result = sut.TryValidate(out var validationErrors);

		// Assert
		result.ShouldBeTrue();
	}

	[Theory]
	[InlineAutoData(-5, 2)]
	public void NavMenuViewModel_NegativeSelectedComponentIndex_ShouldPassValidation(
		int selectedComponentIndex,
		int selectedStateIndex,
		IEnumerable<ComponentConfigurations> componentConfigurations)
	{
		// Arrange
		var sut = new NavMenuViewModel
		{
			MenuItems = componentConfigurations,
			SelectedComponentIndex = selectedComponentIndex,
			SelectedStateIndex = selectedStateIndex
		};

		// Act
		var result = sut.TryValidate(out var validationErrors);

		// Assert
		result.ShouldBeFalse();
	}

	[Theory]
	[InlineAutoData(2, -5)]
	public void NavMenuViewModel_NegativeSelectedStateIndex_ShouldPassValidation(
		int selectedComponentIndex,
		int selectedStateIndex,
		IEnumerable<ComponentConfigurations> componentConfigurations)
	{
		// Arrange
		var sut = new NavMenuViewModel
		{
			MenuItems = componentConfigurations,
			SelectedComponentIndex = selectedComponentIndex,
			SelectedStateIndex = selectedStateIndex
		};

		// Act
		var result = sut.TryValidate(out var validationErrors);

		// Assert
		result.ShouldBeFalse();
	}

	[Theory, AutoData]
	public void NavMenuViewModel_NullComponentStates_ShouldFailValidation(
		int selectedComponentIndex,
		int selectedStateIndex)
	{
		// Arrange
		var sut = new NavMenuViewModel
		{
			MenuItems = null,
			SelectedComponentIndex = selectedComponentIndex,
			SelectedStateIndex = selectedStateIndex
		};

		// Act
		var result = sut.TryValidate(out var validationErrors);

		// Assert
		result.ShouldBeFalse();
	}
}
