using AutoFixture.Xunit2;
using Bunit;
using Bunit.TestDoubles;
using Carlton.Core.Flux.Debug.Components.StateViewer.Viewer;
using Carlton.Core.Flux.Debug.Models.Common;
using Carlton.Core.Layouts.Components;
using Carlton.Core.LayoutServices.ViewState;
using Carlton.Core.Utilities.Random;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;
namespace Carlton.Core.Flux.Debug.Tests.Components.StateViewer.Viewer;

public class FluxStateViewerTableComponentTests : TestContext
{
	[Theory, AutoData]
	public void FluxStateViewerTable_Markup_RendersCorrectly(
		IEnumerable<RecordedMutation> expectedRecordedMutations)
	{
		//Arrange
		ComponentFactories.AddStub<ViewStateTable<RecordedMutation>>("<div>ViewStateTable Stub</div>");
		var expectedIndex = RandomUtilities.GetRandomIndex(expectedRecordedMutations.Count());
		var expectedMarkup = @$"<div>ViewStateTable Stub</div>";

		//Act
		var cut = RenderComponent<FluxStateViewerTable>(parameters => parameters
			.Add(p => p.RecordedMutations, expectedRecordedMutations)
			.Add(p => p.SelectedMutationIndex, expectedIndex));

		//Assert
		cut.MarkupMatches(expectedMarkup);
	}

	[Theory, AutoData]
	public void FluxStateViewerTable_RecordedMutationsParameter_RendersCorrectly(
		IEnumerable<RecordedMutation> expectedRecordedMutations)
	{
		//Arrange
		ComponentFactories.AddStub<ViewStateTable<RecordedMutation>>("<div>ViewStateTable Stub</div>");
		var expectedIndex = RandomUtilities.GetRandomIndex(expectedRecordedMutations.Count());

		//Act
		var cut = RenderComponent<FluxStateViewerTable>(parameters => parameters
			.Add(p => p.RecordedMutations, expectedRecordedMutations)
			.Add(p => p.SelectedMutationIndex, expectedIndex));

		var stub = cut.FindComponent<Stub<ViewStateTable<RecordedMutation>>>();

		//Assert
		stub.Instance.Parameters.Get(x => x.Items).ShouldBe(expectedRecordedMutations);
	}

	[Theory, AutoData]
	public void FluxStateViewerTable_OnRecordedMutationParameter_ShouldFireEvents(
		IEnumerable<RecordedMutation> expectedRecordedMutations)
	{
		//Arrange
		Services.AddSingleton<IViewStateService, ViewStateService>();
		var eventFired = false;
		var eventIndex = -1;
		var expectedIndex = RandomUtilities.GetRandomIndex(expectedRecordedMutations.Count());

		var cut = RenderComponent<FluxStateViewerTable>(parameters => parameters
			.Add(p => p.RecordedMutations, expectedRecordedMutations)
			.Add(p => p.SelectedMutationIndex, expectedIndex)
			.Add(p => p.OnRecordedMutationSelected, args =>
			{
				eventFired = true;
				eventIndex = args.SelectedMutationCommandIndex;
			}));

		//Act
		cut.FindComponents<FluxStateViewerTableRow>()
		   .ElementAt(expectedIndex).Instance
		   .OnRecordedMutationSelected.InvokeAsync(new SelectedMutationCommandChangedArgs(expectedIndex));

		//Assert
		eventFired.ShouldBeTrue();
		eventIndex.ShouldBe(expectedIndex);
	}
}
