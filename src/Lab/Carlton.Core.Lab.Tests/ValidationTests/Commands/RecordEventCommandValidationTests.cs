namespace Carlton.Core.Components.Lab.Test.ValidationTests.Commands;

public class RecordEventCommandValidationTests
{
	[Theory, AutoData]
	public void RecordEventsCommand_ShouldPassValidation(
		string recordedEventName)
	{
		// Arrange
		var sut = new RecordEventCommand
		{
			RecordedEventName = recordedEventName
		};

		// Act
		var isValid = sut.TryValidate(out var validationErrors);

		// Assert
		isValid.ShouldBeTrue();
	}

	[Theory, AutoData]
	public void RecordEventsCommand_NullEventName_ShouldFailValidation(
		string recordedEventName)
	{
		// Arrange
		var command = new RecordEventCommand
		{
			RecordedEventName = recordedEventName
		};
		var sut = command with { RecordedEventName = null };

		// Act
		var isValid = sut.TryValidate(out var validationErrors);

		// Assert
		isValid.ShouldBeFalse();
	}

	[Theory, AutoData]
	public void RecordEventsCommand_EmptyEventName_ShouldFailValidation(
		string recordedEventName)
	{
		// Arrange
		var command = new RecordEventCommand
		{
			RecordedEventName = recordedEventName
		};
		var sut = command with { RecordedEventName = string.Empty };

		// Act
		var isValid = sut.TryValidate(out var validationErrors);

		// Assert
		isValid.ShouldBeFalse();
	}
}
