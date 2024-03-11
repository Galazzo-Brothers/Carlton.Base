using Carlton.Core.Flux.Contracts;
using Carlton.Core.Flux.Dispatchers.ViewModels;
using Carlton.Core.Flux.Dispatchers.ViewModels.Decorators;
using Carlton.Core.Flux.Logging;
using Carlton.Core.Foundation.Test;
using Microsoft.Extensions.Logging;
namespace Carlton.Core.Flux.Tests.DecoratorTests.ViewModels;

public class ViewModelExceptionDecoratorTests
{
	[Theory, AutoNSubstituteData]
	internal async Task ExceptionDecoratorDispatch_DispatchCalled_ReturnsViewModel(
		[Frozen] IViewModelQueryDispatcher<TestState> decorated,
		[Frozen] ILogger<ViewModelExceptionDecorator<TestState>> logger,
		ViewModelExceptionDecorator<TestState> sut,
		object sender,
		ViewModelQueryContext<TestViewModel> context,
		TestViewModel expectedResult)
	{
		//Arrange
		decorated.SetupQueryDispatcher(expectedResult);

		//Act 
		var actualResult = await sut.Dispatch(sender, context, CancellationToken.None);

		//Assert
		decorated.VerifyQueryDispatcher<TestViewModel>(1);
		logger.Received().ViewModelQueryCompleted(context.FluxOperationTypeName);
		actualResult.IsSuccess.ShouldBeTrue();
		actualResult.ShouldBe(expectedResult);
	}

	[Theory, AutoNSubstituteData]
	internal async Task ExceptionDecoratorDispatch_Errored_ReturnsUnhandledFluxError(
	   [Frozen] IViewModelQueryDispatcher<TestState> decorated,
	   [Frozen] ILogger<ViewModelExceptionDecorator<TestState>> logger,
	   ViewModelExceptionDecorator<TestState> sut,
	   object sender,
	   ViewModelQueryContext<TestViewModel> context,
	   TestError error)
	{
		//Arrange
		decorated.SetupQueryDispatcherError<TestViewModel>(error);

		//Act 
		var result = await sut.Dispatch(sender, context, CancellationToken.None);

		//Assert
		result.IsSuccess.ShouldBeFalse();
		result.ShouldBe(error);
		logger.Received().ViewModelQueryErrored(context.FluxOperationTypeName);
		context.RequestSucceeded.ShouldBeFalse();
		context.RequestEndTimestamp.ShouldBeGreaterThan(DateTimeOffset.MinValue);
	}

	[Theory, AutoNSubstituteData]
	internal async Task ExceptionDecoratorDispatch_UnhandledException_ReturnsUnhandledFluxError(
		[Frozen] IViewModelQueryDispatcher<TestState> decorated,
		[Frozen] ILogger<ViewModelExceptionDecorator<TestState>> logger,
		ViewModelExceptionDecorator<TestState> sut,
		object sender,
		ViewModelQueryContext<TestViewModel> context,
		Exception ex)
	{
		//Arrange
		var error = UnhandledFluxError(ex);
		decorated.SetupQueryDispatcherException<TestViewModel>(ex);

		//Act 
		var result = await sut.Dispatch(sender, context, CancellationToken.None);

		//Assert
		result.IsSuccess.ShouldBeFalse();
		result.ShouldBe(error);
		logger.Received().ViewModelQueryErrored(context.FluxOperationTypeName, ex);
		context.RequestSucceeded.ShouldBeFalse();
		context.RequestEndTimestamp.ShouldBeGreaterThan(DateTimeOffset.MinValue);
	}
}
