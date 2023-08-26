using Carlton.Core.Components.Flux.Contracts;
using Carlton.Core.Components.Flux.Decorators.Queries;
using Carlton.Core.Components.Flux.Models;
using Carlton.Core.Components.Flux.Test.Common;
using Microsoft.Extensions.Logging;
using Moq;

namespace Carlton.Core.Components.Flux.Test.DecoratorTests.ViewModels;

public class ViewModelExceptionDecoratorTests
{
    private readonly Mock<IViewModelQueryDispatcher<TestState>> _decorated = new();
    private readonly Mock<ILogger<ViewModelExceptionDecorator<TestState>>> _logger = new();
    private readonly ViewModelExceptionDecorator<TestState> _dispatcher;

    public ViewModelExceptionDecoratorTests()
    {
        _dispatcher = new ViewModelExceptionDecorator<TestState>(_decorated.Object, _logger.Object);
    }

    [Theory]
    [MemberData(nameof(TestDataGenerator.GetViewModelData), MemberType = typeof(TestDataGenerator))]
    public async Task Dispatch_DispatchCalled<TViewModel>(TViewModel vm)
    {
        //Arrange
        var query = new ViewModelQuery(new JsRefreshCaller());

        //Act 
        await _dispatcher.Dispatch<TViewModel>(query, CancellationToken.None);

        //Assert
        _decorated.VerifyDispatchCalled<TViewModel>(query);
    }

    [Theory]
    [MemberData(nameof(TestDataGenerator.GetViewModelData), MemberType = typeof(TestDataGenerator))]
    public async Task Dispatch_AssertViewModels<TViewModel>(TViewModel expectedViewModel)
    {
        //Arrange
        _decorated.Setup(_ => _.Dispatch<TViewModel>(It.IsAny<ViewModelQuery>(), CancellationToken.None)).Returns(Task.FromResult(expectedViewModel));
        var query = new ViewModelQuery(this);

        //Act 
        var actualViewModel = await _dispatcher.Dispatch<TViewModel>(query, CancellationToken.None);

        //Assert
        Assert.Equal(expectedViewModel, actualViewModel);
    }

    [Theory]
    [MemberData(nameof(TestDataGenerator.GetViewModelExceptionData), MemberType = typeof(TestDataGenerator))]
    public async Task Dispatch_ThrowsException<TException>(TException ex, string expectedMessage)
        where TException : Exception
    {
        //Arrange
        _decorated.Setup(_ => _.Dispatch<TestViewModel1>(It.IsAny<ViewModelQuery>(), CancellationToken.None))
                  .ThrowsAsync(ex);

        var query = new ViewModelQuery(this);

        //Act
        var thrownEx = await Assert.ThrowsAsync<ViewModelFluxException<TestState, TestViewModel1>>(async () => await _dispatcher.Dispatch<TestViewModel1>(query, CancellationToken.None));

        //Assert
        Assert.Equivalent(expectedMessage, thrownEx.Message);
    }
}
