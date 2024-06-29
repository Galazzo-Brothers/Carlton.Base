using Carlton.Core.Flux.Internals.Dispatchers.Mutations;
using Carlton.Core.Foundation.Tests;
namespace Carlton.Core.Flux.Tests.Internals.Dispatchers.Mutations.Decorators;

public class MutationCommandValidationDecoratorTests
{
	[Theory, AutoNSubstituteData]
	internal async Task ValidationDecoratorDispatch_DispatchCalled_ReturnsMutationCommandResult(
	   [Frozen] IMutationCommandDispatcher<TestState> decorated,
	   MutationValidationDecorator<TestState> sut,
	   object sender,
	   MutationCommandContext<TestCommand> context,
	   MutationCommandResult expectedResult)
	{
		//Arrange
		decorated.SetupCommandDispatcher(context.MutationCommand, expectedResult);

		//Act 
		var actualResult = await sut.Dispatch(sender, context, CancellationToken.None);

		//Assert
		decorated.VerifyCommandDispatcher(1, context.MutationCommand);
		context.RequestValidated.ShouldBeTrue();
		context.ValidationPassed.ShouldBeTrue();
		actualResult.IsSuccess.ShouldBeTrue();
		actualResult.ShouldBe(expectedResult);
	}

	[Theory, AutoNSubstituteData]
	internal async Task ValidationDecoratorDispatch_DispatchCalled_WithFailedValidation_ReturnsValidationError(
	  [Frozen] IMutationCommandDispatcher<TestState> decorated,
	  MutationValidationDecorator<TestState> sut,
	  object sender,
	  MutationCommandResult expectedResult)
	{
		//Arrange
		var invalidCommand = new TestCommand(-1, "Testing", "This should fail", 1, 2);
		var context = new MutationCommandContext<TestCommand>(invalidCommand);
		decorated.SetupCommandDispatcher(invalidCommand, expectedResult);

		//Act 
		var actualResult = await sut.Dispatch(sender, context, CancellationToken.None);

		//Assert
		context.RequestValidated.ShouldBeTrue();
		context.ValidationPassed.ShouldBeFalse();
		context.ValidationErrors.ShouldNotBeEmpty();
		decorated.VerifyCommandDispatcher(0, context.MutationCommand);
		actualResult.GetError().ShouldBeOfType<ValidationError>();
	}
}
