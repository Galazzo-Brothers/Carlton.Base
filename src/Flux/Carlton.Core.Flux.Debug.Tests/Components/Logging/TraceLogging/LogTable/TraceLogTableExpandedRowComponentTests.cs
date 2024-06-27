using AutoFixture.Xunit2;
using Bunit;
using Carlton.Core.Components.Buttons;
using Carlton.Core.Flux.Debug.Components.Logging.TraceLogging.LogTable;
using Carlton.Core.Flux.Debug.Models.Common;
using Carlton.Core.Utilities.Random;
using Shouldly;
using Xunit;
namespace Carlton.Core.Flux.Debug.Tests.Components.Logging.TraceLogging.LogTable;

public class TraceLogTableExpandedRowComponentTests : TestContext
{
	[Theory]
	[InlineAutoData(true)]
	[InlineAutoData(false)]
	public void TraceLogTableExpandableRow_Markup_RendersCorrectly(
		bool expectedIsExpanded,
		TraceLogMessageGroup expectedTraceLogMessageGroup)
	{
		//Arrange
		var selectedIndex = RandomUtilities.GetRandomIndex(expectedTraceLogMessageGroup.ChildEntries.Count() + 1);
		var selectedId = selectedIndex == 0 ? expectedTraceLogMessageGroup.ParentEntry.Id
			: expectedTraceLogMessageGroup.ChildEntries.ElementAt(selectedIndex - 1).Id;
		var expectedMarkup = BuildExpectedMarkup(expectedTraceLogMessageGroup, expectedIsExpanded, selectedId);

		//Act
		var cut = RenderComponent<TraceLogTableExpandableRow>(parameters => parameters
			.Add(p => p.TraceLogMessageGroup, expectedTraceLogMessageGroup)
			.Add(p => p.SelectedTraceLogMessageIndex, selectedId)
			.Add(p => p.IsExpanded, expectedIsExpanded));

		//Assert
		cut.MarkupMatches(expectedMarkup);
	}

	[Theory, AutoData]
	public void TraceLogTableExpandableRow_TraceLogMessageGroupParameter_RendersCorrectly(
		bool expectedIsExpanded,
		TraceLogMessageGroup expectedTraceLogMessageGroup)
	{
		//Act
		var cut = RenderComponent<TraceLogTableExpandableRow>(parameters => parameters
			.Add(p => p.TraceLogMessageGroup, expectedTraceLogMessageGroup)
			.Add(p => p.SelectedTraceLogMessageIndex, -1)
			.Add(p => p.IsExpanded, expectedIsExpanded));

		var cells = cut.FindAll(".starting-row .table-cell");
		var pills = cut.FindComponents<TraceLoggingOutcomePill>();
		var childRows = cut.FindAll(".child-row");

		//Assert
		cells[1].TextContent.ShouldBe(expectedTraceLogMessageGroup.ParentEntry.Timestamp.ToString("MM/dd/yyyy HH:mm:ss.fffffff"));
		cells[2].TextContent.ShouldBe(expectedTraceLogMessageGroup.ParentEntry.FluxAction.ToString());
		cells[3].TextContent.ShouldBe(expectedTraceLogMessageGroup.ParentEntry.TypeDisplayName);
		cells[4].TextContent.ShouldBe(expectedTraceLogMessageGroup.ParentEntry.EventId.Id.ToString());
		pills[0].Instance.RequestSucceeded.ShouldBe(expectedTraceLogMessageGroup.ParentEntry.RequestSucceeded);
		childRows.Count.ShouldBe(expectedTraceLogMessageGroup.ChildEntries.Count());

		Assert.All(childRows, (row, i) =>
		{
			var childCells = childRows[i].Children;
			childCells[1].TextContent.ShouldBe(expectedTraceLogMessageGroup.ChildEntries.ElementAt(i).Timestamp.ToString("MM/dd/yyyy HH:mm:ss.fffffff"));
			childCells[2].TextContent.ShouldBe(expectedTraceLogMessageGroup.ChildEntries.ElementAt(i).FluxAction.ToString());
			childCells[3].TextContent.ShouldBe(expectedTraceLogMessageGroup.ChildEntries.ElementAt(i).TypeDisplayName);
			childCells[4].TextContent.ShouldBe(expectedTraceLogMessageGroup.ChildEntries.ElementAt(i).EventId.Id.ToString());
			pills[i + 1].Instance.RequestSucceeded.ShouldBe(expectedTraceLogMessageGroup.ChildEntries.ElementAt(i).RequestSucceeded);
		});
	}

