using Carlton.Core.Flux.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;
namespace Carlton.Core.Flux.Tests.Components.FluxWrappers;

public class InteropWrapperComponentTests : TestContext
{
	private readonly IViewModelQueryDispatcher<TestState> _mockQueryDispatcher;
	private readonly IMutationCommandDispatcher<TestState> _mockCommandDispatcher;
	private readonly IFluxStateObserver<TestState> _mockObserver;
	private readonly ILogger<InteropFluxWrapper<TestState, TestViewModel>> _mockLogger;

	//Arrange
	public InteropWrapperComponentTests()
	{
		//Create Mocks
		_mockQueryDispatcher = Substitute.For<IViewModelQueryDispatcher<TestState>>();
		_mockCommandDispatcher = Substitute.For<IMutationCommandDispatcher<TestState>>();
		_mockObserver = Substitute.For<IFluxStateObserver<TestState>>();
		_mockLogger = Substitute.For<ILogger<InteropFluxWrapper<TestState, TestViewModel>>>();

		//Container Registrations
		Services.AddSingleton<IConnectedComponent<TestViewModel>>(new DummyConnectedComponent());
		Services.AddSingleton(_mockQueryDispatcher);
		Services.AddSingleton(_mockCommandDispatcher);
		Services.AddSingleton(_mockObserver);
		Services.AddSingleton(_mockLogger);
	}


	[Theory, AutoData]
	public void InteropWrapper_RendersCorrectly(
		string moduleName,
		string functionName,
		object[] jsParameters,
		TestViewModel vm)
	{
		//Arrange
		var moduleInterop = JSInterop.SetupModule(moduleName);
		moduleInterop.Setup<TestViewModel>(functionName, jsParameters).SetResult(vm);
		var expectedMarkup = @$"
            <div class=""vm-props"">
              <span class=""id"">{vm.Id}</span>
              <span class=""name"">{vm.Name}</span>
              <span class=""description"">{vm.Description}</span>
              <button>Command Event Test</button>
            </div>";


		// Act
		var cut = RenderComponent<InteropFluxWrapper<TestState, TestViewModel>>(
			parameters => parameters.Add(p => p.JsModule, moduleName)
				.Add(p => p.JsFunction, functionName)
				.Add(p => p.JsParameters, jsParameters));

		// Assert
		cut.MarkupMatches(expectedMarkup);
	}

	[Theory, AutoData]
	public void InteropWrapper_InitializesCorrectly(
		string moduleName,
		string functionName,
		object[] jsParameters,
		TestViewModel vm)
	{
		//Arrange
		var moduleInterop = JSInterop.SetupModule(moduleName);
		moduleInterop.Setup<TestViewModel>(functionName, jsParameters).SetResult(vm);

		// Act
		var cut = RenderComponent<InteropFluxWrapper<TestState, TestViewModel>>(
			 parameters => parameters.Add(p => p.JsModule, moduleName)
				 .Add(p => p.JsFunction, functionName)
				 .Add(p => p.JsParameters, jsParameters));

		// Assert
		moduleInterop.VerifyInvoke(functionName);
	}

	[Theory, AutoData]
	public async Task InteropWrapper_OnComponentEvent_CallsMutationDispatcher(
		string moduleName,
		string functionName,
		object[] jsParameters,
		TestViewModel vm,
		TestCommand command,
		MutationCommandResult expectedResult)
	{
		//Arrange
		var moduleInterop = JSInterop.SetupModule(moduleName);
		moduleInterop.Setup<TestViewModel>(functionName, jsParameters).SetResult(vm);
		_mockCommandDispatcher.SetupCommandDispatcher(command, expectedResult);
		var cut = RenderComponent<InteropFluxWrapper<TestState, TestViewModel>>(
			 parameters => parameters.Add(p => p.JsModule, moduleName)
				 .Add(p => p.JsFunction, functionName)
				 .Add(p => p.JsParameters, jsParameters));
		var wrappedComponent = cut.FindComponent<DummyConnectedComponent>();

		// Act
		await wrappedComponent.InvokeAsync(() => wrappedComponent.Instance.RaiseComponentEvent(command));

		// Assert
		_mockCommandDispatcher.VerifyCommandDispatcher(1, command);
	}

	[Theory, AutoData]
	public void InteropWrapper_ObservableStateEvents_InitializeCorrectly(
		string moduleName,
		string functionName,
		object[] jsParameters,
		TestViewModel vm)
	{
		//Arrange
		var moduleInterop = JSInterop.SetupModule(moduleName);
		moduleInterop.Setup<TestViewModel>(functionName, jsParameters).SetResult(vm);

		// Act
		var cut = RenderComponent<InteropFluxWrapper<TestState, TestViewModel>>(
			parameters => parameters.Add(p => p.JsModule, moduleName)
				.Add(p => p.JsFunction, functionName)
				.Add(p => p.JsParameters, jsParameters));

		// Assert
		Assert.Collection(cut.Instance.ObservableStateEvents,
		  evt => evt.ShouldBe("TestEvent"),
		  evt => evt.ShouldBe("TestEvent2"),
		  evt => evt.ShouldBe("TestEvent3")
	  );
	}

