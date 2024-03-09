using Carlton.Core.Flux.Contracts;
using Carlton.Core.Foundation.Test;
using Carlton.Core.Flux.Dispatchers.Mutations.Decorators;
using Carlton.Core.Flux.Dispatchers.Mutations;
namespace Carlton.Core.Flux.Tests.Dispatchers.Mutations.Decorators;

public class MutationCommandValidationDecoratorTests
{
	[Theory, AutoNSubstituteData]
	public async Task ValidationDecoratorDispatch_DispatchCalled_ReturnsMutationCommandResult(
	   [Frozen] IMutationCommandDispatcher<TestState> decorated,
	   MutationValidationDecorator<TestState> sut,
	   object sender,
	   MutationCommandContext<TestCommand1> context,
	   MutationCommandResult expectedResult)
	{
		//Arrange
		decorated.SetupMutationDispatcher<TestCommand1>(context.MutationCommand);

		//Act 
		var actualResult = await sut.Dispatch(sender, context, CancellationToken.None);

		//Assert
		decorated.VerifyCommandDispatcher(1, context.MutationCommand);
		actualResult.IsSuccess.ShouldBeTrue();
		actualResult.ShouldBe(expectedResult);
		context.RequestValidated.ShouldBeTrue();
		context.ValidationResult.ValidationPassed.ShouldBeTrue();
	}

	[Theory, AutoNSubstituteData]
	public async Task ValidationDecoratorDispatch_DispatchCalled_WithFailedValidation_ReturnsValidationError(
	  [Frozen] IMutationCommandDispatcher<TestState> decorated,
	  MutationValidationDecorator<TestState> sut,
	  object sender)
	{
		//Arrange
		var invalidCommand = new TestCommand1(-1, "Testing", "This should fail");
		var context = new MutationCommandContext<TestCommand1>(invalidCommand);
		decorated.SetupMutationDispatcher<TestCommand1>(invalidCommand);

		//Act 
		var actualResult = await sut.Dispatch(sender, context, CancellationToken.None);

		//Assert
		decorated.VerifyCommandDispatcher(0, context.MutationCommand);
		context.RequestValidated.ShouldBeTrue();
		context.ValidationResult.ValidationErrors.ShouldNotBeEmpty();
		actualResult.GetError().ShouldBeOfType<ValidationError>();
	}
}
