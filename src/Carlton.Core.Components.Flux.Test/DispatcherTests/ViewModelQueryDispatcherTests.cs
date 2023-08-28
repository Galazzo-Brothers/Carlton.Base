using Carlton.Core.Components.Flux.Contracts;
using Carlton.Core.Components.Flux.Dispatchers;
using Carlton.Core.Components.Flux.Models;
using Carlton.Core.Components.Flux.Test.Common;
using Moq;

namespace Carlton.Core.Components.Flux.Test.DispatcherTests;

public class ViewModelQueryDispatcherTests
{
    private readonly IViewModelQueryDispatcher<TestState> _queryDispatcher;
    private readonly Mock<IServiceProvider> _mockServiceProvider = new();

    public ViewModelQueryDispatcherTests()
    {
        _queryDispatcher = new ViewModelQueryDispatcher<TestState>(_mockServiceProvider.Object);
    }

    [Theory]
    [MemberData(nameof(TestDataGenerator.GetViewModelData), MemberType = typeof(TestDataGenerator))]
    public async Task Dispatch_AssertHandlerCalled<TViewModel>(TViewModel vm)
    {
        //Arrange
        var sender = new object();
        var query = new ViewModelQuery();
        var handler = new Mock<IViewModelQueryHandler<TestState, TViewModel>>();
        _mockServiceProvider.SetupServiceProvider<IViewModelQueryHandler<TestState, TViewModel>>(handler.Object);

        //Act
        _ = await _queryDispatcher.Dispatch<TViewModel>(sender,query, CancellationToken.None);

        //Assert
        handler.VerifyHandler();
    }

    [Theory]
    [MemberData(nameof(TestDataGenerator.GetViewModelData), MemberType = typeof(TestDataGenerator))]
    public async Task ViewModelDispatcher_Dispatch_AssertViewModelResponse<TViewModel>(TViewModel expectedViewModel)
    {
        //Arrange
        var sender = new object();
        var query = new ViewModelQuery();
        var handler = new Mock<IViewModelQueryHandler<TestState, TViewModel>>();
        handler.SetupHandler(expectedViewModel);
        _mockServiceProvider.SetupServiceProvider<IViewModelQueryHandler<TestState, TViewModel>>(handler.Object);
        
        //Act
        var actualViewModel = await _queryDispatcher.Dispatch<TViewModel>(sender, query, CancellationToken.None);

        //Assert
        Assert.Equal(expectedViewModel, actualViewModel);
    }
}



