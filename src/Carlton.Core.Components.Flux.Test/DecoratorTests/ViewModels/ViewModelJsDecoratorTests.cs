using AutoFixture.AutoMoq;
using Carlton.Core.Components.Flux.Contracts;
using Carlton.Core.Components.Flux.Decorators.Queries;
using Carlton.Core.Components.Flux.Models;
using Carlton.Core.Components.Flux.Test.Common;
using Carlton.Core.Components.Flux.Test.Common.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using System.Reflection;

namespace Carlton.Core.Components.Flux.Test.DecoratorTests.ViewModels;

public class ViewModelJsDecoratorTests
{
    private readonly IFixture _fixture;
    private readonly Mock<IViewModelQueryDispatcher<TestState>> _decorated;
    private readonly Mock<IJSRuntime> _js;
    private readonly Mock<IJSObjectReference> _jsObject;
    private readonly Mock<IMutableFluxState<TestState>> _state;
    private readonly Mock<ILogger<ViewModelJsDecorator<TestState>>> _logger;

    public ViewModelJsDecoratorTests()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
        _decorated = _fixture.Freeze<Mock<IViewModelQueryDispatcher<TestState>>>();
        _js = _fixture.Freeze<Mock<IJSRuntime>>();
        _jsObject = _fixture.Freeze<Mock<IJSObjectReference>>();
        _state = _fixture.Freeze<Mock<IMutableFluxState<TestState>>>();
        _logger = _fixture.Freeze<Mock<ILogger<ViewModelJsDecorator<TestState>>>>();
        _js.SetupIJSRuntime("test_module", _jsObject.Object);
    }

    [Theory]
    [MemberData(nameof(TestDataGenerator.GetViewModelData), MemberType = typeof(TestDataGenerator))]
    public async Task Dispatch_DispatchAndJsModuleAndStateMutationCalled_And_AssertViewModels<TViewModel>(TViewModel expectedViewModel)
    {
        //Arrange
        var sender = new JsRefreshCaller();
        var query = new ViewModelQuery();
        var attribute = sender.GetType().GetCustomAttribute<ViewModelJsInteropRefreshAttribute>();
        var sut = _fixture.Create<ViewModelJsDecorator<TestState>>();

        _decorated.SetupDispatcher(expectedViewModel);
        _jsObject.SetupIJSObjectReference(attribute.Function, attribute.Parameters, expectedViewModel);

        //Act 
        var actualViewModel = await sut.Dispatch<TViewModel>(sender, query, CancellationToken.None);

        //Assert
        _js.VerifyJSRuntime(1);
        _decorated.VerifyDispatch<TViewModel>(query);
        _jsObject.VerifyJSObjectReference<TViewModel>(attribute.Function, attribute.Parameters);
        _state.VerifyStateMutation(expectedViewModel, 1);
        Assert.Equal(expectedViewModel, actualViewModel);
    }

    [Theory]
    [MemberData(nameof(TestDataGenerator.GetJsCallersData), MemberType = typeof(TestDataGenerator))]
    public async Task Dispatch_JsObjectCalled(object jsCaller)
    {
        //Arrange
        var expectedViewModel = _fixture.Create<TestViewModel1>();
        var attribute = jsCaller.GetType().GetCustomAttribute<ViewModelJsInteropRefreshAttribute>();
        var query = new ViewModelQuery();
        var sut = _fixture.Create<ViewModelJsDecorator<TestState>>();

        _decorated.SetupDispatcher(expectedViewModel);
        _jsObject.SetupIJSObjectReference(attribute.Function, attribute.Parameters, expectedViewModel);

        //Act 
        await sut.Dispatch<TestViewModel1>(jsCaller, query, CancellationToken.None);

        //Assert
        _jsObject.VerifyJSObjectReference<TestViewModel1>(attribute.Function, attribute.Parameters);
    }

    [Fact]
    public async Task Dispatch_JsModuleNotCalled()
    {
        //Arrange
        var expectedViewModel = _fixture.Create<TestViewModel1>();
        var sender = new NoRefreshCaller();
        var query = new ViewModelQuery();
        var sut = _fixture.Create<ViewModelJsDecorator<TestState>>();
       
        _decorated.SetupDispatcher(expectedViewModel);

        //Act 
        await sut.Dispatch<TestViewModel1>(sender, query, CancellationToken.None);

        //Assert
        _js.VerifyJSRuntime(0);
        _decorated.VerifyDispatch<TestViewModel1>(query);
    }
}
