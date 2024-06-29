using AutoFixture.Xunit2;
using Bunit;
using Carlton.Core.Flux.Debug.Components.Logging.EventLogging.LogTable;
using Carlton.Core.Flux.Debug.Models.Common;
using Shouldly;
using Xunit;
namespace Carlton.Core.Flux.Debug.Tests.Components.Logging.EventLogging.LogTable;

public class EventLogTableRowComponentTests : TestContext
{
	[Theory]
	[InlineAutoData(true)]
	[InlineAutoData(false)]
	public void EventLogTableRow_Markup_RendersCorrectly(
		bool isSelected,
		LogMessageSummary logMessage)
	{
		//Arrange
		var expectedMarkup = @$"
		<div class=""table-row {(isSelected ? "selected" : string.Empty)}"">
			<div class=""table-cell"">
				<span class=""cell-text"">{logMessage.Timestamp.ToString("MM/dd/yyyy HH:mm:ss.fffffff")}</span>
			</div>
			<div class=""table-cell"">
				<span class=""cell-text"">{logMessage.Message}</span>
			</div>
			<div class=""table-cell"">
				<div class=""log-level-pill trace"">
					<div class=""pill"">{logMessage.LogLevel.ToString()}</div>
				</div>
			</div>
			<div class=""table-cell"">
				<span class=""cell-text"">{logMessage.EventId.Id}</span>
			</div>
			<div class=""table-cell"">
				<span class=""cell-text"">{logMessage.EventId.Name}</span>
			</div>
		</div>";

		//Act
		var cut = RenderComponent<EventLogTableRow>(parameters => parameters
			.Add(p => p.LogEntry, logMessage)
			.Add(p => p.IsSelected, isSelected));

		//Assert
		cut.MarkupMatches(expectedMarkup);
	}

	[Theory, AutoData]
	public void EventLogTableRow_LogEntryParameter_RendersCorrectly(
		bool isSelected,
		LogMessageSummary logMessage)
	{
		//Act
		var cut = RenderComponent<EventLogTableRow>(parameters => parameters
			.Add(p => p.LogEntry, logMessage)
			.Add(p => p.IsSelected, isSelected));

		var cellText = cut.FindAll(".cell-text");
		var pill = cut.Find(".pill");

		//Assert
		cellText[0].TextContent.ShouldBe(logMessage.Timestamp.ToString("MM/dd/yyyy HH:mm:ss.fffffff"));
		cellText[1].TextContent.ShouldBe(logMessage.Message);
		cellText[2].TextContent.ShouldBe(logMessage.EventId.Id.ToString());
		cellText[3].TextContent.ShouldBe(logMessage.EventId.Name);
		pill.TextContent.ShouldBe(logMessage.LogLevel.ToString());
	}

	[Theory]
	[InlineAutoData(true)]
	[InlineAutoData(false)]
	public void EventLogTableRow_IsSelectedParameter_RendersCorrectly(
		bool isSelected,
		LogMessageSummary logMessage)
	{
		//Act
		var cut = RenderComponent<EventLogTableRow>(parameters => parameters
			.Add(p => p.LogEntry, logMessage)
			.Add(p => p.IsSelected, isSelected));

		var hasSelectedClass = cut.Find(".table-row").ClassList.Contains("selected");

		//Assert
		hasSelectedClass.ShouldBe(isSelected);
	}

	[Theory, AutoData]
	public void EventLogTableRow_OnLogMessageSelectedParameter_ShouldFireEvents(
		bool isSelected,
		LogMessageSummary logMessage)
	{
		//Arrange
		var eventFired = false;
		var selectedIndex = -1;
		var cut = RenderComponent<EventLogTableRow>(parameters => parameters
			.Add(p => p.LogEntry, logMessage)
			.Add(p => p.IsSelected, isSelected)
			.Add(p => p.OnLogMessageSelected, args =>
			{
				eventFired = true;
				selectedIndex = args;
			}));

		//Act
		cut.Find(".table-row").Click();

		//Assert
		eventFired.ShouldBeTrue();
		selectedIndex.ShouldBe(logMessage.Id);
	}
}
