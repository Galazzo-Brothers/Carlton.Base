using Carlton.Core.Flux.Components;
using Carlton.Core.Flux.Contracts;
using Carlton.Core.Flux.Models;
using Carlton.Core.Flux.Tests.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Polly;
using System.Reflection;
using System.Threading;
namespace Carlton.Core.Flux.Tests.ComponentTests;

public class ConnectedWrapperComponentTests : TestContext
{
    [Theory, AutoData]
    public void ConnectedWrapper_RendersCorrectly(TestViewModel vm)
    {
        //Arrange
        var mockQueryDispatcher = Substitute.For<IViewModelQueryDispatcher<TestState>>();
        var mockCommandDispatcher = Substitute.For<IMutationCommandDispatcher<TestState>>();
        var mockObserver = Substitute.For<IFluxStateObserver<TestState>>();
        var mockLogger = Substitute.For<ILogger<FluxWrapper<TestState, TestViewModel>>>();

        Services.AddSingleton<IConnectedComponent<TestViewModel>>(new DummyConnectedComponent());
        Services.AddSingleton(mockQueryDispatcher);
        Services.AddSingleton(mockCommandDispatcher);
        Services.AddSingleton(mockObserver);
        Services.AddSingleton(mockLogger);

        var expectedMarkup = @$"
            <div class=""vm-props"">
              <span class=""id"">{vm.ID}</span>
              <span class=""name"">{vm.Name}</span>
              <span class=""description"">{vm.Description}</span>
              <button>Command Event Test</button>
            </div>";

        mockQueryDispatcher.Dispatch(Arg.Any<object>(), Arg.Any<ViewModelQueryContext<TestViewModel>>(), Arg.Any<CancellationToken>()).Returns(Task.FromResult(vm));

        // Act
        var cut = RenderComponent<FluxWrapper<TestState, TestViewModel>>();

        // Assert
        cut.MarkupMatches(expectedMarkup);
    }

    [Theory, AutoData]
    public void ConnectedWrapper_InitializesCorrectly(TestViewModel vm)
    {
        //Arrange
        var mockQueryDispatcher = Substitute.For<IViewModelQueryDispatcher<TestState>>();
        var mockCommandDispatcher = Substitute.For<IMutationCommandDispatcher<TestState>>();
        var mockObserver = Substitute.For<IFluxStateObserver<TestState>>();
        var mockLogger = Substitute.For<ILogger<FluxWrapper<TestState, TestViewModel>>>();

        Services.AddSingleton<IConnectedComponent<TestViewModel>>(new DummyConnectedComponent());
        Services.AddSingleton(mockQueryDispatcher);
        Services.AddSingleton(mockCommandDispatcher);
        Services.AddSingleton(mockObserver);
        Services.AddSingleton(mockLogger);

        mockQueryDispatcher.Dispatch(Arg.Any<object>(), Arg.Any<ViewModelQueryContext<TestViewModel>>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(vm));

        // Act
        var cut = RenderComponent<FluxWrapper<TestState, TestViewModel>>();

        // Assert
        mockQueryDispatcher.Received(1).Dispatch(
            Arg.Any<object>(),
            Arg.Any<ViewModelQueryContext<TestViewModel>>(),
            Arg.Any<CancellationToken>());
    }


    [Theory, AutoData]
    public async Task ConnectedWrapper_OnComponentEvent_CallsMutationDispatcher(TestViewModel vm, TestCommand1 command)
    {
        //Arrange
        var mockQueryDispatcher = Substitute.For<IViewModelQueryDispatcher<TestState>>();
        var mockCommandDispatcher = Substitute.For<IMutationCommandDispatcher<TestState>>();
        var mockObserver = Substitute.For<IFluxStateObserver<TestState>>();
        var mockLogger = Substitute.For<ILogger<FluxWrapper<TestState, TestViewModel>>> ();

       
        Services.AddSingleton(mockQueryDispatcher);
        Services.AddSingleton(mockCommandDispatcher);
        Services.AddSingleton(mockObserver);
        Services.AddSingleton(mockLogger);
        Services.AddSingleton(new DummyComponentService(command));
        Services.AddSingleton<IConnectedComponent<TestViewModel>>(_ => new DummyConnectedComponent(_.GetService<DummyComponentService>()));

        mockQueryDispatcher.Dispatch(Arg.Any<object>(), Arg.Any<ViewModelQueryContext<TestViewModel>>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(vm));

        var cut = RenderComponent<FluxWrapper<TestState, TestViewModel>>();
        var buttonElement = cut.Find("button");

        // Act
        buttonElement.Click();

        // Assert
        await mockCommandDispatcher.Received(1).Dispatch(
            Arg.Any<object>(),
            Arg.Is<MutationCommandContext<object>>(_ => _.MutationCommand == command),
            Arg.Any<CancellationToken>());
    }

