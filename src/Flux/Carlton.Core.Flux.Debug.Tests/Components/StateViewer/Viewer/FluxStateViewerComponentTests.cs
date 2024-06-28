using AutoFixture;
using AutoFixture.Xunit2;
using Bunit;
using Bunit.TestDoubles;
using Carlton.Core.Components.Buttons;
using Carlton.Core.Components.Consoles;
using Carlton.Core.Components.Modals;
using Carlton.Core.Components.Toggles;
using Carlton.Core.Flux.Debug.Components.StateViewer.Viewer;
using Carlton.Core.Flux.Debug.Models.Common;
using Carlton.Core.LayoutServices.Modals;
using Carlton.Core.LayoutServices.Toasts;
using Carlton.Core.Utilities.Random;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Shouldly;
using System.Reflection;
using Xunit;

namespace Carlton.Core.Flux.Debug.Tests.Components.StateViewer.Viewer;

public class FluxStateViewerComponentTests : TestContext
{
	private readonly IToastState _toastState;
	private readonly IModalState _modalState;

	public FluxStateViewerComponentTests()
	{
		//Arrange
		_toastState = Substitute.For<IToastState>();
		_modalState = Substitute.For<IModalState>();
		Services.AddSingleton(_toastState);
		Services.AddSingleton(_modalState);

		ComponentFactories.AddStub<ToggleSelect<int>>("<div>ToggleSelect Stub</div>");
		ComponentFactories.AddStub<IconButton>("<div>IconButton Stub</div>");
		ComponentFactories.AddStub<JsonViewerConsole>("<div>JsonViewerConsole Stub</div>");
		ComponentFactories.AddStub<FluxStateViewerTable>("<div>FluxStateViewerTable Stub</div>");
	}

	[Theory]
	[InlineAutoData(ToggleSelectOption.FirstOption)]
	[InlineAutoData(ToggleSelectOption.SecondOption)]
	public void FluxStateViewer_Markup_RendersCorrectly(
		ToggleSelectOption expectedToggleOption,
		IEnumerable<RecordedMutation> expectedRecordedMutations,
		object expectedFluxState)
	{
		//Arrange
		var expectedIndex = RandomUtilities.GetRandomIndex(expectedRecordedMutations.Count());
		var expectedMarkup = @$"
			<div class=""flux-state-viewer"" >
			  <span class=""selected-mutation-label"" >Selected State: {(expectedIndex == 1 ? "Initial State" : $"Mutation {expectedIndex + 1}")}</span>
			  <div class=""flux-state-actions"" >
				<div>ToggleSelect Stub</div>
				<div class=""flux-state-action-btns"" >
				  <div>IconButton Stub</div>
				  <div>IconButton Stub</div>
				  <div>IconButton Stub</div>
				</div>
			  </div>
			 {(expectedToggleOption == ToggleSelectOption.FirstOption ?
				@"<div>JsonViewerConsole Stub</div>" :
				@"<div>FluxStateViewerTable Stub</div>")}
			</div>";

		//Act
		var cut = RenderComponent<FluxStateViewer>(parameters => parameters
			.Add(p => p.RecordedMutations, expectedRecordedMutations)
			.Add(p => p.SelectedFluxState, expectedFluxState)
			.Add(p => p.SelectedToggleOption, expectedToggleOption)
			.Add(p => p.SelectedMutationIndex, expectedIndex));

		//Assert
		cut.MarkupMatches(expectedMarkup);
	}

	[Theory]
	[InlineAutoData(ToggleSelectOption.SecondOption)]
	public void FluxStateViewer_RecordedMutationsParameter_RendersCorrectly(
		ToggleSelectOption expectedToggleOption,
		IEnumerable<RecordedMutation> expectedRecordedMutations,
		object expectedFluxState)
	{
		//Arrange
		var expectedIndex = RandomUtilities.GetRandomIndex(expectedRecordedMutations.Count());

		//Act
		var cut = RenderComponent<FluxStateViewer>(parameters => parameters
			.Add(p => p.RecordedMutations, expectedRecordedMutations)
			.Add(p => p.SelectedFluxState, expectedFluxState)
			.Add(p => p.SelectedToggleOption, expectedToggleOption)
			.Add(p => p.SelectedMutationIndex, expectedIndex));

		var stub = cut.FindComponent<Stub<FluxStateViewerTable>>();

		//Assert
		stub.Instance.Parameters.Get(x => x.RecordedMutations).ShouldBe(expectedRecordedMutations);
	}

