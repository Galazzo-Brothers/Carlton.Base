using AutoFixture.Xunit2;
using Bunit;
using Xunit;
using Carlton.Core.Components.Consoles;
using Carlton.Core.Flux.Debug.Components.Logging.TraceLogging.ContextDetails;
using Bunit.TestDoubles;
using Shouldly;
namespace Carlton.Core.Flux.Debug.Tests.Components.Logging.TraceLogging.ContextDetails;

public class TraceLogRequestContextDetailsComponentTests : TestContext
{
	[Theory, AutoData]
	public void TraceLogRequestContextDetails_Markup_RendersCorrectly(object expectedContext)
	{
		//Arrange
		ComponentFactories.AddStub<JsonViewerConsole>("<div>JsonViewerConsole Stub</div>");
		var expectedMarkup = @"
			<div class=""trace-log-request-context-details"">
				<div>JsonViewerConsole Stub</div>
			</div>";

		//Act
		var cut = RenderComponent<TraceLogRequestContextDetails>(parameters => parameters
			.Add(p => p.SelectedRequestContext, expectedContext));

		//Assert
		cut.MarkupMatches(expectedMarkup);
	}

	[Theory, AutoData]
	public void TraceLogRequestContextDetails_ContextParameter_RendersCorrectly(object expectedContext)
	{
		//Arrange
		ComponentFactories.AddStub<JsonViewerConsole>("<div>JsonViewerConsole Stub</div>");

		//Act
		var cut = RenderComponent<TraceLogRequestContextDetails>(parameters => parameters
			.Add(p => p.SelectedRequestContext, expectedContext));
		var stub = cut.FindComponent<Stub<JsonViewerConsole>>();

		//Assert
		stub.Instance.Parameters.Get(x => x.Object).ShouldBeSameAs(expectedContext);
	}
}
