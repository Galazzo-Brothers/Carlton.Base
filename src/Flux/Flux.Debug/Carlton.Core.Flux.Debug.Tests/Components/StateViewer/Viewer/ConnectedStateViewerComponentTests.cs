using AutoFixture.Xunit2;
using Bunit;
using Bunit.TestDoubles;
using Carlton.Core.Flux.Debug.Components.StateViewer.Viewer;
using Carlton.Core.Flux.Debug.Models.Commands;
using Carlton.Core.Flux.Debug.Models.ViewModels;
using Shouldly;
using Xunit;
namespace Carlton.Core.Flux.Debug.Tests.Components.StateViewer.Viewer;

public class ConnectedStateViewerComponentTests : TestContext
{
	[Theory, AutoData]
	public void ConnectedFluxStateViewer_Markup_RendersCorrectly(
		FluxStateViewerViewModel expectedViewModel)
	{
		//Arrange
		ComponentFactories.AddStub<ViewStateFluxStateViewer>("<div>FluxStateViewer Stub</div>");
		var expectedMarkup = "<div>FluxStateViewer Stub</div>";

		//Act
		var cut = RenderComponent<ConnectedFluxStateViewer>(parameters => parameters
			.Add(p => p.ViewModel, expectedViewModel));

		//Assert
		cut.MarkupMatches(expectedMarkup);
	}

	[Theory, AutoData]
	public void ConnectedFluxStateViewer_ViewModelParameter_RendersCorrectly(
		FluxStateViewerViewModel expectedViewModel)
	{
		//Arrange
		ComponentFactories.AddStub<ViewStateFluxStateViewer>("<div>FluxStateViewer Stub</div>");

		//Act
		var cut = RenderComponent<ConnectedFluxStateViewer>(parameters => parameters
			.Add(p => p.ViewModel, expectedViewModel));

		var stub = cut.FindComponent<Stub<ViewStateFluxStateViewer>>();

		//Assert
		stub.Instance.Parameters.Get(x => x.RecordedMutations).ShouldBe(expectedViewModel.RecordedMutations);
		stub.Instance.Parameters.Get(x => x.SelectedMutationIndex).ShouldBe(expectedViewModel.SelectedMutationIndex);
		stub.Instance.Parameters.Get(x => x.SelectedFluxState).ShouldBeSameAs(expectedViewModel.SelectedFluxState);
	}

	[Theory, AutoData]
	public void ConnectedFluxStateViewer_OnRecordedMutationSelected_ShouldRaiseMutationCommand(
		FluxStateViewerViewModel expectedViewModel,
		int expectedSelectedIndex)
	{
		//Arrange
		var eventFired = false;
		var command = (object)null;
		ComponentFactories.AddStub<ViewStateFluxStateViewer>("<div>FluxStateViewer Stub</div>");

		var cut = RenderComponent<ConnectedFluxStateViewer>(parameters => parameters
			.Add(p => p.ViewModel, expectedViewModel)
			.Add(p => p.OnComponentEvent, args =>
			{
				eventFired = true;
				command = args;
			}));

		var stub = cut.FindComponent<Stub<ViewStateFluxStateViewer>>();

		//Act
		cut.InvokeAsync(() => stub.Instance.Parameters.Get(x => x.OnRecordedMutationSelected)
			.InvokeAsync(new SelectedMutationCommandChangedArgs(expectedSelectedIndex)));

		//Assert
		eventFired.ShouldBeTrue();
		command.ShouldBeOfType<ChangeSelectedCommandMutationCommand>()
			   .SelectedMutationCommandIndex.ShouldBe(expectedSelectedIndex);
	}

	[Theory, AutoData]
	public void ConnectedFluxStateViewer_OnPopMutation_ShouldRaiseMutationCommand(
		FluxStateViewerViewModel expectedViewModel)
	{
		//Arrange
		var eventFired = false;
		var command = (object)null;
		ComponentFactories.AddStub<ViewStateFluxStateViewer>("<div>FluxStateViewer Stub</div>");

		var cut = RenderComponent<ConnectedFluxStateViewer>(parameters => parameters
			.Add(p => p.ViewModel, expectedViewModel)
			.Add(p => p.OnComponentEvent, args =>
			{
				eventFired = true;
				command = args;
			}));

		var stub = cut.FindComponent<Stub<ViewStateFluxStateViewer>>();

		//Act
		cut.InvokeAsync(() => stub.Instance.Parameters.Get(x => x.OnPopMutation)
			.InvokeAsync(new PopMutationEventArgs()));

		//Assert
		eventFired.ShouldBeTrue();
		command.ShouldBeOfType<PopMutationCommand>();
	}
}
