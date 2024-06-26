using AutoFixture.Xunit2;
using Bunit;
using Bunit.TestDoubles;
using Carlton.Core.Components.Tables;
using Carlton.Core.Flux.Debug.Components.Logging.EventLogging.LogTable;
using Carlton.Core.Flux.Debug.Models.Common;
using Carlton.Core.Foundation.Web.ViewState;
using Carlton.Core.Utilities.Random;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;
namespace Carlton.Core.Flux.Debug.Tests.Components.Logging.EventLogging.LogTable;

public class EventLogTableComponentTests : TestContext
{
	public EventLogTableComponentTests()
	{
		Services.AddSingleton<IViewStateService, ViewStateService>();
	}

	[Theory, AutoData]
	public void EventLogTable_Markup_RendersCorrectly(
		IList<LogMessageSummary> expectedLogs,
		int selectedId)
	{
		//Arrange
		ComponentFactories.AddStub<ViewStateTable<LogMessageSummary>>("<div>Log Table Stub</div>");

		//Act
		var cut = RenderComponent<EventLogTable>(parameters => parameters
			.Add(p => p.LogMessages, expectedLogs)
			.Add(p => p.SelectedLogMessageIndex, selectedId));


		//Assert
		cut.MarkupMatches(@"<div class=""log-table""><div>Log Table Stub</div></div>");
	}

	[Theory, AutoData]
	public void EventLogTable_LogMessagesParameter_RendersCorrectly(
		IList<LogMessageSummary> expectedLogs,
		int selectedId)
	{
		//Arrange
		ComponentFactories.AddStub<ViewStateTable<LogMessageSummary>>("<div>Log Table Stub</div>");

		//Act
		var cut = RenderComponent<EventLogTable>(parameters => parameters
			.Add(p => p.LogMessages, expectedLogs.ToList())
			.Add(p => p.SelectedLogMessageIndex, selectedId));

		var table = cut.FindComponent<Stub<ViewStateTable<LogMessageSummary>>>();
		var actualLogs = table.Instance.Parameters.Get(x => x.Items);

		//Assert
		actualLogs.ShouldBe(expectedLogs);
	}

	[Theory, AutoData]
	public void EventLogTable_SelectedLogMessageIndexParameter_RendersCorrectly(
		IList<LogMessageSummary> expectedLogs)
	{
		//Arrange
		ComponentFactories.AddStub<EventLogTableRow>("<div>Log Table Row Stub</div>");
		var indexToSelect = RandomUtilities.GetRandomIndex(expectedLogs.Count);
		var idToSelect = expectedLogs.ElementAt(indexToSelect).Id;

		//Act
		var cut = RenderComponent<EventLogTable>(parameters => parameters
			.Add(p => p.LogMessages, expectedLogs)
			.Add(p => p.SelectedLogMessageIndex, idToSelect));

		var rows = cut.FindComponents<Stub<EventLogTableRow>>();
		var selectedRow = rows.First(x => x.Instance.Parameters.Get(p => p.LogEntry).Id == idToSelect);
		var otherRows = rows.Where(x => x != selectedRow);

		//Assert
		selectedRow.Instance.Parameters.Get(x => x.IsSelected).ShouldBeTrue();
		otherRows.ShouldAllBe(x => x.Instance.Parameters.Get(p => p.IsSelected) == false);
	}

	[Theory, AutoData]
	public void EventLogTable_OnLogMessageSelectedParameter_ShouldFireEvents(
		IList<LogMessageSummary> expectedLogs,
		int expectedSelectedId)
	{
		//Arrange
		ComponentFactories.AddStub<EventLogTableRow>("<div>Log Table Row Stub</div>");
		var indexToSelect = RandomUtilities.GetRandomIndex(expectedLogs.Count);
		var eventFired = false;
		var eventSelectedIndex = -1;

		var cut = RenderComponent<EventLogTable>(parameters => parameters
			.Add(p => p.LogMessages, expectedLogs)
			.Add(p => p.SelectedLogMessageIndex, -1)
			.Add(p => p.OnLogMessageSelected, args =>
			{
				eventFired = true;
				eventSelectedIndex = args;
			}));

		var rows = cut.FindComponents<Stub<EventLogTableRow>>();
		var selectedRow = rows.ElementAt(indexToSelect);
		var otherRows = rows.Where(x => x != selectedRow);

		//Act
		selectedRow.Instance.Parameters.Get(x => x.OnLogMessageSelected).InvokeAsync(expectedSelectedId);

		//Assert
		eventFired.ShouldBeTrue();
		eventSelectedIndex.ShouldBe(expectedSelectedId);
	}
}
