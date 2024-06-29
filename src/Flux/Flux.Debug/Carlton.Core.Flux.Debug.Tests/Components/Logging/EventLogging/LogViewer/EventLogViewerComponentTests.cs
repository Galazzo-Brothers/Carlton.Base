using AutoFixture.Xunit2;
using Bunit;
using Bunit.TestDoubles;
using Carlton.Core.Flux.Debug.Components.Logging.EventLogging;
using Carlton.Core.Flux.Debug.Components.Logging.EventLogging.LogTable;
using Carlton.Core.Flux.Debug.Components.Logging.EventLogging.LogViewer;
using Carlton.Core.Flux.Debug.Models.Common;
using Carlton.Core.LayoutServices.ViewState;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;
namespace Carlton.Core.Flux.Debug.Tests.Components.Logging.EventLogging.LogViewer;

public class EventLogViewerComponentTests : TestContext
{
	public EventLogViewerComponentTests()
	{
		Services.AddSingleton<IViewStateService, ViewStateService>();
		ComponentFactories.AddStub<EventLogLevelFilter>("<div>Log Level Filters Stub</div>");
		ComponentFactories.AddStub<EventLogTextFilter>("<div>Log Text Filter Stub</div>");
		ComponentFactories.AddStub<EventLogTable>("<div>Log Table Stub</div>");
	}

	[Theory, AutoData]
	public void EventLogViewer_Markup_RendersCorrectly(
		IEnumerable<LogMessageSummary> expectedLogMessages,
		int selectedIndex,
		EventLogViewerFilterState expectedFilterState)
	{
		//Arrange
		var expectedMarkup = @"
		<div class=""log-viewer"">
			<div class=""log-filters"">
				<div>Log Level Filters Stub</div>
				<div>Log Level Filters Stub</div>
				<div>Log Level Filters Stub</div>
				<div>Log Level Filters Stub</div>
				<div>Log Level Filters Stub</div>
				<div>Log Level Filters Stub</div>
				<div>Log Text Filter Stub</div>
			</div>
			<div>Log Table Stub</div>
		</div>";

		//Act
		var cut = RenderComponent<EventLogViewer>(parameters => parameters
			.Add(p => p.LogMessages, expectedLogMessages.ToList())
			.Add(p => p.SelectedLogMessageIndex, selectedIndex)
			.Add(p => p.EventLogViewerFilterState, expectedFilterState)
		);

		//Assert
		cut.MarkupMatches(expectedMarkup);
	}

	[Theory, AutoData]
	public void EventLogViewer_LogMessagesParameter_RendersCorrectly(
		IEnumerable<LogMessageSummary> expectedLogMessages,
		int selectedIndex,
		EventLogViewerFilterState expectedFilterState)
	{
		//Arrange
		var expectedFilteredMessages = expectedLogMessages.Where(x => expectedFilterState.IncludedLogLevels.Contains(x.LogLevel));

		//Act
		var cut = RenderComponent<EventLogViewer>(parameters => parameters
			.Add(p => p.LogMessages, expectedLogMessages.ToList())
			.Add(p => p.SelectedLogMessageIndex, selectedIndex)
			.Add(p => p.EventLogViewerFilterState, expectedFilterState)
		);

		var displayedMessages = cut.FindComponent<Stub<EventLogTable>>().Instance.Parameters.Get(x => x.LogMessages);

		//Assert
		displayedMessages.ShouldBe(expectedFilteredMessages);
	}

	[Theory, AutoData]
	public void EventLogViewer_SelectedLogMessageIndexParameter_RendersCorrectly(
		IEnumerable<LogMessageSummary> expectedLogMessages,
		int expectedSelectedIndex,
		EventLogViewerFilterState expectedFilterState)
	{
		//Act
		var cut = RenderComponent<EventLogViewer>(parameters => parameters
			.Add(p => p.LogMessages, expectedLogMessages.ToList())
			.Add(p => p.SelectedLogMessageIndex, expectedSelectedIndex)
			.Add(p => p.EventLogViewerFilterState, expectedFilterState)
		);

		var actualIndex = cut.FindComponent<Stub<EventLogTable>>().Instance.Parameters.Get(x => x.SelectedLogMessageIndex);

		//Assert
		actualIndex.ShouldBe(expectedSelectedIndex);
	}

	[Theory, AutoData]
	public void EventLogViewer_EventLogViewerFileterStateParameter_RendersCorrectly(
		IEnumerable<LogMessageSummary> expectedLogMessages,
		int expectedSelectedIndex,
		EventLogViewerFilterState expectedFilterState)
	{
		//Act
		var cut = RenderComponent<EventLogViewer>(parameters => parameters
			.Add(p => p.LogMessages, expectedLogMessages.ToList())
			.Add(p => p.SelectedLogMessageIndex, expectedSelectedIndex)
			.Add(p => p.EventLogViewerFilterState, expectedFilterState)
		);

		var textFilterComponent = cut.FindComponent<Stub<EventLogTextFilter>>();
		var levelFilterComponents = cut.FindComponents<Stub<EventLogLevelFilter>>();

		//Assert
		textFilterComponent.Instance.Parameters.Get(x => x.Text).ShouldBe(expectedFilterState.FilterText);
		levelFilterComponents.ToList().ForEach(x =>
		{
			var currentLevel = x.Instance.Parameters.Get(p => p.LogLevel);
			var expectedCount = expectedLogMessages.Count(x => x.LogLevel == currentLevel);
			var actualCount = x.Instance.Parameters.Get(p => p.Count);
			actualCount.ShouldBe(expectedCount);
		});
	}

	[Theory, AutoData]
	public void EventLogViewer_OnLogMessageSelectedParameter_ShouldFireEvents(
		IEnumerable<LogMessageSummary> expectedLogMessages,
		int expectedSelectedIndex,
		EventLogViewerFilterState expectedFilterState,
		int expectedArgValue)
	{
		//Arrange
		var eventFired = false;
		var eventValue = -1;

		//Act
		var cut = RenderComponent<EventLogViewer>(parameters => parameters
			.Add(p => p.LogMessages, expectedLogMessages.ToList())
			.Add(p => p.SelectedLogMessageIndex, expectedSelectedIndex)
			.Add(p => p.EventLogViewerFilterState, expectedFilterState)
			.Add(p => p.OnLogMessageSelected, args =>
			{
				eventFired = true;
				eventValue = args.SelectedLogMessageIndex;
			}));

		var tableComponent = cut.FindComponent<Stub<EventLogTable>>();
		cut.InvokeAsync(() => tableComponent.Instance.Parameters.Get(x => x.OnLogMessageSelected).InvokeAsync(expectedArgValue));

		//Assert
		eventFired.ShouldBeTrue();
		eventValue.ShouldBe(expectedArgValue);
	}
}



