using Carlton.Core.Components.Flux.Contracts;
using Carlton.Core.Components.Flux.Decorators.Queries;
using Carlton.Core.Components.Flux.Models;
using Carlton.Core.Components.Flux.Test.Common;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using Moq;
using System.Reflection;

namespace Carlton.Core.Components.Flux.Test.DecoratorTests.ViewModels;

public class ViewModelJsDecoratorTests
{
    private readonly Mock<IViewModelQueryDispatcher<TestState>> _decorated = new();
    private readonly Mock<IJSRuntime> _js = new();
    private readonly Mock<IJSObjectReference> _jsObject = new();
    private readonly Mock<IMutableFluxState<TestState>> _state = new();
    private readonly Mock<ILogger<ViewModelJsDecorator<TestState>>> _logger = new();
    private readonly ViewModelJsDecorator<TestState> _dispatcher;

    public ViewModelJsDecoratorTests()
    {
        _js.SetupIJsRuntime("test_module", _jsObject.Object);
        _dispatcher = new ViewModelJsDecorator<TestState>(_decorated.Object, _js.Object, _state.Object, _logger.Object);
    }

    [Theory]
    [MemberData(nameof(TestDataGenerator.GetViewModelData), MemberType = typeof(TestDataGenerator))]
    public async Task Dispatch_DispatchAndJsModuleCalled<TViewModel>(TViewModel vm)
    {
        //Arrange
        var sender = new JsRefreshCaller();
        var query = new ViewModelQuery();

        //Act 
        await _dispatcher.Dispatch<TViewModel>(sender, query, CancellationToken.None);

        //Assert
        _js.VerifyJsRuntime(1);
        _decorated.VerifyDispatch<TViewModel>(query);
    }

    [Theory]
    [MemberData(nameof(TestDataGenerator.GetJsCallersData), MemberType = typeof(TestDataGenerator))]
    public async Task Dispatch_JsObjectCalled(object jsCaller)
    {
        //Arrange
        var attribute = jsCaller.GetType().GetCustomAttribute<ViewModelJsInteropRefreshAttribute>();
        var query = new ViewModelQuery();

        //Act 
        await _dispatcher.Dispatch<TestViewModel1>(jsCaller, query, CancellationToken.None);

        //Assert
        _jsObject.VerifyJsObjectReference<TestViewModel1>(attribute.Function, attribute.Parameters);
    }

    [Fact]
    public async Task Dispatch_JsModuleNotCalled()
    {
        //Arrange
        var sender = new NoRefreshCaller();
        var query = new ViewModelQuery();

        //Act 
        await _dispatcher.Dispatch<TestViewModel1>(sender, query, CancellationToken.None);

        //Assert
        _js.VerifyJsRuntime(0);
        _decorated.VerifyDispatch<TestViewModel1>(query);
    }

    [Theory]
    [MemberData(nameof(TestDataGenerator.GetViewModelData), MemberType = typeof(TestDataGenerator))]
    public async Task Dispatch_AssertViewModels<TViewModel>(TViewModel expectedViewModel)
    {
        //Arrange
        var sender = new object();
        var query = new ViewModelQuery();
        _decorated.SetupDispatcher(expectedViewModel);

        //Act 
        var actualViewModel = await _dispatcher.Dispatch<TViewModel>(sender, query, CancellationToken.None);

        //Assert
        Assert.Equal(expectedViewModel, actualViewModel);
    }
}
