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
    public async Task ExceptionDecoratorDispatch_DispatchCalled_ReturnsViewModel(
        [Frozen] IViewModelQueryDispatcher<TestState> decorated,
        [Frozen] ILogger<ViewModelExceptionDecorator<TestState>> logger,
        ViewModelExceptionDecorator<TestState> sut,
        object sender,
        ViewModelQueryContext<TestViewModel> queryContext,
        TestViewModel expectedResult)
    {
        //Arrange
        decorated.SetupQueryDispatcher(expectedResult);

        //Act 
        var actualResult = await sut.Dispatch(sender, queryContext, CancellationToken.None);

        //Assert
        decorated.VerifyQueryDispatcher(1);
        logger.Received().ViewModelQueryCompleted(queryContext.FluxOperationTypeName);
        actualResult.IsSuccess.ShouldBeTrue();
        actualResult.ShouldBe(expectedResult);
    }

    [Theory, AutoNSubstituteData]
    public async Task ExceptionDecoratorDispatch_Errored_ReturnsUnhandledFluxError(
       [Frozen] IViewModelQueryDispatcher<TestState> decorated,
       [Frozen] ILogger<ViewModelExceptionDecorator<TestState>> logger,
       ViewModelExceptionDecorator<TestState> sut,
       object sender,
       ViewModelQueryContext<TestViewModel> queryContext)
    {
        //Arrange
        var error = new TestError(queryContext);
        decorated.SetupQueryDispatcherError(error);

        //Act 
        var result = await sut.Dispatch(sender, queryContext, CancellationToken.None);

        //Assert
        result.IsSuccess.ShouldBeFalse();
        result.ShouldBe(error);
        logger.Received().ViewModelQueryErrored(queryContext.FluxOperationTypeName, error);
        queryContext.RequestResult.RequestSucceeded.ShouldBeFalse();
        queryContext.RequestResult.RequestEndTimestamp.ShouldBeGreaterThan(DateTimeOffset.MinValue);
    }

    [Theory, AutoNSubstituteData]
    public async Task ExceptionDecoratorDispatch_UnhandledException_ReturnsUnhandledFluxError(
        [Frozen] IViewModelQueryDispatcher<TestState> decorated,
        [Frozen] ILogger<ViewModelExceptionDecorator<TestState>> logger,
        ViewModelExceptionDecorator<TestState> sut,
        object sender,
        ViewModelQueryContext<TestViewModel> queryContext,
        Exception ex)
    {
        //Arrange
        var error = new UnhandledFluxError(ex, queryContext);
        decorated.SetupQueryDispatcherException(ex);

        //Act 
        var result = await sut.Dispatch(sender, queryContext, CancellationToken.None);

        //Assert
        result.IsSuccess.ShouldBeFalse();
        result.ShouldBe(error);
        logger.Received().ViewModelQueryErrored(queryContext.FluxOperationTypeName, ex);
        queryContext.RequestResult.RequestSucceeded.ShouldBeFalse();
        queryContext.RequestResult.RequestEndTimestamp.ShouldBeGreaterThan(DateTimeOffset.MinValue);
    }
}
