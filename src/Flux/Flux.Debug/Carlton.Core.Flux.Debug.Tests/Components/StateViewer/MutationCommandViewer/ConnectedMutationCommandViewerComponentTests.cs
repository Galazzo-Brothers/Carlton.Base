using AutoFixture.Xunit2;
using Bunit;
using Bunit.TestDoubles;
using Carlton.Core.Flux.Debug.Models.ViewModels;
using Shouldly;
using Xunit;
namespace Carlton.Core.Flux.Debug.Tests.Components.StateViewer.MutationCommandViewer;

public class ConnectedMutationCommandViewerComponentTests : TestContext
{
	[Theory, AutoData]
	public void ConnectedMutationCommandViewer_Markup_RendersCorrectly(MutationCommandViewerViewModel expectedViewModel)
	{
		//Arrange
		ComponentFactories.AddStub<Debug.Components.StateViewer.MutationCommandViewer.MutationCommandViewer>("<div>MutationCommandViewer Stub</div>");
		var expectedMarkup = @"<div>MutationCommandViewer Stub</div>";

		//Act 
		var cut = RenderComponent<Debug.Components.StateViewer.MutationCommandViewer.ConnectedMutationCommandViewer>(parameters => parameters
					.Add(p => p.ViewModel, expectedViewModel));

		//Assert
		cut.MarkupMatches(expectedMarkup);
	}

	[Theory, AutoData]
	public void ConnectedMutationCommandViewer_SelectedMutationCommandParameter_RendersCorrectly(MutationCommandViewerViewModel expectedViewModel)
	{
		//Arrange
		ComponentFactories.AddStub<Debug.Components.StateViewer.MutationCommandViewer.MutationCommandViewer>("<div>MutationCommandViewer Stub</div>");

		//Act 
		var cut = RenderComponent<Debug.Components.StateViewer.MutationCommandViewer.ConnectedMutationCommandViewer>(parameters => parameters
					.Add(p => p.ViewModel, expectedViewModel));

		var stub = cut.FindComponent<Stub<Debug.Components.StateViewer.MutationCommandViewer.MutationCommandViewer>>();

		//Assert
		stub.Instance.Parameters.Get(x => x.SelectedMutationCommand).ShouldBeSameAs(expectedViewModel.SelectedMutationCommand);
	}
}