    //[Fact]
    //public void ConnectedWrapper_ObservableStateEvents_InitializeCorrectly()
    //{
    //    //Arrange
    //    var vm = _fixture.Create<TestViewModel>();
    //    _vmDispatcher.SetupDispatcher(vm);

    //    // Act
    //    var cut = RenderComponent<ConnectedWrapper<TestViewModel, TestState>>();

    //    // Assert
    //    Assert.Collection(cut.Instance.ObservableStateEvents,
    //      evt => Assert.Equal("TestEvent", evt),
    //      evt => Assert.Equal("TestEvent2", evt),
    //      evt => Assert.Equal("TestEvent3", evt)
    //  );  
    //}

    //[Fact]
    //public void ConnectedWrapper_OnStateChangeTestEvent_CallsViewModelDispatcher()
    //{
    //    // Arrange
    //    var vm = _fixture.Create<TestViewModel>();
    //    _vmDispatcher.SetupDispatcher(vm);

    //    RenderComponent<FluxWrapper<TestViewModel, TestState>>();
    //    var times = 2; //Once for the component init and once again for the state change

    //    //Act
    //    _observer.Raise(_ => _.StateChanged += null, "TestEvent");

    //    // Assert
    //    _vmDispatcher.VerifyDispatcher<TestViewModel>(times);
    //}

    //[Theory, AutoData]
    //public void ConnectedWrapper_OnStateChangeNonListeningEvent_DoesNotCallViewModelDispatcher(string stateEventName)
    //{
    //    // Arrange
    //    var vm = _fixture.Create<TestViewModel>();
    //    _vmDispatcher.SetupDispatcher(vm);

    //    RenderComponent<FluxWrapper<TestViewModel, TestState>>();
    //    var times = 1; //One and only time for the component init

    //    //Act
    //    _observer.Raise(_ => _.StateChanged += null, stateEventName);

    //    // Assert
    //    _vmDispatcher.VerifyDispatcher<TestViewModel>(times);
    //}

    //[Fact]
    //public void ConnectedWrapperDisposesCorrectly()
    //{
    //    //Arrange
    //    var vm = _fixture.Create<TestViewModel>();
    //    _vmDispatcher.SetupDispatcher(vm);

    //    RenderComponent<FluxWrapper<TestViewModel, TestState>>();
    //    var times = 1; //One and only time for the component init, event handler removed correctly during dispose
    //    DisposeComponents();

    //    //Act
    //    _observer.Raise(_ => _.StateChanged += null, "TestEvent");

    //    // Assert
    //    _vmDispatcher.VerifyDispatcher<TestViewModel>(times);
    //}

    //[Fact]
    //public void ConnectedWrapper_LoadingContent()
    //{
    //    //Arrange
    //    var vm = _fixture.Create<TestViewModel>();
    //    _vmDispatcher.SetupDispatcher(vm);

    //    var spinnerMarkup = "<span class='spinner'>This is a spinner.</span>";
    //    var renderFragment = new RenderFragment(builder =>
    //    {
    //        builder.AddMarkupContent(0, spinnerMarkup);
    //    });

    //    var propInfo = typeof(FluxWrapper<TestViewModel, TestState>)
    //        .GetProperty("IsLoading");

    //    var cut = RenderComponent<FluxWrapper<TestViewModel, TestState>>(parameters => parameters
    //        .Add(p => p.SpinnerContent, renderFragment));

    //    //Act
    //    propInfo.SetValue(cut.Instance, true);
    //    cut.Render();

    //    //Assert
    //    cut.MarkupMatches(spinnerMarkup);
    //}
}