	[Theory]
	[InlineAutoData(ToggleSelectOption.FirstOption, 0, true)]
	[InlineAutoData(ToggleSelectOption.FirstOption, 1, true)]
	[InlineAutoData(ToggleSelectOption.FirstOption, 2, false)]
	[InlineAutoData(ToggleSelectOption.SecondOption, 0, true)]
	[InlineAutoData(ToggleSelectOption.SecondOption, 1, true)]
	[InlineAutoData(ToggleSelectOption.FirstOption, 2, false)]
	public void FluxStateViewer_SelectedIndexParameter_RendersCorrectly(
		ToggleSelectOption expectedToggleOption,
		int expectedIndex,
		bool isSubmitDisabled,
		IFixture fixture,
		object expectedFluxState)
	{
		//Arrange
		var expectedRecordedMutations = fixture.CreateMany<RecordedMutation>(3);

		//Act
		var cut = RenderComponent<FluxStateViewer>(parameters => parameters
			.Add(p => p.RecordedMutations, expectedRecordedMutations)
			.Add(p => p.SelectedFluxState, expectedFluxState)
			.Add(p => p.SelectedToggleOption, expectedToggleOption)
			.Add(p => p.SelectedMutationIndex, expectedIndex));

		var btnStub = cut.FindComponents<Stub<IconButton>>()[2];
		var tableStub = cut.FindComponents<Stub<FluxStateViewerTable>>().FirstOrDefault();

		//Assert
		btnStub.Instance.Parameters.Get(x => x.IsDisabled).ShouldBe(isSubmitDisabled);
		tableStub?.Instance.Parameters.Get(x => x.SelectedMutationIndex).ShouldBe(expectedIndex);
	}

	[Theory, AutoData]
	public void FluxStateViewer_SelectedFluxStateParameter_RendersCorrectly(
		IEnumerable<RecordedMutation> expectedRecordedMutations,
		object expectedFluxState)
	{
		//Act
		var cut = RenderComponent<FluxStateViewer>(parameters => parameters
			.Add(p => p.RecordedMutations, expectedRecordedMutations)
			.Add(p => p.SelectedFluxState, expectedFluxState)
			.Add(p => p.SelectedToggleOption, ToggleSelectOption.FirstOption)
			.Add(p => p.SelectedMutationIndex, 0));

		var stub = cut.FindComponent<Stub<JsonViewerConsole>>();

		//Assert
		stub.Instance.Parameters.Get(x => x.Object).ShouldBeSameAs(expectedFluxState);
	}

	[Theory]
	[InlineAutoData(ToggleSelectOption.FirstOption)]
	public void FluxStateViewer_SelectedToggleOptionParameter_FirstOption_RendersCorrectly(
	ToggleSelectOption expectedToggleOption,
	IEnumerable<RecordedMutation> expectedRecordedMutations,
	object expectedFluxState)
	{
		//Act
		var cut = RenderComponent<FluxStateViewer>(parameters => parameters
			.Add(p => p.RecordedMutations, expectedRecordedMutations)
			.Add(p => p.SelectedFluxState, expectedFluxState)
			.Add(p => p.SelectedToggleOption, expectedToggleOption)
			.Add(p => p.SelectedMutationIndex, 0));

		//Assert
		var stub = cut.FindComponent<Stub<JsonViewerConsole>>();
	}

	[Theory]
	[InlineAutoData(ToggleSelectOption.SecondOption)]
	public void FluxStateViewer_SelectedToggleOptionParameter_SecondOption_RendersCorrectly(
	ToggleSelectOption expectedToggleOption,
	IEnumerable<RecordedMutation> expectedRecordedMutations,
	object expectedFluxState)
	{
		//Act
		var cut = RenderComponent<FluxStateViewer>(parameters => parameters
			.Add(p => p.RecordedMutations, expectedRecordedMutations)
			.Add(p => p.SelectedFluxState, expectedFluxState)
			.Add(p => p.SelectedToggleOption, expectedToggleOption)
			.Add(p => p.SelectedMutationIndex, 0));

		//Assert
		var stub = cut.FindComponent<Stub<FluxStateViewerTable>>();
	}

