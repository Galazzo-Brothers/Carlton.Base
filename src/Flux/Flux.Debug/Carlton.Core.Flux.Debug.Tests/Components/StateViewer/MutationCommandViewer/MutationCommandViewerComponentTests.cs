using AutoFixture.Xunit2;
using Bunit;
using Bunit.TestDoubles;
using Carlton.Core.Components.Consoles;
using Shouldly;
using Xunit;

namespace Carlton.Core.Flux.Debug.Tests.Components.StateViewer.MutationCommandViewer;

public class MutationCommandViewerComponentTests : TestContext
{
	[Theory, AutoData]
	public void MutationCommandViewer_Markup_RendersCorrectly(object expectedSelectedMutationCommand)
	{
		//Arrange
		ComponentFactories.AddStub<JsonViewerConsole>("<div>JsonViewerConsole Stub</div>");
		var expectedMarkup = @"
			<div class=""mutation-command-viewer"" >
				<span class=""mutation-command-label"" >Mutation Command: Object</span>
				<div>JsonViewerConsole Stub</div>
			</div>";

		//Act 
		var cut = RenderComponent<Debug.Components.StateViewer.MutationCommandViewer.MutationCommandViewer>(parameters => parameters
				.Add(p => p.SelectedMutationCommand, expectedSelectedMutationCommand));

		//Assert
		cut.MarkupMatches(expectedMarkup);
	}

	[Theory, AutoData]
	public void MutationCommandViewer_SelectedMutationCommandParameter_RendersCorrectly(object expectedSelectedMutationCommand)
	{
		//Arrange
		ComponentFactories.AddStub<JsonViewerConsole>("<div>JsonViewerConsole Stub</div>");

		//Act 
		var cut = RenderComponent<Debug.Components.StateViewer.MutationCommandViewer.MutationCommandViewer>(parameters => parameters
					.Add(p => p.SelectedMutationCommand, expectedSelectedMutationCommand));

		var stub = cut.FindComponent<Stub<JsonViewerConsole>>();

		//Assert
		stub.Instance.Parameters.Get(x => x.Object).ShouldBeSameAs(expectedSelectedMutationCommand);
	}
}
