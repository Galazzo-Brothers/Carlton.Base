using Carlton.Core.Flux.Contracts;
using Carlton.Core.Foundation.Test;
using Microsoft.Extensions.Logging;
using Carlton.Core.Flux.Dispatchers.Mutations.Decorators;
using Carlton.Core.Flux.Dispatchers.Mutations;
using Carlton.Core.Flux.Logging;
namespace Carlton.Core.Flux.Tests.Dispatchers.Mutations.Decorators;

public class MutationCommandExceptionDecoratorTests
{
	[Theory, AutoNSubstituteData]
	public async Task ExceptionDecoratorDispatch_DispatchCalled_ReturnsViewModel(
		[Frozen] IMutationCommandDispatcher<TestState> decorated,
		[Frozen] ILogger<MutationExceptionDecorator<TestState>> logger,
		MutationExceptionDecorator<TestState> sut,
		object sender,
		MutationCommandContext<TestCommand1> context,
		MutationCommandResult expectedResult)
	{
		//Arrange
		decorated.SetupCommandDispatcher<TestCommand1>(context.MutationCommand);

		//Act 
		var actualResult = await sut.Dispatch(sender, context, CancellationToken.None);

		//Assert
		decorated.VerifyCommandDispatcher(1, context.MutationCommand);
		logger.Received().MutationCommandCompleted(context.FluxOperationTypeName);
		actualResult.IsSuccess.ShouldBeTrue();
		actualResult.ShouldBe(expectedResult);
	}

	[Theory, AutoNSubstituteData]
	public async Task ExceptionDecoratorDispatch_Errored_ReturnsUnhandledFluxError(
	   [Frozen] IMutationCommandDispatcher<TestState> decorated,
	   [Frozen] ILogger<MutationExceptionDecorator<TestState>> logger,
	   MutationExceptionDecorator<TestState> sut,
	   object sender,
	   MutationCommandContext<TestCommand1> context,
	   TestError error)
	{
		//Arrange
		decorated.SetupCommandDispatcherError(context.MutationCommand, error);

		//Act 
		var result = await sut.Dispatch(sender, context, CancellationToken.None);

		//Assert
		result.IsSuccess.ShouldBeFalse();
		result.ShouldBe(error);
		logger.Received().ViewModelQueryErrored(context.FluxOperationTypeName);
		context.RequestResult.RequestSucceeded.ShouldBeFalse();
		context.RequestResult.RequestEndTimestamp.ShouldBeGreaterThan(DateTimeOffset.MinValue);
	}

	[Theory, AutoNSubstituteData]
	public async Task ExceptionDecoratorDispatch_UnhandledException_ReturnsUnhandledFluxError(
		[Frozen] IMutationCommandDispatcher<TestState> decorated,
		[Frozen] ILogger<MutationExceptionDecorator<TestState>> logger,
		MutationExceptionDecorator<TestState> sut,
		object sender,
		MutationCommandContext<TestCommand1> context,
		Exception ex)
	{
		//Arrange
		var error = UnhandledFluxError(ex);
		decorated.SetupCommandDispatcherException<TestCommand1>(ex);

		//Act 
		var result = await sut.Dispatch(sender, context, CancellationToken.None);

		//Assert
		result.IsSuccess.ShouldBeFalse();
		result.ShouldBe(error);
		logger.Received().MutationCommandErrored(context.FluxOperationTypeName, ex);
		context.RequestResult.RequestSucceeded.ShouldBeFalse();
		context.RequestResult.RequestEndTimestamp.ShouldBeGreaterThan(DateTimeOffset.MinValue);
	}
}
