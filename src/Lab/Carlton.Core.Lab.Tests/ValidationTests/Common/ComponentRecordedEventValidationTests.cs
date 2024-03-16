using Carlton.Core.Lab.Models.Common;
namespace Carlton.Core.Lab.Test.ValidationTests.Common;

public class ComponentRecordedEventValidationTests
{
	[Theory, AutoData]
	public void ComponentRecordedEvent_ShouldPassValidation(
		string name,
		object eventObj)
	{
		// Arrange
		var sut = new ComponentRecordedEvent
		{
			Name = name,
			EventObj = eventObj
		};

		// Act
		var isValid = sut.TryValidate(out var validationErrors);

		// Assert
		isValid.ShouldBeTrue();
	}

	[Theory, AutoData]
	public void ComponentRecordedEvent_NullName_ShouldFailValidation(
		object eventObj)
	{
		// Arrange
		var sut = new ComponentRecordedEvent
		{
			Name = null,
			EventObj = eventObj
		};

		// Act
		var isValid = sut.TryValidate(out var validationErrors);

		// Assert
		isValid.ShouldBeFalse();
	}

	[Theory, AutoData]
	public void ComponentRecordedEvent_EmptyName_ShouldFailValidation(
		object eventObj)
	{
		// Arrange
		var sut = new ComponentRecordedEvent
		{
			Name = string.Empty,
			EventObj = eventObj
		};

		// Act
		var isValid = sut.TryValidate(out var validationErrors);

		// Assert
		isValid.ShouldBeFalse();
	}

	[Theory, AutoData]
	public void ComponentRecordedEvent_NullEventObj_ShouldFailValidation(
		string name)
	{
		// Arrange
		var sut = new ComponentRecordedEvent
		{
			Name = name,
			EventObj = null
		};

		// Act
		var isValid = sut.TryValidate(out var validationErrors);

		// Assert
		isValid.ShouldBeFalse();
	}
}
