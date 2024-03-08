using Carlton.Core.Flux.Components;
using Carlton.Core.Flux.Contracts;
using Carlton.Core.Flux.Dispatchers.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;
namespace Carlton.Core.Flux.Tests.Components.FluxWrappers;

public class PassiveWrapperComponentTests : TestContext
{
    private readonly IViewModelQueryDispatcher<TestState> _mockQueryDispatcher;
    private readonly IMutationCommandDispatcher<TestState> _mockCommandDispatcher;
    private readonly IFluxStateObserver<TestState> _mockObserver;
    private readonly ILogger<FluxWrapper<TestState, TestViewModel>> _mockLogger;

    //Arrange
    public PassiveWrapperComponentTests()
    {
        //Create Mocks
        _mockQueryDispatcher = Substitute.For<IViewModelQueryDispatcher<TestState>>();
        _mockCommandDispatcher = Substitute.For<IMutationCommandDispatcher<TestState>>();
        _mockObserver = Substitute.For<IFluxStateObserver<TestState>>();
        _mockLogger = Substitute.For<ILogger<FluxWrapper<TestState, TestViewModel>>>();

        //Container Registrations
        Services.AddSingleton<IConnectedComponent<TestViewModel>>(new DummyConnectedComponent());
        Services.AddSingleton(_mockQueryDispatcher);
        Services.AddSingleton(_mockCommandDispatcher);
        Services.AddSingleton(_mockObserver);
        Services.AddSingleton(_mockLogger);
    }

    [Theory, AutoData]
    public void PassiveWrapper_RendersCorrectly(TestViewModel vm)
    {
        //Arrange
        var expectedMarkup = @$"
            <div class=""vm-props"">
              <span class=""id"">{vm.ID}</span>
              <span class=""name"">{vm.Name}</span>
              <span class=""description"">{vm.Description}</span>
              <button>Command Event Test</button>
            </div>";

        _mockQueryDispatcher.SetupQueryDispatcher(vm);

        // Act
        var cut = RenderComponent<PassiveFluxWrapper<TestState, TestViewModel>>(
            parameters => parameters.Add(p => p.PassiveViewModel, vm));

        // Assert
        cut.MarkupMatches(expectedMarkup);
    }

    [Theory, AutoData]
    public void PassiveWrapper_InitializesCorrectly(TestViewModel vm)
    {
        // Act
        var cut = RenderComponent<PassiveFluxWrapper<TestState, TestViewModel>>(
          parameters => parameters.Add(p => p.PassiveViewModel, vm));

        // Assert
        cut.Instance.ViewModel.ShouldBe(vm);
    }

    [Theory, AutoData]
    public async Task PassiveWrapper_OnComponentEvent_CallsMutationDispatcher(TestViewModel vm, TestCommand1 command)
    {
        //Arrange
        _mockCommandDispatcher.SetupMutationDispatcher<TestCommand1>(command);
        var cut = RenderComponent<PassiveFluxWrapper<TestState, TestViewModel>>(
            parameters => parameters.Add(p => p.PassiveViewModel, vm));
        var wrappedComponent = cut.FindComponent<DummyConnectedComponent>();

        // Act
        await wrappedComponent.InvokeAsync(() => wrappedComponent.Instance.RaiseComponentEvent(command));

        // Assert
        _mockCommandDispatcher.VerifyCommandDispatcher<TestCommand1>(1, command);
    }

    [Theory, AutoData]
    public void PassiveWrapper_ObservableStateEvents_InitializeCorrectly(TestViewModel vm)
    {
        // Act
        var cut = RenderComponent<PassiveFluxWrapper<TestState, TestViewModel>>(
            parameters => parameters.Add(p => p.PassiveViewModel, vm));

        // Assert
        Assert.Collection(cut.Instance.ObservableStateEvents,
          evt => evt.ShouldBe("TestEvent"),
          evt => evt.ShouldBe("TestEvent2"),
          evt => evt.ShouldBe("TestEvent3")
      );
    }

    [Theory, AutoData]
    public void PassiveWrapper_OnStateChangeTestEvent_DoesNotCallViewModelDispatcher(TestViewModel vm)
    {
        // Arrange
        var cut = RenderComponent<PassiveFluxWrapper<TestState, TestViewModel>>(
           parameters => parameters.Add(p => p.PassiveViewModel, vm));

        //Act
        _mockObserver.StateChanged += Raise.Event<Func<FluxStateChangedEventArgs, Task>>(new FluxStateChangedEventArgs("TestEvent"));

        // Assert
        _mockQueryDispatcher.DidNotReceive().Dispatch(
            Arg.Any<object>(),
            Arg.Any<ViewModelQueryContext<TestViewModel>>(),
            Arg.Any<CancellationToken>());
    }

    [Theory, AutoData]
    public void PassiveWrapper_OnStateChangeNonListeningEvent_DoesNotCallViewModelDispatcher(
        TestViewModel vm)
    {
        // Arrange
        var cut = RenderComponent<PassiveFluxWrapper<TestState, TestViewModel>>(
           parameters => parameters.Add(p => p.PassiveViewModel, vm));

        //Act
        _mockObserver.StateChanged += Raise.Event<Func<FluxStateChangedEventArgs, Task>>(new FluxStateChangedEventArgs("Some not relevant event"));

        // Assert
        _mockQueryDispatcher.DidNotReceive().Dispatch(
             Arg.Any<object>(),
             Arg.Any<ViewModelQueryContext<TestViewModel>>(),
             Arg.Any<CancellationToken>());
    }

    [Theory, AutoData]
    public void PassiveWrapper_LoadingContent(TestViewModel vm)
    {
        //Arrange
        var spinnerMarkup = "<span class='spinner'>This is a spinner.</span>";
        var propInfo = typeof(FluxWrapper<TestState, TestViewModel>)
            .GetProperty("IsLoading", BindingFlags.Instance | BindingFlags.NonPublic);

        var cut = RenderComponent<PassiveFluxWrapper<TestState, TestViewModel>>(
          parameters => parameters.Add(p => p.PassiveViewModel, vm)
                                  .Add(p => p.SpinnerContent, spinnerMarkup));

        //Act
        propInfo.SetValue(cut.Instance, true);
        cut.Render();

        //Assert
        cut.MarkupMatches(spinnerMarkup);
    }
}
