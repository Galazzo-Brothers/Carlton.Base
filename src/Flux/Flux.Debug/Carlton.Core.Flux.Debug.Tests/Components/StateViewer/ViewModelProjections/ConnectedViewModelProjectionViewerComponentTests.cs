using AutoFixture.Xunit2;
using Bunit;
using Bunit.TestDoubles;
using Carlton.Core.Flux.Debug.Components.StateViewer.ViewModelProjections;
using Carlton.Core.Flux.Debug.Models.ViewModels;
using Shouldly;
using Xunit;
namespace Carlton.Core.Flux.Debug.Tests.Components.StateViewer.ViewModelProjections;

public class ConnectedViewModelProjectionViewerComponentTests : TestContext
{
	[Theory, AutoData]
	public void ConnectedViewModelProjectionViewer_Markup_RendersCorrectly(
		ViewModelProjectionsViewerViewModel expectedViewModel)
	{
		//Arrange
		ComponentFactories.AddStub<ViewModelProjectionViewer>("<div>ViewModelProjectionViewer Stub</div>");
		var expectedMarkup = @"<div>ViewModelProjectionViewer Stub</div>";

		//Act
		var cut = RenderComponent<ConnectedViewModelProjectionViewer>(parameters => parameters
			.Add(p => p.ViewModel, expectedViewModel));

		//Assert
		cut.MarkupMatches(expectedMarkup);
	}

	[Theory, AutoData]
	public void ConnectedViewModelProjectionViewer_ViewModelParameters_RendersCorrectly(
		ViewModelProjectionsViewerViewModel expectedViewModel)
	{
		//Arrange
		ComponentFactories.AddStub<ViewStateViewModelProjectionViewer>("<div>ViewModelProjectionViewer Stub</div>");

		//Act
		var cut = RenderComponent<ConnectedViewModelProjectionViewer>(parameters => parameters
			.Add(p => p.ViewModel, expectedViewModel));

		var stub = cut.FindComponent<Stub<ViewStateViewModelProjectionViewer>>();

		//Assert
		stub.Instance.Parameters.Get(x => x.ViewModelTypes).ShouldBeSameAs(expectedViewModel.ViewModelTypes);
		stub.Instance.Parameters.Get(x => x.State).ShouldBeSameAs(expectedViewModel.FluxState);
	}
}