	[Theory, AutoData]
	public void TraceLogTableExpandableRow_TraceLogMessageIndexParameter_RendersCorrectly(
		bool expectedIsExpanded,
		TraceLogMessageGroup expectedTraceLogMessageGroup)
	{
		//Arrange
		var selectedIndex = RandomUtilities.GetRandomIndex(expectedTraceLogMessageGroup.ChildEntries.Count() + 1);
		var selectedId = selectedIndex == 0 ? expectedTraceLogMessageGroup.ParentEntry.Id
			: expectedTraceLogMessageGroup.ChildEntries.ElementAt(selectedIndex - 1).Id;

		var allEntries = new List<TraceLogMessage> { expectedTraceLogMessageGroup.ParentEntry }.Union(expectedTraceLogMessageGroup.ChildEntries);
		var selectedEntry = allEntries.ElementAt(selectedIndex);

		//Act
		var cut = RenderComponent<TraceLogTableExpandableRow>(parameters => parameters
			.Add(p => p.TraceLogMessageGroup, expectedTraceLogMessageGroup)
			.Add(p => p.SelectedTraceLogMessageIndex, selectedId)
			.Add(p => p.IsExpanded, expectedIsExpanded));

		var selectedCells = cut.FindAll(".selected .table-cell");

		//Assert
		selectedCells[1].TextContent.ShouldBe(selectedEntry.Timestamp.ToString("MM/dd/yyyy HH:mm:ss.fffffff"));
		selectedCells[2].TextContent.ShouldBe(selectedEntry.FluxAction.ToString());
		selectedCells[3].TextContent.ShouldBe(selectedEntry.TypeDisplayName);
		selectedCells[4].TextContent.ShouldBe(selectedEntry.EventId.Id.ToString());
	}

	[Theory]
	[InlineAutoData(true)]
	[InlineAutoData(false)]
	public void TraceLogTableExpandableRow_IsExpandedParameter_RendersCorrectly(
		bool expectedIsExpanded,
		TraceLogMessageGroup expectedTraceLogMessageGroup)
	{
		//Arrange
		var expectedCount = expectedIsExpanded ? 0 : expectedTraceLogMessageGroup.ChildEntries.Count();

		//Act
		var cut = RenderComponent<TraceLogTableExpandableRow>(parameters => parameters
			.Add(p => p.TraceLogMessageGroup, expectedTraceLogMessageGroup)
			.Add(p => p.SelectedTraceLogMessageIndex, -1)
			.Add(p => p.IsExpanded, expectedIsExpanded));

		var collapsedRows = cut.FindAll(".child-row.collapsed");

		//Assert
		collapsedRows.Count.ShouldBe(expectedCount);
	}

	[Theory, AutoData]
	public void TraceLogTableExpandableRow_OnSelectedTraceLogMessageChangedParameter_ShouldFireEvent(
		TraceLogMessageGroup expectedTraceLogMessageGroup)
	{
		//Arrange
		var eventFired = false;
		var actualSelectedIndex = -1;
		var itemToSelect = RandomUtilities.GetRandomNonZeroIndex(expectedTraceLogMessageGroup.ChildEntries.Count() + 1);
		var allEntries = new List<TraceLogMessage> { expectedTraceLogMessageGroup.ParentEntry }.Union(expectedTraceLogMessageGroup.ChildEntries);

		var cut = RenderComponent<TraceLogTableExpandableRow>(parameters => parameters
			.Add(p => p.TraceLogMessageGroup, expectedTraceLogMessageGroup)
			.Add(p => p.SelectedTraceLogMessageIndex, -1)
			.Add(p => p.IsExpanded, true)
			.Add(p => p.OnSelectedTraceLogMessageChanged, args =>
			{
				eventFired = true;
				actualSelectedIndex = args.SelectedTraceLogMessageIndex;
			}));

		var collapsedRows = cut.FindAll(".child-row.collapsed");

		//Act
		cut.InvokeAsync(() => cut.FindComponents<LinkButton>().ElementAt(itemToSelect).Instance.OnClick.InvokeAsync());

		//Assert
		eventFired.ShouldBeTrue();
		actualSelectedIndex.ShouldBe(allEntries.ElementAt(itemToSelect).Id);
	}

