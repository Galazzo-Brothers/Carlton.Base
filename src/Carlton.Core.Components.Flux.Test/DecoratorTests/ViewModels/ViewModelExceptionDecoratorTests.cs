using AutoFixture.AutoMoq;
using Carlton.Core.Components.Flux.Contracts;
using Carlton.Core.Components.Flux.Decorators.Queries;
using Carlton.Core.Components.Flux.Models;
using Carlton.Core.Components.Flux.Test.Common;
using Carlton.Core.Components.Flux.Test.Common.Extensions;
using Microsoft.Extensions.Logging;

namespace Carlton.Core.Components.Flux.Test.DecoratorTests.ViewModels;

public class ViewModelExceptionDecoratorTests
{
    private readonly IFixture _fixture;
    private readonly Mock<IViewModelQueryDispatcher<TestState>> _decorated;
    private readonly Mock<ILogger<ViewModelExceptionDecorator<TestState>>> _logger;

    public ViewModelExceptionDecoratorTests()
    { 
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
        _decorated = _fixture.Freeze<Mock<IViewModelQueryDispatcher<TestState>>>();
        _logger = _fixture.Freeze<Mock<ILogger<ViewModelExceptionDecorator<TestState>>>>();
    }

    [Theory]
    [MemberData(nameof(TestDataGenerator.GetViewModelData), MemberType = typeof(TestDataGenerator))]
    public async Task Dispatch_DispatchCalled_AssertViewModels<TViewModel>(TViewModel expectedResult)
    {
        //Arrange
        var sender = new JsRefreshCaller();
        var query = new ViewModelQuery();
        var sut = _fixture.Create<ViewModelExceptionDecorator<TestState>>();

        _decorated.SetupDispatcher(expectedResult);

        //Act 
        var actualResult = await sut.Dispatch<TViewModel>(sender, query, CancellationToken.None);

        //Assert
        _decorated.VerifyDispatch<TViewModel>(query);
        Assert.Equal(expectedResult, actualResult);
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
        var sut = _fixture.Create<ViewModelExceptionDecorator<TestState>>();

        //Act
        var thrownEx = await Assert.ThrowsAsync<ViewModelFluxException<TestState, TestViewModel1>>(async () => await sut.Dispatch<TestViewModel1>(sender, query, CancellationToken.None));

        //Assert
        Assert.Equivalent(expectedMessage, thrownEx.Message);
    }
}
