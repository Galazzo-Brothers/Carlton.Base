using Carlton.Core.Lab.Components.ComponentViewer;
namespace Carlton.Core.Lab.Test.Components.ComponentViewer;

public class ConnectedComponentViewerComponentTests : TestContext
{
	[Theory, AutoData]
	public void ConnectedComponentViewer_Markup_RendersCorrectly(
		TestParameters expectedParameters)
	{
		//Arrange
		var vm = new ComponentViewerViewModel
		{
			ComponentType = typeof(DummyComponent),
			ComponentParameters = expectedParameters
		};

		//Act
		var cut = RenderComponent<ConnectedComponentViewer>(parameters => parameters
				.Add(p => p.ViewModel, vm));

		//Assert
		cut.MarkupMatches(@$"
<div class=""dynamic-event-capturing-wrapper"">
	<div class=""view-model-test-wrapper"">
		<span class=""view-model-id"">{expectedParameters.SomeNumber}</span>
		<span class=""view-model-text"">{expectedParameters.SomeText}</span>
		<span class=""view-model-boolean"">{expectedParameters.SomeBoolean}</span>
		<button class=""event-callback-test"">EventCallback Test</button>
		<button class=""generic-event-callback-test"">EventCallback Test</button>
	</div>
</div>");
	}

	[Theory, AutoData]
	public void ConnectedComponentViewer_OnEventRecorded_RaisesComponentEvent(
		TestParameters expectedParameters)
	{
		//Arrange
		var onComponentEventCalled = false;
		var recordEventCommand = (RecordEventCommand)null;

		var vm = new ComponentViewerViewModel
		{
			ComponentType = typeof(DummyComponent),
			ComponentParameters = expectedParameters
		};

		var cut = RenderComponent<ConnectedComponentViewer>(parameters => parameters
				.Add(p => p.ViewModel, vm)
				.Add(p => p.OnComponentEvent, cmd =>
				{
					recordEventCommand = (RecordEventCommand)cmd;
					onComponentEventCalled = true;
				}));
		var btn = cut.Find(".event-callback-test");

		//Act
		btn.Click();

		//Assert
		onComponentEventCalled.ShouldBeTrue();
		recordEventCommand.ShouldNotBeNull();
		recordEventCommand.RecordedEventName.ShouldBe(nameof(DummyComponent.TestCallback));
		recordEventCommand.EventArgs.ShouldBeOfType<object>();
	}

	[Theory, AutoData]
	public void ConnectedComponentViewer_OnGenericEventRecorded_RaisesComponentEvent(
		TestParameters expectedParameters,
		TestArgs expectedArgs)
	{
		//Arrange
		var onComponentEventCalled = false;
		var recordEventCommand = (RecordEventCommand)null;

		var vm = new ComponentViewerViewModel
		{
			ComponentType = typeof(DummyComponent),
			ComponentParameters = expectedParameters
		};

		var cut = RenderComponent<ConnectedComponentViewer>(parameters => parameters
				.Add(p => p.ViewModel, vm)
				.Add(p => p.OnComponentEvent, cmd =>
				{
					recordEventCommand = (RecordEventCommand)cmd;
					onComponentEventCalled = true;
				}));


		cut.Render();
		var btn = cut.Find(".generic-event-callback-test");
		cut.FindComponent<DummyComponent>().Instance.CallbackTypedArgs = expectedArgs;

		//Act
		btn.Click();

		//Assert
		onComponentEventCalled.ShouldBeTrue();
		recordEventCommand.ShouldNotBeNull();
		recordEventCommand.RecordedEventName.ShouldBe(nameof(DummyComponent.TestGenericCallback));
		recordEventCommand.EventArgs.ShouldBeOfType<TestArgs>();
		recordEventCommand.EventArgs.ShouldBeSameAs(expectedArgs);
	}
}
