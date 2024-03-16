namespace Carlton.Core.Lab.Test.ValidationTests.ViewModels;

public class BreadCrumbsViewModelValidationTests
{
	[Theory, AutoData]
	public void BreadCrumbsViewModel_ShouldPassValidation(
		string selectedComponent, string selectedComponentState)
	{
		// Arrange
		var sut = new BreadCrumbsViewModel
		{
			SelectedComponent = selectedComponent,
			SelectedComponentState = selectedComponentState
		};

		// Act
		var result = sut.TryValidate(out var validationErrors);

		// Assert
		result.ShouldBeTrue();
	}

	[Theory, AutoData]
	public void BreadCrumbsViewModel_NullSelectedComponent_ShouldFailValidation(string selectedComponentState)
	{
		// Arrange
		var sut = new BreadCrumbsViewModel
		{
			SelectedComponent = null,
			SelectedComponentState = selectedComponentState
		};

		// Act
		var result = sut.TryValidate(out var validationErrors);

		// Assert
		result.ShouldBeFalse();
	}

	[Theory, AutoData]
	public void BreadCrumbsViewModel_EmptySelectedComponent_ShouldFailValidation(string selectedComponentState)
	{
		// Arrange
		var sut = new BreadCrumbsViewModel
		{
			SelectedComponent = string.Empty,
			SelectedComponentState = selectedComponentState
		};

		// Act
		var result = sut.TryValidate(out var validationErrors);

		// Assert
		result.ShouldBeFalse();
	}

	[Theory, AutoData]
	public void BreadCrumbsViewModel_NullSelectedComponentState_ShouldFailValidation(string selectedComponent)
	{
		// Arrange
		var sut = new BreadCrumbsViewModel
		{
			SelectedComponent = selectedComponent,
			SelectedComponentState = null
		};

		// Act
		var result = sut.TryValidate(out var validationErrors);

		// Assert
		result.ShouldBeFalse();
	}

	[Theory, AutoData]
	public void BreadCrumbsViewModel_EmptySelectedComponentState_ShouldFailValidation(string selectedComponent)
	{
		// Arrange
		var sut = new BreadCrumbsViewModel
		{
			SelectedComponent = selectedComponent,
			SelectedComponentState = string.Empty
		};

		// Act
		var result = sut.TryValidate(out var validationErrors);

		// Assert
		result.ShouldBeFalse();
	}
}
