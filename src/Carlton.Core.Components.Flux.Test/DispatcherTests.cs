using Carlton.Core.Components.Flux.Contracts;
using Carlton.Core.Components.Flux.Dispatchers;
using Carlton.Core.Components.Flux.Models;
using Microsoft.Extensions.DependencyInjection;
using Moq;
namespace Carlton.Core.Components.Flux.Test;

public record TestViewModel(int ID, string Name, string Description);

public class DispatcherTests
{
    private readonly IViewModelQueryDispatcher<object> _queryDispatcher;
    private readonly Mock<IViewModelQueryHandler<object, TestViewModel>> _queryHandler = new();
    private readonly TestViewModel _expectedResults = new(1, "Testing", "This is a test");

    public DispatcherTests()
    {
        var mockServiceProvider = new Mock<IServiceProvider>();
        _queryHandler.Setup(_ => _.Handle(It.IsAny<ViewModelQuery>(), CancellationToken.None)).Returns(Task.FromResult(_expectedResults));
        mockServiceProvider.Setup(_ => _.GetService(typeof(IViewModelQueryHandler<object, TestViewModel>))).Returns(_queryHandler.Object);

        _queryDispatcher = new ViewModelQueryDispatcher<object>(mockServiceProvider.Object);
    }

    [Fact]
    public async Task Test1()
    {
        //Arrange
        TestViewModel actualResult;

        //Act
        actualResult = await _queryDispatcher.Dispatch<TestViewModel>(new ViewModelQuery(this), CancellationToken.None);

        //Assert
        Assert.Equal(_expectedResults, actualResult);
    }
}