using AutoFixture.Xunit2;
using Bunit;
using Carlton.Core.Flux.Debug.Components.StateViewer.Viewer;
using Carlton.Core.Flux.Debug.Models.Common;
using Shouldly;
using Xunit;
namespace Carlton.Core.Flux.Debug.Tests.Components.StateViewer.Viewer;

public class FluxStateViewerTableRowComponentTests : TestContext
{
	[Theory]
	[InlineAutoData(true)]
	[InlineAutoData(false)]
	public void FluxStateViewerTableRow_Markup_RendersCorrectly(
		bool expectedIsSelected,
		RecordedMutation expectedRecordedMutation)
	{
		//Arrange
		var expectedMarkup = @$"
		<div class=""table-row {(expectedIsSelected ? "selected" : string.Empty)}"">
			<div class=""table-cell"">
				<span class=""cell-text"">{expectedRecordedMutation.MutationIndex}</span>
			</div>
			<div class=""table-cell"">
				<span class=""cell-text"">{expectedRecordedMutation.MutationDate.ToString("MM/dd/yyyy HH:mm:ss.fffffff")}</span>
			</div>
			<div class=""table-cell"">
				<span class=""cell-text"">{expectedRecordedMutation.MutationName}</span>
			</div>
			<div class=""table-cell"">
				<span class=""cell-text"">
					<div class=""link-btn""  >
						<span >Select</span>
					</div>
				</span>
			</div>
		</div>";

		//Act
		var cut = RenderComponent<FluxStateViewerTableRow>(parameters => parameters
			.Add(p => p.Mutation, expectedRecordedMutation)
			.Add(p => p.IsSelected, expectedIsSelected));

		//Assert
		cut.MarkupMatches(expectedMarkup);
	}

	[Theory, AutoData]
	public void FluxStateViewerTableRow_MutationParameter_RendersCorrectly(
		bool expectedIsSelected,
		RecordedMutation expectedRecordedMutation)
	{
		//Act
		var cut = RenderComponent<FluxStateViewerTableRow>(parameters => parameters
			.Add(p => p.Mutation, expectedRecordedMutation)
			.Add(p => p.IsSelected, expectedIsSelected));

		var cellTextValues = cut.FindAll(".cell-text");

		//Assert
		cellTextValues[0].TextContent.ShouldBe(expectedRecordedMutation.MutationIndex.ToString());
		cellTextValues[1].TextContent.ShouldBe(expectedRecordedMutation.MutationDate.ToString("MM/dd/yyyy HH:mm:ss.fffffff"));
		cellTextValues[2].TextContent.ShouldBe(expectedRecordedMutation.MutationName);
	}

	[Theory, AutoData]
	public void FluxStateViewerTableRow_IsSelectedParameter_RendersCorrectly(
		bool expectedIsSelected,
		RecordedMutation expectedRecordedMutation)
	{
		//Act
		var cut = RenderComponent<FluxStateViewerTableRow>(parameters => parameters
			.Add(p => p.Mutation, expectedRecordedMutation)
			.Add(p => p.IsSelected, expectedIsSelected));

		var tableRow = cut.Find(".table-row");

		//Assert
		tableRow.ClassList.Contains("selected").ShouldBe(expectedIsSelected);
	}

	[Theory, AutoData]
	public void FluxStateViewerTableRow_OnRecordedMutationSelectedParameter_ShouldFireEvent(
		bool expectedIsSelected,
		RecordedMutation expectedRecordedMutation,
		int expectedIndex)
	{
		//Arrange
		var eventFired = false;
		var actualIndex = -1;

		var cut = RenderComponent<FluxStateViewerTableRow>(parameters => parameters
			.Add(p => p.Mutation, expectedRecordedMutation)
			.Add(p => p.IsSelected, expectedIsSelected)
			.Add(p => p.OnRecordedMutationSelected, args =>
			{
				eventFired = true;
				actualIndex = args.SelectedMutationCommandIndex;
			}));

		//Act
		cut.Instance.OnRecordedMutationSelected.InvokeAsync(
			new SelectedMutationCommandChangedArgs(expectedIndex));


		//Assert
		eventFired.ShouldBeTrue();
		actualIndex.ShouldBe(expectedIndex);
	}
}
