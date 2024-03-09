using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Carlton.Core.Flux.Components;
using Carlton.Core.Flux.Contracts;
namespace Carlton.Core.Flux.Tests.Components.FluxWrappers;

public class ConnectedWrapperComponentTests : TestContext
{
	private readonly IViewModelQueryDispatcher<TestState> _mockQueryDispatcher;
	private readonly IMutationCommandDispatcher<TestState> _mockCommandDispatcher;
	private readonly IFluxStateObserver<TestState> _mockObserver;
	private readonly ILogger<FluxWrapper<TestState, TestViewModel>> _mockLogger;

	//Arrange
	public ConnectedWrapperComponentTests()
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
	public void ConnectedWrapper_RendersCorrectly(TestViewModel vm)
	{
		//Arrange
		var expectedMarkup = @$"
            <div class=""vm-props"">
              <span class=""id"">{vm.Id}</span>
              <span class=""name"">{vm.Name}</span>
              <span class=""description"">{vm.Description}</span>
              <button>Command Event Test</button>
            </div>";

		_mockQueryDispatcher.SetupQueryDispatcher(vm);

		// Act
		var cut = RenderComponent<FluxWrapper<TestState, TestViewModel>>();

		// Assert
		cut.MarkupMatches(expectedMarkup);
	}

	[Theory, AutoData]
	public void ConnectedWrapper_InitializesCorrectly(TestViewModel vm)
	{
		//Arrange
		_mockQueryDispatcher.SetupQueryDispatcher(vm);

		// Act
		var cut = RenderComponent<FluxWrapper<TestState, TestViewModel>>();

		// Assert
		_mockQueryDispatcher.VerifyQueryDispatcher<TestViewModel>(1);
	}

	[Theory, AutoData]
	public async Task ConnectedWrapper_OnComponentEvent_CallsMutationDispatcher(TestViewModel vm, TestCommand1 command)
	{
		//Arrange
		_mockQueryDispatcher.SetupQueryDispatcher(vm);
		_mockCommandDispatcher.SetupMutationDispatcher<TestCommand1>(command);
		var cut = RenderComponent<FluxWrapper<TestState, TestViewModel>>();
		var wrappedComponent = cut.FindComponent<DummyConnectedComponent>();

		// Act
		await wrappedComponent.InvokeAsync(() => wrappedComponent.Instance.RaiseComponentEvent(command));

		// Assert
		_mockCommandDispatcher.VerifyCommandDispatcher<TestCommand1>(1, command);
	}

	[Theory, AutoData]
	public void ConnectedWrapper_ObservableStateEvents_InitializeCorrectly(TestViewModel vm)
	{
		//Arrange
		_mockQueryDispatcher.SetupQueryDispatcher(vm);

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
	public void ConnectedWrapper_OnStateChangeTestEvent_CallsViewModelDispatcher(TestViewModel vm)
	{
		// Arrange
		_mockQueryDispatcher.SetupQueryDispatcher(vm);
		RenderComponent<FluxWrapper<TestState, TestViewModel>>();
		var expectedTimes = 2; //Once for the component init and once again for the state change

		//Act
		_mockObserver.StateChanged += Raise.Event<Func<FluxStateChangedEventArgs, Task>>(new FluxStateChangedEventArgs("TestEvent"));

		// Assert
		_mockQueryDispatcher.VerifyQueryDispatcher<TestViewModel>(expectedTimes);
	}

	[Theory, AutoData]
	public void ConnectedWrapper_OnStateChangeNonListeningEvent_DoesNotCallViewModelDispatcher(TestViewModel vm)
	{
		// Arrange
		_mockQueryDispatcher.SetupQueryDispatcher(vm);
		RenderComponent<FluxWrapper<TestState, TestViewModel>>();
		var expectedTimes = 1; //One and only time for the component init

		//Act
		_mockObserver.StateChanged += Raise.Event<Func<FluxStateChangedEventArgs, Task>>(new FluxStateChangedEventArgs("Some not relevant event"));

		// Assert
		_mockQueryDispatcher.VerifyQueryDispatcher<TestViewModel>(expectedTimes);
	}

	[Theory, AutoData]
	public void ConnectedWrapper_DisposesCorrectly(TestViewModel vm)
	{
		//Arrange
		_mockQueryDispatcher.SetupQueryDispatcher(vm);
		RenderComponent<FluxWrapper<TestState, TestViewModel>>();
		var expectedTimes = 1; //One and only time for the component init, event handler removed correctly during dispose
		DisposeComponents();

		//Act
		_mockObserver.StateChanged += Raise.Event<Func<FluxStateChangedEventArgs, Task>>(new FluxStateChangedEventArgs("TestEvent"));

		// Assert
		_mockQueryDispatcher.VerifyQueryDispatcher<TestViewModel>(expectedTimes);
	}

	[Theory, AutoData]
	public void ConnectedWrapper_LoadingContent(TestViewModel vm)
	{
		//Arrange
		_mockQueryDispatcher.SetupQueryDispatcher(vm);

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


