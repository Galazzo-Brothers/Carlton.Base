using AutoFixture.Xunit2;
using Bunit;
using Bunit.TestDoubles;
using Carlton.Core.Components.Consoles;
using Carlton.Core.Flux.Debug.Components.Logging.TraceLogging.RequestObjectDetails;
using Shouldly;
using Xunit;
namespace Carlton.Core.Flux.Debug.Tests.Components.Logging.TraceLogging.RequestObjectDetails;

public class TraceLogRequestObjectDetailsComponentTests : TestContext
{
	[Theory, AutoData]
	public void TraceLogRequestObjectDetails_Markup_RendersCorrectly(object expectedRequestObject)
	{
		//Arrange
		ComponentFactories.AddStub<JsonViewerConsole>("<div>JsonViewerConsoleStub Stub</div>");
		var expectedMarkup = @"
		<div class=""trace-log-request-object-details"">
			<div>JsonViewerConsoleStub Stub</div>
		</div>";

		//Act
		var cut = RenderComponent<TraceLogRequestObjectDetails>(parameters => parameters
			.Add(p => p.SelectedRequestObject, expectedRequestObject));

		//Assert
		cut.MarkupMatches(expectedMarkup);
	}

	[Theory, AutoData]
	public void TraceLogRequestObjectDetails_ExpectedRequestObjectDetailsParameter_RendersCorrectly(object expectedRequestObject)
	{
		//Arrange
		ComponentFactories.AddStub<JsonViewerConsole>("<div>JsonViewerConsoleStub Stub</div>");

		//Act
		var cut = RenderComponent<TraceLogRequestObjectDetails>(parameters => parameters
			.Add(p => p.SelectedRequestObject, expectedRequestObject));

		var stub = cut.FindComponent<Stub<JsonViewerConsole>>();

		//Assert
		stub.Instance.Parameters.Get(x => x.Object).ShouldBeSameAs(expectedRequestObject);
	}
}
