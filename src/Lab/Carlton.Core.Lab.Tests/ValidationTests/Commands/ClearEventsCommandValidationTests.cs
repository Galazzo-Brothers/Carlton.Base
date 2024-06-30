namespace Carlton.Core.Lab.Test.ValidationTests.Commands;

public class ClearEventsCommandValidationTests
{
	[Fact]
	public void ClearEventsCommand_ShouldPassValidation()
	{
		// Arrange
		var sut = new ClearEventsCommand();

		// Act
		var isValid = sut.TryValidate(out var validationErrors);

		// Assert
		isValid.ShouldBeTrue();
	}
}
