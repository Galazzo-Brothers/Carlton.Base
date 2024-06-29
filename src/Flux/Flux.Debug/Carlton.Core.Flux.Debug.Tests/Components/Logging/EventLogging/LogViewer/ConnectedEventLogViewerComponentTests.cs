using AutoFixture.Xunit2;
using Bunit;
using Bunit.TestDoubles;
using Carlton.Core.Flux.Debug.Components.Logging.EventLogging.LogTable;
using Carlton.Core.Flux.Debug.Components.Logging.EventLogging.LogViewer;
using Carlton.Core.Flux.Debug.Models.Commands;
using Carlton.Core.Flux.Debug.Models.ViewModels;
using Shouldly;
using Xunit;

namespace Carlton.Core.Flux.Debug.Tests.Components.Logging.EventLogging.LogViewer;

public class ConnectedEventLogViewerComponentTests : TestContext
{
	[Theory, AutoData]
	public void ConnectedEventLogViewer_Markup_RendersCorrectly(
		EventLogViewerViewModel expectedViewModel)
	{
		//Arrange
		var expectedMarkup = @"<div>EventLogViewer Stub</div>";
		ComponentFactories.AddStub<ViewStateEventLogViewer>(@"<div>EventLogViewer Stub</div>");

		//Act
		var cut = RenderComponent<ConnectedEventLogViewer>(parameters => parameters
			.Add(p => p.ViewModel, expectedViewModel));

		//Assert
		cut.MarkupMatches(expectedMarkup);
	}

	[Theory, AutoData]
	public void ConnectedEventLogViewer_ViewModelParameter_RendersCorrectly(
		EventLogViewerViewModel expectedViewModel)
	{
		//Arrange
		ComponentFactories.AddStub<ViewStateEventLogViewer>(@"<div>EventLogViewer Stub</div>");

		//Act
		var cut = RenderComponent<ConnectedEventLogViewer>(parameters => parameters
			.Add(p => p.ViewModel, expectedViewModel));
		var stub = cut.FindComponent<Stub<ViewStateEventLogViewer>>();

		//Assert
		stub.Instance.Parameters.Get(x => x.LogMessages).ShouldBe(expectedViewModel.LogMessages);
		stub.Instance.Parameters.Get(x => x.SelectedLogMessageIndex).ShouldBe(expectedViewModel.SelectedLogMessageIndex);
	}

	[Theory, AutoData]
	public void ConnectedEventLogViewer_SelectLogEntry_ShouldRaiseEvent(
		EventLogViewerViewModel expectedViewModel,
		int logMessageIndexToSelect)
	{
		//Arrange
		var eventFired = false;
		var command = (object)null;
		ComponentFactories.AddStub<ViewStateEventLogViewer>(@"<div>EventLogViewer Stub</div>");
		var cut = RenderComponent<ConnectedEventLogViewer>(parameters => parameters
			.Add(p => p.ViewModel, expectedViewModel)
			.Add(p => p.OnComponentEvent, args =>
			{
				eventFired = true;
				command = args;
			}));
		var stub = cut.FindComponent<Stub<ViewStateEventLogViewer>>();

		//Act
		cut.InvokeAsync(() => stub.Instance.Parameters.Get(x => x.OnLogMessageSelected).InvokeAsync(
			new SelectedEventLogMessageChangedArgs(logMessageIndexToSelect)));

		//Assert
		eventFired.ShouldBeTrue();
		command.ShouldBeOfType<ChangeSelectedLogMessageCommand>();
	}
}