	[Theory]
	[InlineAutoData(true)]
	[InlineAutoData(false)]
	public void TraceLogTableExpandableRow_OnTraceLogMessageExpansionChangedParameter_ShouldFireEvent(
		bool expectedIsExpanded,
		TraceLogMessageGroup expectedTraceLogMessageGroup)
	{
		//Arrange
		var eventFired = false;
		var actualIsExpanded = false;
		var itemToSelect = RandomUtilities.GetRandomNonZeroIndex(expectedTraceLogMessageGroup.ChildEntries.Count() + 1);
		var allEntries = new List<TraceLogMessage> { expectedTraceLogMessageGroup.ParentEntry }.Union(expectedTraceLogMessageGroup.ChildEntries);

		var cut = RenderComponent<TraceLogTableExpandableRow>(parameters => parameters
			.Add(p => p.TraceLogMessageGroup, expectedTraceLogMessageGroup)
			.Add(p => p.SelectedTraceLogMessageIndex, -1)
			.Add(p => p.IsExpanded, expectedIsExpanded)
			.Add(p => p.OnTraceLogMessageExpansionChanged, args =>
			{
				eventFired = true;
				actualIsExpanded = args.IsExpanded;
			}));

		//Act
		cut.Find(".chevron").Click();

		//Assert
		eventFired.ShouldBeTrue();
		actualIsExpanded.ShouldBe(!expectedIsExpanded);
	}

	private static string BuildExpectedMarkup(TraceLogMessageGroup traceLogMessageGroup, bool isExpanded, int selectedId)
	{
		return @$"
		<div class=""starting-row table-row {(traceLogMessageGroup.ParentEntry.Id == selectedId ? "selected" : string.Empty)}"">
			<div class=""table-cell chevron-cell"">
				<span class=""chevron mdi mdi-24px {(isExpanded ? " mdi-chevron-down" : "mdi-chevron-right")}""></span>
			</div>
			<div class=""table-cell"">
				<span class=""cell-text"">{traceLogMessageGroup.ParentEntry.Timestamp.ToString("MM/dd/yyyy HH:mm:ss.fffffff")}</span>
			</div>
			<div class=""table-cell"">
				<span class=""cell-text"">{traceLogMessageGroup.ParentEntry.FluxAction}</span>
			</div>
			<div class=""table-cell"">
				<span class=""cell-text"">{traceLogMessageGroup.ParentEntry.TypeDisplayName}</span>
			</div>
			<div class=""table-cell"">
				<span class=""cell-text"">{traceLogMessageGroup.ParentEntry.EventId.Id}</span>
			</div>
			<div class=""table-cell"">
				<span class=""cell-text"">
					<div class=""log-outcome-pill {(traceLogMessageGroup.ParentEntry.RequestSucceeded ? "succeeded" : "failed")}"">
						<div class=""pill"">{(traceLogMessageGroup.ParentEntry.RequestSucceeded ? "Success" : "Error")}</div>
					</div> 
				</span>
			</div>
			<div class=""table-cell"">
				<span class=""cell-text"">
					<div class=""link-btn"">
						<span>View Details</span>
					</div>
				</span>
			</div>
		</div>
		{BuildExpectedRowMarkup(traceLogMessageGroup.ChildEntries, isExpanded, selectedId)}";
	}

	public static string BuildExpectedRowMarkup(IEnumerable<TraceLogMessage> childEntries, bool isExpanded, int selectedId)
	{
		return string.Join(Environment.NewLine, childEntries.Select(log => @$"
		<div class=""child-row table-row {(isExpanded ? string.Empty : "collapsed")} {(log.Id == selectedId ? "selected" : string.Empty)}"">
			<div class=""table-cell""></div>
				<div class=""table-cell"">
					<span class=""cell-text"">{log.Timestamp:MM/dd/yyyy HH:mm:ss.fffffff}</span>
				</div>
				<div class=""table-cell"">
					<span class=""cell-text"">{log.FluxAction}</span>
				</div>
				<div class=""table-cell"">
					<span class=""cell-text"">{log.TypeDisplayName}</span>
				</div>
				<div class=""table-cell"">
					<span class=""cell-text"">{log.EventId.Id}</span>
				</div>
				<div class=""table-cell"">
					<span class=""cell-text"">
						<div class=""log-outcome-pill {(log.RequestSucceeded ? "succeeded" : "failed")}"">
							<div class=""pill"">{(log.RequestSucceeded ? "Success" : "Error")}</div>
						</div>
					</span>
				</div>
				<div class=""table-cell"">
					<span class=""cell-text"">
						<div class=""link-btn"">
							<span>View Details</span>
						</div>
					</span>
			</div>
		</div>"));
	}
}
