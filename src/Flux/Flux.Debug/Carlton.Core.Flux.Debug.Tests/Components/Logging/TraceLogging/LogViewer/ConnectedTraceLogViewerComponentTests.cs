using AutoFixture.Xunit2;
using Bunit;
using Bunit.TestDoubles;
using Carlton.Core.Components.Flux.Debug.Components;
using Carlton.Core.Flux.Debug.Components.Logging.TraceLogging.LogTable;
using Carlton.Core.Flux.Debug.Models.Commands;
using Carlton.Core.Flux.Debug.Models.ViewModels;
using Shouldly;
using Xunit;
namespace Carlton.Core.Flux.Debug.Tests.Components.Logging.TraceLogging.LogViewer;

public class ConnectedTraceLogViewerComponentTests : TestContext
{
	[Theory, AutoData]
	public void ConnectedTraceLogViewer_Markup_RendersCorrectly(TraceLogViewerViewModel expectedViewModel)
	{
		//Arrange
		ComponentFactories.AddStub<TraceLogTable>("<div>TraceLogTable Stub</div>");
		var expectedMarkup = @"<div>TraceLogTable Stub</div>";

		//Act
		var cut = RenderComponent<ConnectedTraceLogViewer>(parameters => parameters
			.Add(p => p.ViewModel, expectedViewModel));

		//Assert
		cut.MarkupMatches(expectedMarkup);
	}

	[Theory, AutoData]
	public void ConnectedTraceLogViewer_ViewModelParameter_RendersCorrectly(TraceLogViewerViewModel expectedViewModel)
	{
		//Arrange
		ComponentFactories.AddStub<TraceLogTable>("<div>TraceLogTable Stub</div>");

		//Act
		var cut = RenderComponent<ConnectedTraceLogViewer>(parameters => parameters
			.Add(p => p.ViewModel, expectedViewModel));

		var stub = cut.FindComponent<Stub<TraceLogTable>>();

		//Assert
		stub.Instance.Parameters.Get(x => x.TraceLogMessageGroups).ShouldBe(expectedViewModel.TraceLogMessages);
		stub.Instance.Parameters.Get(x => x.SelectedTraceLogMessageIndex).ShouldBe(expectedViewModel.SelectedTraceLogMessageIndex);
	}

	[Theory, AutoData]
	public void ConnectedTraceLogViewer_OnSelectedTraceLogMessageChangedParameter_RendersCorrectly(
		TraceLogViewerViewModel expectedViewModel,
		int expectedIndex)
	{
		//Arrange
		var eventFired = false;
		var command = (object)null;
		ComponentFactories.AddStub<TraceLogTable>("<div>TraceLogTable Stub</div>");

		var cut = RenderComponent<ConnectedTraceLogViewer>(parameters => parameters
			.Add(p => p.ViewModel, expectedViewModel)
			.Add(p => p.OnComponentEvent, args =>
			{
				eventFired = true;
				command = args;
			}));

		//ACt
		cut.InvokeAsync(() => cut.FindComponent<Stub<TraceLogTable>>()
		   .Instance.Parameters.Get(x => x.OnSelectedTraceLogMessageChanged)
		   .InvokeAsync(new SelectedTraceLogMessageChangedArgs(expectedIndex)));

		//Assert
		eventFired.ShouldBeTrue();
		command.ShouldBeOfType<ChangeSelectedTraceLogMessageCommand>()
			   .SelectedTraceLogMessageIndex.ShouldBe(expectedIndex);
	}
}
