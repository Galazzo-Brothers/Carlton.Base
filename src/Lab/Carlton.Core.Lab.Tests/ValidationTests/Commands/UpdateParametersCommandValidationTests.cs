namespace Carlton.Core.Lab.Test.ValidationTests.Commands;

public class UpdateParametersCommandValidationTests
{
	[Theory, AutoData]
	public void UpdateParametersCommand_ShouldPassValidation(
		object parameters)
	{
		// Arrange
		var sut = new UpdateParametersCommand
		{
			Parameters = parameters
		};

		// Act
		var result = sut.TryValidate(out var validationErrors);

		// Assert
		result.ShouldBeTrue();
	}

	[Fact]
	public void UpdateParametersCommand_NullParameters_ShouldFailValidation()
	{
		// Arrange
		var sut = new UpdateParametersCommand
		{
			Parameters = null
		};

		// Act
		var result = sut.TryValidate(out var validationErrors);

		// Assert
		result.ShouldBeFalse();
	}
}
