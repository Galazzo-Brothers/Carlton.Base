using AutoFixture.Xunit2;
using Bunit;
using Bunit.TestDoubles;
using Carlton.Core.Components.Tables;
using Carlton.Core.Flux.Debug.Components.Logging.TraceLogging.LogTable;
using Carlton.Core.Flux.Debug.Models.Common;
using Carlton.Core.Foundation.Web.ViewState;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;
namespace Carlton.Core.Flux.Debug.Tests.Components.Logging.TraceLogging.LogTable;

public class TraceLogTableComponentTests : TestContext
{
	[Theory, AutoData]
	public void TraceLogTable_Markup_RendersCorrectly(
		IEnumerable<TraceLogMessageGroup> expectedLogMessageGroups,
		int expectedSelectedMessageIndex)
	{
		//Arrange
		ComponentFactories.AddStub<ViewStateTable<TraceLogMessageGroup>>("<div>TraceLogMessageGroup Stub</div>");
		var expectedMarkup = @$"
		<div class=""trace-log-table"" >
			<div>TraceLogMessageGroup Stub</div>
		</div>";

		//Act
		var cut = RenderComponent<TraceLogTable>(parameters => parameters
			.Add(p => p.TraceLogMessageGroups, expectedLogMessageGroups)
			.Add(p => p.SelectedTraceLogMessageIndex, expectedSelectedMessageIndex));

		//Assert
		cut.MarkupMatches(expectedMarkup);
	}

	[Theory, AutoData]
	public void TraceLogTable_TraceLogMessageGroupsParameter_RendersCorrectly(
		IEnumerable<TraceLogMessageGroup> expectedLogMessageGroups,
		int expectedSelectedMessageIndex)
	{
		//Arrange
		ComponentFactories.AddStub<ViewStateTable<TraceLogMessageGroup>>("<div>TraceLogMessageGroup Stub</div>");

		//Act
		var cut = RenderComponent<TraceLogTable>(parameters => parameters
			.Add(p => p.TraceLogMessageGroups, expectedLogMessageGroups)
			.Add(p => p.SelectedTraceLogMessageIndex, expectedSelectedMessageIndex));

		var stub = cut.FindComponent<Stub<ViewStateTable<TraceLogMessageGroup>>>();

		//Assert
		stub.Instance.Parameters.Get(x => x.Items).ShouldBe(expectedLogMessageGroups);
	}

	[Theory, AutoData]
	public void TraceLogTable_SelectedLogMessageIndexParameter_RendersCorrectly(
		IEnumerable<TraceLogMessageGroup> expectedLogMessageGroups,
		int expectedSelectedMessageIndex)
	{
		//Arrange
		Services.AddSingleton<IViewStateService, ViewStateService>();
		ComponentFactories.AddStub<ViewStateTraceLogTableRow>("<div>TraceLogMessageGroup Stub</div>");

		//Act
		var cut = RenderComponent<TraceLogTable>(parameters => parameters
			.Add(p => p.TraceLogMessageGroups, expectedLogMessageGroups)
			.Add(p => p.SelectedTraceLogMessageIndex, expectedSelectedMessageIndex));

		var stub = cut.FindComponent<Stub<ViewStateTraceLogTableRow>>();

		//Assert
		stub.Instance.Parameters.Get(x => x.SelectedTraceLogMessageIndex).ShouldBe(expectedSelectedMessageIndex);
	}

	[Theory, AutoData]
	public void TraceLogTable_OnSelectedTraceLogMessageChangedParameter_ShouldFireEvent(
		IEnumerable<TraceLogMessageGroup> expectedLogMessageGroups,
		int expectedSelectedMessageIndex)
	{
		//Arrange
		var eventFired = false;
		var eventIndex = -1;
		Services.AddSingleton<IViewStateService, ViewStateService>();
		ComponentFactories.AddStub<ViewStateTraceLogTableRow>("<div>TraceLogMessageGroup Stub</div>");

		var cut = RenderComponent<TraceLogTable>(parameters => parameters
			.Add(p => p.TraceLogMessageGroups, expectedLogMessageGroups)
			.Add(p => p.SelectedTraceLogMessageIndex, -1)
			.Add(p => p.OnSelectedTraceLogMessageChanged, args =>
			{
				eventFired = true;
				eventIndex = args.SelectedTraceLogMessageIndex;
			}));

		var stub = cut.FindComponent<Stub<ViewStateTraceLogTableRow>>();

		//Act
		stub.Instance.Parameters.Get(x => x.OnSelectedTraceLogMessageChanged).InvokeAsync(new SelectedTraceLogMessageChangedArgs(expectedSelectedMessageIndex));

		//Assert
		eventFired.ShouldBeTrue();
		eventIndex.ShouldBe(expectedSelectedMessageIndex);
	}

	[Theory]
	[InlineAutoData(true)]
	[InlineAutoData(false)]
	public void TraceLogTable_OnTraceLogMessageExpansionChangedParameter_ShouldFireEvent(
		bool expectedIsExpanded,
		IEnumerable<TraceLogMessageGroup> expectedLogMessageGroups,
		int expectedSelectedMessageIndex)
	{
		//Arrange
		var eventFired = false;
		var eventIsExpanded = !expectedIsExpanded;
		Services.AddSingleton<IViewStateService, ViewStateService>();
		ComponentFactories.AddStub<ViewStateTraceLogTableRow>("<div>TraceLogMessageGroup Stub</div>");

		var cut = RenderComponent<TraceLogTable>(parameters => parameters
			.Add(p => p.TraceLogMessageGroups, expectedLogMessageGroups)
			.Add(p => p.SelectedTraceLogMessageIndex, expectedSelectedMessageIndex)
			.Add(p => p.OnTraceLogMessageExpansionChanged, args =>
			{
				eventFired = true;
				eventIsExpanded = args.IsExpanded;
			}));

		var stub = cut.FindComponent<Stub<ViewStateTraceLogTableRow>>();

		//Act
		stub.Instance.Parameters.Get(x => x.OnTraceLogMessageExpansionChanged).InvokeAsync(new TraceLogMessageExpansionChangedArgs(null, expectedIsExpanded));

		//Assert
		eventFired.ShouldBeTrue();
		expectedIsExpanded.ShouldBe(expectedIsExpanded);
	}
}