	[Theory, AutoData]
	public void FluxStateViewer_OnRecordedMutationSelectedParameter_ShouldFireEvent(
	IEnumerable<RecordedMutation> expectedRecordedMutations,
	object expectedFluxState,
	int indexToSelect)
	{
		//Arrange
		var eventFired = false;
		var eventIndex = -1;
		var cut = RenderComponent<FluxStateViewer>(parameters => parameters
			.Add(p => p.RecordedMutations, expectedRecordedMutations)
			.Add(p => p.SelectedFluxState, expectedFluxState)
			.Add(p => p.SelectedToggleOption, ToggleSelectOption.SecondOption)
			.Add(p => p.SelectedMutationIndex, 0)
			.Add(p => p.OnRecordedMutationSelected, args =>
			{
				eventFired = true;
				eventIndex = args.SelectedMutationCommandIndex;
			}));

		var stub = cut.FindComponent<Stub<FluxStateViewerTable>>();

		//Act
		stub.Instance.Parameters.Get(x => x.OnRecordedMutationSelected)
			.InvokeAsync(new SelectedMutationCommandChangedArgs(indexToSelect));

		//Assert
		eventFired.ShouldBeTrue();
		eventIndex.ShouldBe(indexToSelect);
	}

	[Theory, AutoData]
	public void FluxStateViewer_OnToggleSelectParameter_ShouldFireEvent(
	ToggleSelectOption expectedToggleSelectOption,
	IEnumerable<RecordedMutation> expectedRecordedMutations,
	object expectedFluxState)
	{
		//Arrange
		var eventFired = false;
		var eventToggleSelection = expectedToggleSelectOption == ToggleSelectOption.FirstOption ?
			ToggleSelectOption.SecondOption : ToggleSelectOption.FirstOption;
		var cut = RenderComponent<FluxStateViewer>(parameters => parameters
			.Add(p => p.RecordedMutations, expectedRecordedMutations)
			.Add(p => p.SelectedFluxState, expectedFluxState)
			.Add(p => p.SelectedToggleOption, expectedToggleSelectOption)
			.Add(p => p.SelectedMutationIndex, 0)
			.Add(p => p.OnToggleSelectChange, args =>
			{
				eventFired = true;
				eventToggleSelection = args;
			}));

		var stub = cut.FindComponent<Stub<ToggleSelect<int>>>();

		//Act
		cut.InvokeAsync(() => stub.Instance.Parameters.Get(x => x.OnOptionChange)
			.InvokeAsync(expectedToggleSelectOption));

		//Assert
		eventFired.ShouldBeTrue();
		eventToggleSelection.ShouldBe(expectedToggleSelectOption);
	}

	[Theory]
	[InlineAutoData(ToggleSelectOption.SecondOption)]
	public void FluxStateViewer_PopMutationClick_ShouldRaiseModal(
	ToggleSelectOption expectedToggleOption,
	IEnumerable<RecordedMutation> expectedRecordedMutations,
	object expectedFluxState)
	{
		//Arrange
		var cut = RenderComponent<FluxStateViewer>(parameters => parameters
			.Add(p => p.RecordedMutations, expectedRecordedMutations)
			.Add(p => p.SelectedFluxState, expectedFluxState)
			.Add(p => p.SelectedToggleOption, expectedToggleOption)
			.Add(p => p.SelectedMutationIndex, expectedRecordedMutations.Count()));

		var stub = cut.FindComponents<Stub<IconButton>>()[2];

		//Act
		cut.InvokeAsync(() => stub.Instance.Parameters.Get(x => x.OnClick).InvokeAsync());

		//Assert
		_modalState.Received().RaiseModal(Arg.Is<string>(ModalTypes.ConfirmationModal.ToString()),
			Arg.Is<ModalViewModel>(x =>
				x.Prompt == "Are you sure" &&
				x.Message == "Are you sure you want to revert this mutation?"));
	}

	[Theory, AutoData]
	public void FluxStateViewer_ConfirmModalPopMutation_ShouldRaiseMutationCommand(
	ToggleSelectOption expectedToggleSelectOption,
	IEnumerable<RecordedMutation> expectedRecordedMutations,
	object expectedFluxState)
	{
		//Arrange
		var eventFired = false;
		var eventToggleSelection = expectedToggleSelectOption == ToggleSelectOption.FirstOption ?
			ToggleSelectOption.SecondOption : ToggleSelectOption.FirstOption;
		var cut = RenderComponent<FluxStateViewer>(parameters => parameters
			.Add(p => p.RecordedMutations, expectedRecordedMutations)
			.Add(p => p.SelectedFluxState, expectedFluxState)
			.Add(p => p.SelectedToggleOption, expectedToggleSelectOption)
			.Add(p => p.SelectedMutationIndex, 0)
			.Add(p => p.OnPopMutation, args =>
			{
				eventFired = true;
			}));

		var stub = cut.FindComponent<Stub<ToggleSelect<int>>>();

		//Act
		typeof(FluxStateViewer).GetMethod("CloseModal", BindingFlags.Instance |
			BindingFlags.NonPublic).Invoke(cut.Instance, [new ModalCloseEventArgs(true)]);

		//Assert
		eventFired.ShouldBeTrue();
	}
}




