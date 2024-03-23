using Carlton.Core.Lab.Components.ComponentViewer;
namespace Carlton.Core.Lab.Tests.ValidationTests.Commands;

public class InitSelectionCommandValidationTests
{
	[Theory, AutoData]
	public void InitSelectionCommand_ShouldPassValidation(
		string component,
		string componentState)
	{
		// Arrange
		var sut = new InitSelectionCommand
		{ ComponentName = component, ComponentState = componentState };

		// Act
		var isValid = sut.TryValidate(out var validationErrors);

		// Assert
		isValid.ShouldBeTrue();
	}

	[Theory, AutoData]
	public void InitSelectionCommand_NullComponent_ShouldPassValidation(
		string componentState)
	{
		// Arrange
		var sut = new InitSelectionCommand
		{ ComponentName = null, ComponentState = componentState };

		// Act
		var isValid = sut.TryValidate(out var validationErrors);

		// Assert
		isValid.ShouldBeFalse();
	}

	[Theory, AutoData]
	public void InitSelectionCommand_EmptyComponent_ShouldPassValidation(
		string componentState)
	{
		// Arrange
		var sut = new InitSelectionCommand
		{ ComponentName = string.Empty, ComponentState = componentState };

		// Act
		var isValid = sut.TryValidate(out var validationErrors);

		// Assert
		isValid.ShouldBeFalse();
	}

	[Theory, AutoData]
	public void InitSelectionCommand_NullComponentState_ShouldPassValidation(
		string component)
	{
		// Arrange
		var sut = new InitSelectionCommand
		{ ComponentName = component, ComponentState = null };

		// Act
		var isValid = sut.TryValidate(out var validationErrors);

		// Assert
		isValid.ShouldBeFalse();
	}

	[Theory, AutoData]
	public void InitSelectionCommand_EmptyComponentState_ShouldPassValidation(
		string component)
	{
		// Arrange
		var sut = new InitSelectionCommand
		{ ComponentName = component, ComponentState = string.Empty };

		// Act
		var isValid = sut.TryValidate(out var validationErrors);

		// Assert
		isValid.ShouldBeFalse();
	}
}
