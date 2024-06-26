using AutoFixture.Xunit2;
using Bunit;
using Bunit.TestDoubles;
using Carlton.Core.Flux.Debug.Components.Logging.EventLogging.Details;
using Xunit;
using Shouldly;
using Carlton.Core.Utilities.Logging;
using Carlton.Core.Components.Consoles;
namespace Carlton.Core.Flux.Debug.Tests.Components.Logging.EventLogging.Details;

public class EventLogDetailsComponentTests : TestContext
{
	[Theory, AutoData]
	public void EventLogDetails_Markup_RendersCorrectly(LogMessage expectedLogMessage)
	{
		//Arrange
		var expectedMarkup = @"
		<div class=""log-exception-details"">
		  <div class=""log-exception-details"">
			<div>JsonViewerConsole Stub</div>
		  </div>
		</div>";
		ComponentFactories.AddStub<JsonViewerConsole>(@"<div class=""log-exception-details""><div>JsonViewerConsole Stub</div></div>");

		//Act
		var cut = RenderComponent<EventLogDetails>(parameters => parameters
			.Add(p => p.LogMessage, expectedLogMessage));

		//Assert
		cut.MarkupMatches(expectedMarkup);
	}

	[Theory, AutoData]
	public void EventLogDetails_LogMessageParameter_RendersCorrectly(LogMessage expectedLogMessage)
	{
		//Arrange
		ComponentFactories.AddStub<JsonViewerConsole>("<div>JsonViewerConsole Stub</div>");

		//Act
		var cut = RenderComponent<EventLogDetails>(parameters => parameters
			.Add(p => p.LogMessage, expectedLogMessage));
		var stub = cut.FindComponent<Stub<JsonViewerConsole>>();

		//Assert
		stub.Instance.Parameters.Get(x => x.Object).ShouldBe(expectedLogMessage);
	}
}
