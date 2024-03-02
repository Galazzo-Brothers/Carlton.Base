using Carlton.Core.Flux.Attributes;
using Carlton.Core.Flux.Components;
using Carlton.Core.Flux.Contracts;
using Carlton.Core.Flux.Models;
using Carlton.Core.Flux.Tests.Common;
using Castle.Core.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System.Reflection;
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

        Services.AddSingleton<IConnectedComponent<TestViewModel>>(new DummyConnectedComponent());
        Services.AddSingleton(mockQueryDispatcher);
        Services.AddSingleton(mockCommandDispatcher);
        Services.AddSingleton(mockObserver);
        Services.AddSingleton(mockLogger);
      
        mockQueryDispatcher.Dispatch(Arg.Any<object>(), Arg.Any<ViewModelQueryContext<TestViewModel>>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(vm));

        var cut = RenderComponent<FluxWrapper<TestState, TestViewModel>>();
        var wrappedComponent = cut.FindComponent<DummyConnectedComponent>();

        // Act
        await wrappedComponent.InvokeAsync(() => wrappedComponent.Instance.RaiseComponentEvent(command));

        // Assert
        await mockCommandDispatcher.Received(1).Dispatch(
            Arg.Any<object>(),
            Arg.Is<MutationCommandContext<object>>(_ => _.MutationCommand == command),
            Arg.Any<CancellationToken>());
    }

    [Theory, AutoData]
    public void ConnectedWrapper_ObservableStateEvents_InitializeCorrectly(TestViewModel vm)
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
        Assert.Collection(cut.Instance.ObservableStateEvents,
          evt => evt.ShouldBe("TestEvent"),
          evt => evt.ShouldBe("TestEvent2"),
          evt => evt.ShouldBe("TestEvent3")
      );
    }

    [Theory, AutoData]
    public void ConnectedWrapper_OnStateChangeTestEvent_CallsViewModelDispatcher(
        TestViewModel vm)
    {
        // Arrange
        var mockQueryDispatcher = Substitute.For<IViewModelQueryDispatcher<TestState>>();
        var mockCommandDispatcher = Substitute.For<IMutationCommandDispatcher<TestState>>();
        var mockObserver = Substitute.For<IFluxStateObserver<TestState>>();
        var mockLogger = Substitute.For<ILogger<FluxWrapper<TestViewModel, TestState>>>();

        Services.AddSingleton<IConnectedComponent<TestViewModel>>(new DummyConnectedComponent());
        Services.AddSingleton(mockQueryDispatcher);
        Services.AddSingleton(mockCommandDispatcher);
        Services.AddSingleton(mockObserver);
        Services.AddSingleton(mockLogger);

        mockQueryDispatcher.Dispatch(Arg.Any<object>(), Arg.Any<ViewModelQueryContext<TestViewModel>>(), Arg.Any<CancellationToken>())
           .Returns(Task.FromResult(vm));

        RenderComponent<FluxWrapper<TestState, TestViewModel>>();
        var expectedTimes = 2; //Once for the component init and once again for the state change

        //Act
        mockObserver.StateChanged += Raise.Event<Func<FluxStateChangedEventArgs, Task>>(new FluxStateChangedEventArgs("TestEvent"));

        // Assert
        mockQueryDispatcher.Received(expectedTimes).Dispatch(
            Arg.Any<object>(),
            Arg.Any<ViewModelQueryContext<TestViewModel>>(),
            Arg.Any<CancellationToken>());
    }

    [Theory, AutoData]
    public void ConnectedWrapper_OnStateChangeNonListeningEvent_DoesNotCallViewModelDispatcher(
        TestViewModel vm)
    {
        // Arrange
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

        RenderComponent<FluxWrapper<TestState, TestViewModel>>();
        var expectedTimes = 1; //One and only time for the component init

        //Act
        mockObserver.StateChanged += Raise.Event<Func<FluxStateChangedEventArgs, Task>>(new FluxStateChangedEventArgs("Some not relevant event"));

        // Assert
        mockQueryDispatcher.Received(expectedTimes).Dispatch(
            Arg.Any<object>(),
            Arg.Any<ViewModelQueryContext<TestViewModel>>(),
            Arg.Any<CancellationToken>());
    }

    [Theory, AutoData]
    public void ConnectedWrapper_DisposesCorrectly(TestViewModel vm)
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

        mockQueryDispatcher.Dispatch(
            Arg.Any<object>(),
            Arg.Any<ViewModelQueryContext<TestViewModel>>(),
            Arg.Any<CancellationToken>())
           .Returns(Task.FromResult(vm));

        RenderComponent<FluxWrapper<TestState, TestViewModel>>();
        var expectedTimes = 1; //One and only time for the component init, event handler removed correctly during dispose
        DisposeComponents();

        //Act
        mockObserver.StateChanged += Raise.Event<Func<FluxStateChangedEventArgs, Task>>(new FluxStateChangedEventArgs("TestEvent"));

        // Assert
        mockQueryDispatcher.Received(expectedTimes).Dispatch(
           Arg.Any<object>(),
           Arg.Any<ViewModelQueryContext<TestViewModel>>(),
           Arg.Any<CancellationToken>());
    }

    [Theory, AutoData]
    public void ConnectedWrapper_LoadingContent(TestViewModel vm)
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

        mockQueryDispatcher.Dispatch(
           Arg.Any<object>(),
           Arg.Any<ViewModelQueryContext<TestViewModel>>(),
           Arg.Any<CancellationToken>())
          .Returns(Task.FromResult(vm));

        var spinnerMarkup = "<span class='spinner'>This is a spinner.</span>";
        var propInfo = typeof(FluxWrapper<TestState, TestViewModel>)
            .GetProperty("IsLoading", BindingFlags.Instance | BindingFlags.NonPublic);

        var cut = RenderComponent<FluxWrapper<TestState, TestViewModel>>(parameters => parameters
            .Add(p => p.SpinnerContent, spinnerMarkup));

        //Act
        propInfo.SetValue(cut.Instance, true);
        cut.Render();

        //Assert
        cut.MarkupMatches(spinnerMarkup);
    }
}


