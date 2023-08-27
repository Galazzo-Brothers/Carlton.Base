using Carlton.Core.Components.Flux.Contracts;
using Carlton.Core.Components.Flux.Decorators.Queries;
using Carlton.Core.Components.Flux.Models;
using Carlton.Core.Components.Flux.Test.Common;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using Moq;
using System.Linq.Expressions;

namespace Carlton.Core.Components.Flux.Test.DecoratorTests.ViewModels;

public class ViewModelJsDecoratorTests
{
    private readonly Mock<IViewModelQueryDispatcher<TestState>> _decorated = new();
    private readonly Mock<IJSRuntime> _js = new();
    private readonly Mock<IJSObjectReference> _jsObject = new();
    private readonly Mock<IMutableFluxState<TestState>> _state = new();
    private readonly Mock<ILogger<ViewModelJsDecorator<TestState>>> _logger = new();
    private readonly ViewModelJsDecorator<TestState> _dispatcher;

    private readonly Expression<Func<IJSRuntime, ValueTask<IJSObjectReference>>> jsModuleVerifyExpression
        = mock => mock.InvokeAsync<IJSObjectReference>("import", new object[] { "test_module" });

    public ViewModelJsDecoratorTests()
    {
        _js.Setup(_ => _.InvokeAsync<IJSObjectReference>("import", new object[] { "test_module" })).Returns(ValueTask.FromResult(_jsObject.Object));
        _dispatcher = new ViewModelJsDecorator<TestState>(_decorated.Object, _js.Object, _state.Object, _logger.Object);
    }

    [Theory]
    [MemberData(nameof(TestDataGenerator.GetViewModelData), MemberType = typeof(TestDataGenerator))]
    public async Task Dispatch_DispatchAndJsModuleCalled<TViewModel>(TViewModel vm)
    {
        //Arrange
        var query = new ViewModelQuery(new JsRefreshCaller());

        //Act 
        await _dispatcher.Dispatch<TViewModel>(query, CancellationToken.None);

        //Assert
        _js.Verify(jsModuleVerifyExpression, Times.Once);
        _decorated.VerifyDispatchCalled<TViewModel>(query);
    }

    [Theory]
    [MemberData(nameof(TestDataGenerator.GetJsCallersData), MemberType = typeof(TestDataGenerator))]
    public async Task Dispatch_JsObjectCalled( object jsCaller)
    {
        //Arrange
        var query = new ViewModelQuery(jsCaller);

        //Act 
        await _dispatcher.Dispatch<TestViewModel1>(query, CancellationToken.None);

        //Assert
        _jsObject.VerifyAll();
    }

    [Fact]
    public async Task Dispatch_JsModuleNotCalled()
    {
        //Arrange
        var query = new ViewModelQuery(new NoRefreshCaller());

        //Act 
        await _dispatcher.Dispatch<TestViewModel1>(query, CancellationToken.None);

        //Assert
        _js.Verify(jsModuleVerifyExpression, Times.Never);
        _decorated.VerifyDispatchCalled<TestViewModel1>(query);
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
}
