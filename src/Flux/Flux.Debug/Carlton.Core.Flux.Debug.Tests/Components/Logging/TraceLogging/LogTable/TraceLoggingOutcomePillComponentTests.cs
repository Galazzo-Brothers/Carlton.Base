using AutoFixture.Xunit2;
using Bunit;
using Xunit;
using Carlton.Core.Flux.Debug.Components.Logging.TraceLogging.LogTable;
namespace Carlton.Core.Flux.Debug.Tests.Components.Logging.TraceLogging.LogTable;

public class TraceLoggingOutcomePillComponentTests : TestContext
{
	[Theory]
	[InlineAutoData(true)]
	[InlineAutoData(false)]
	public void TraceLoggingOutcomePill_Markup_RendersCorrectly(bool expectedRequestSucceeded)
	{
		//Arrange
		var expectedMarkup = @$"
			<div class=""log-outcome-pill {(expectedRequestSucceeded ? "succeeded" : "failed")}"" >
			  <div class=""pill"">{(expectedRequestSucceeded ? "Success" : "Error")}</div>
			</div>";

		//Act
		var cut = RenderComponent<TraceLoggingOutcomePill>(parameters => parameters
			.Add(p => p.RequestSucceeded, expectedRequestSucceeded));

		//Assert
		cut.MarkupMatches(expectedMarkup);
	}

	[Theory]
	[InlineAutoData(true)]
	[InlineAutoData(false)]
	public void TraceLoggingOutcomePill_RequestSucceededParameter_RendersCorrectly(bool expectedRequestSucceeded)
	{
		//Arrange
		var expectedText = expectedRequestSucceeded ? "Success" : "Error";

		//Act
		var cut = RenderComponent<TraceLoggingOutcomePill>(parameters => parameters
			.Add(p => p.RequestSucceeded, expectedRequestSucceeded));

		var pill = cut.Find(".pill");

		//Assert
		pill.TextContent = expectedText;
	}
}
