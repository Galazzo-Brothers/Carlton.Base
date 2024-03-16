using Carlton.Core.Lab.Models.Common;
namespace Carlton.Core.Lab.Test.ValidationTests.ViewModels;

public class EventConsoleViewModelValidationTests
{
	[Theory, AutoData]
	public void EventConsoleViewModel_ShouldPassValidation(
		IEnumerable<ComponentRecordedEvent> recordedEvents)
	{
		// Arrange
		var sut = new EventConsoleViewModel
		{
			RecordedEvents = recordedEvents
		};

		// Act
		var result = sut.TryValidate(out var validationErrors);

		// Assert
		result.ShouldBeTrue();
	}

	[Fact]
	public void EventConsoleViewModel_NullRecordedEvents_ShouldFailValidation()
	{
		// Arrange
		var sut = new EventConsoleViewModel
		{
			RecordedEvents = null
		};

		// Act
		var result = sut.TryValidate(out var validationErrors);

		// Assert
		result.ShouldBeFalse();
	}
}