	[Theory, AutoData]
	public void InteropWrapper_OnStateChangeTestEvent_CallsViewModelJsInterop(
		string moduleName,
		string functionName,
		object[] jsParameters,
		TestViewModel vm)
	{
		// Arrange
		var moduleInterop = JSInterop.SetupModule(moduleName);
		moduleInterop.Setup<TestViewModel>(functionName, jsParameters).SetResult(vm);
		var cut = RenderComponent<InteropFluxWrapper<TestState, TestViewModel>>(
			parameters => parameters.Add(p => p.JsModule, moduleName)
				.Add(p => p.JsFunction, functionName)
				.Add(p => p.JsParameters, jsParameters));
		var expectedTimes = 2; //Once for the component init and once again for the state change

		//Act
		cut.InvokeAsync(() => _mockObserver.StateChanged += Raise.Event<Func<FluxStateChangedEventArgs, Task>>(new FluxStateChangedEventArgs("TestEvent")));
		cut.WaitForState(() => !cut.Instance.IsLoading, TimeSpan.FromSeconds(2));

		// Assert
		moduleInterop.VerifyInvoke(functionName, expectedTimes);
	}

	[Theory, AutoData]
	public void InteropWrapper_OnStateChangeNonListeningEvent_DoesNotCallViewModelJsInterop(
		string moduleName,
		string functionName,
		object[] jsParameters,
		TestViewModel vm)
	{
		// Arrange
		var moduleInterop = JSInterop.SetupModule(moduleName);
		moduleInterop.Setup<TestViewModel>(functionName, jsParameters).SetResult(vm);
		var cut = RenderComponent<InteropFluxWrapper<TestState, TestViewModel>>(
			parameters => parameters.Add(p => p.JsModule, moduleName)
				.Add(p => p.JsFunction, functionName)
				.Add(p => p.JsParameters, jsParameters));
		var expectedTimes = 1; //Only once for the component init

		//Act
		_mockObserver.StateChanged += Raise.Event<Func<FluxStateChangedEventArgs, Task>>(new FluxStateChangedEventArgs("Some not relevant event"));

		// Assert
		moduleInterop.VerifyInvoke(functionName, expectedTimes);
	}

	[Theory, AutoData]
	public void InteropWrapper_DisposesCorrectly(
		string moduleName,
		string functionName,
		object[] jsParameters,
		TestViewModel vm)
	{
		//Arrange
		var moduleInterop = JSInterop.SetupModule(moduleName);
		moduleInterop.Setup<TestViewModel>(functionName, jsParameters).SetResult(vm);
		var cut = RenderComponent<InteropFluxWrapper<TestState, TestViewModel>>(
			parameters => parameters.Add(p => p.JsModule, moduleName)
				.Add(p => p.JsFunction, functionName)
				.Add(p => p.JsParameters, jsParameters));
		var expectedTimes = 1; //One and only time for the component init, event handler removed correctly during dispose
		DisposeComponents();

		//Act
		_mockObserver.StateChanged += Raise.Event<Func<FluxStateChangedEventArgs, Task>>(new FluxStateChangedEventArgs("TestEvent"));

		// Assert
		moduleInterop.VerifyInvoke(functionName, expectedTimes);
	}

	[Theory, AutoData]
	public void InteropWrapper_LoadingContent(
		string moduleName,
		string functionName,
		object[] jsParameters,
		TestViewModel vm)
	{
		//Arrange
		var moduleInterop = JSInterop.SetupModule(moduleName);
		moduleInterop.Setup<TestViewModel>(functionName, jsParameters).SetResult(vm);

		var spinnerMarkup = "<span class='spinner'>This is a spinner.</span>";
		var propInfo = typeof(FluxWrapper<TestState, TestViewModel>)
			.GetProperty("IsLoading", BindingFlags.Instance | BindingFlags.Public);

		var cut = RenderComponent<InteropFluxWrapper<TestState, TestViewModel>>(
		   parameters => parameters.Add(p => p.JsModule, moduleName)
			   .Add(p => p.JsFunction, functionName)
			   .Add(p => p.JsParameters, jsParameters)
			   .Add(p => p.SpinnerContent, spinnerMarkup));

		//Act
		propInfo.SetValue(cut.Instance, true);
		cut.Render();

		//Assert
		cut.MarkupMatches(spinnerMarkup);
	}
}
