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
        var sender = new JsRefreshCaller();
        var query = new ViewModelQuery();

        //Act 
        await _dispatcher.Dispatch<TViewModel>(sender, query, CancellationToken.None);

        //Assert
        _decorated.VerifyDispatch<TViewModel>(query);
    }

    [Theory]
    [MemberData(nameof(TestDataGenerator.GetViewModelData), MemberType = typeof(TestDataGenerator))]
    public async Task Dispatch_AssertViewModels<TViewModel>(TViewModel expectedViewModel)
    {
        //Arrange
        var sender = new object();
        var query = new ViewModelQuery();
        _decorated.Setup(_ => _.Dispatch<TViewModel>(It.IsAny<object>(), It.IsAny<ViewModelQuery>(), CancellationToken.None)).Returns(Task.FromResult(expectedViewModel));

        //Act 
        var actualViewModel = await _dispatcher.Dispatch<TViewModel>(sender,query, CancellationToken.None);

        //Assert
        Assert.Equal(expectedViewModel, actualViewModel);
    }

    [Theory]
    [MemberData(nameof(TestDataGenerator.GetViewModelExceptionData), MemberType = typeof(TestDataGenerator))]
    public async Task Dispatch_ThrowsException<TException>(TException ex, string expectedMessage)
        where TException : Exception
    {
        //Arrange
        var sender = new object();
        _decorated.Setup(_ => _.Dispatch<TestViewModel1>(It.IsAny<object>(), It.IsAny<ViewModelQuery>(), CancellationToken.None)).ThrowsAsync(ex);
        var query = new ViewModelQuery();

        //Act
        var thrownEx = await Assert.ThrowsAsync<ViewModelFluxException<TestState, TestViewModel1>>(async () => await _dispatcher.Dispatch<TestViewModel1>(sender, query, CancellationToken.None));

        //Assert
        Assert.Equivalent(expectedMessage, thrownEx.Message);
    }
}
