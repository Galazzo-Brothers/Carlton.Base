using Carlton.Core.Components.Flux.Contracts;
using Carlton.Core.Components.Flux.Dispatchers;
using Carlton.Core.Components.Flux.Models;
using Carlton.Core.Components.Flux.Test.Common;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Carlton.Core.Components.Flux.Test.DispatcherTests;

public class ViewModelQueryDispatcherTests
{
    private readonly IViewModelQueryDispatcher<object> _queryDispatcher;
    private readonly Mock<IViewModelQueryHandler<object, TestViewModel1>> _queryHandler = new();
    private readonly Mock<IViewModelQueryHandler<object, TestViewModel2>> _queryHandler2 = new();
    private readonly Mock<IServiceProvider> _mockServiceProvider = new();

    public ViewModelQueryDispatcherTests()
    {
        _queryHandler.Setup(_ => _.Handle(It.IsAny<ViewModelQuery>(), CancellationToken.None)).Returns(Task.FromResult(TestDataGenerator.ExpectedViewModel_1));
        _queryHandler2.Setup(_ => _.Handle(It.IsAny<ViewModelQuery>(), CancellationToken.None)).Returns(Task.FromResult(TestDataGenerator.ExpectedViewModel_2));
        _mockServiceProvider.Setup(_ => _.GetService(typeof(Mock<IViewModelQueryHandler<object, TestViewModel1>>))).Returns(_queryHandler);
        _mockServiceProvider.Setup(_ => _.GetService(typeof(Mock<IViewModelQueryHandler<object, TestViewModel2>>))).Returns(_queryHandler2);
        _mockServiceProvider.Setup(_ => _.GetService(typeof(IViewModelQueryHandler<object, TestViewModel1>))).Returns(_queryHandler.Object);
        _mockServiceProvider.Setup(_ => _.GetService(typeof(IViewModelQueryHandler<object, TestViewModel2>))).Returns(_queryHandler2.Object);
        _queryDispatcher = new ViewModelQueryDispatcher<object>(_mockServiceProvider.Object);
    }

    [Theory]
    [MemberData(nameof(TestDataGenerator.GetViewModelData), MemberType = typeof(TestDataGenerator))]
    public async Task Dispatch_AssertHandlerCalled<TViewModel>(TViewModel _)
    {
        //Arrange
        var handler = _mockServiceProvider.Object.GetRequiredService<Mock<IViewModelQueryHandler<object, TViewModel>>>();
        var query = new ViewModelQuery(this);

        //Act
        _ = await _queryDispatcher.Dispatch<TViewModel>(query, CancellationToken.None);

        //Assert
        handler.VerifyAll();
    }

    [Theory]
    [MemberData(nameof(TestDataGenerator.GetViewModelData), MemberType = typeof(TestDataGenerator))]
    public async Task ViewModelDispatcher_Dispatch_AssertViewModelResponse<TViewModel>(TViewModel expectedViewModel)
    {
        //Act
        var actualViewModel = await _queryDispatcher.Dispatch<TViewModel>(new ViewModelQuery(this), CancellationToken.None);

        //Assert
        Assert.Equal(expectedViewModel, actualViewModel);
    }
}

