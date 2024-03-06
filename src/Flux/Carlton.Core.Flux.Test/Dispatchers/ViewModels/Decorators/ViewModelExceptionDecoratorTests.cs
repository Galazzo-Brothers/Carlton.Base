using Carlton.Core.Flux.Contracts;
using Carlton.Core.Flux.Dispatchers.ViewModels;
using Carlton.Core.Flux.Dispatchers.ViewModels.Decorators;
using Carlton.Core.Flux.Extensions;
using Carlton.Core.Flux.Logging;
using Carlton.Core.Foundation.Test;
using Microsoft.Extensions.Logging;
namespace Carlton.Core.Flux.Tests.DecoratorTests.ViewModels;

public class ViewModelExceptionDecoratorTests
{
    [Theory, AutoNSubstituteData]
    public async Task Dispatch_DispatchCalled_AssertViewModels(
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
        var actualResult = (await sut.Dispatch(sender, queryContext, CancellationToken.None)).GetViewModelResultOrThrow();

        //Assert
        decorated.VerifyQueryDispatcher(1);
        logger.Received().ViewModelQueryCompleted(queryContext.FluxOperationTypeName);
        actualResult.ShouldBe(expectedResult);
    }

    [Theory, AutoNSubstituteData]
    public async Task Dispatch_Errors_ReturnsUnhandledFluxError(
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
        result.ShouldBe(error);
    }
}
