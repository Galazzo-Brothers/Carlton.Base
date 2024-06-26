using Bunit;
using Carlton.Core.Flux.Debug.Components.Logging.EventLogging.LogTable;
using Microsoft.Extensions.Logging;
using Shouldly;
using Xunit;
namespace Carlton.Core.Flux.Debug.Tests.Components.Logging.EventLogging.LogTable;

public class LogLevelPillComponentTests : TestContext
{
	[Theory]
	[InlineData(LogLevel.Trace)]
	[InlineData(LogLevel.Debug)]
	[InlineData(LogLevel.Information)]
	[InlineData(LogLevel.Warning)]
	[InlineData(LogLevel.Error)]
	[InlineData(LogLevel.Critical)]
	public void LogLevelPill_Markup_RendersCorrectly(LogLevel expectedLogLevel)
	{
		//Arrange
		var expectedMarkup = @$"
		<div class=""log-level-pill {expectedLogLevel.ToString().ToLower()}"">
			<div class=""pill"">{expectedLogLevel.ToString()}</div>
		</div>";

		//Act
		var cut = RenderComponent<LogLevelPill>(parameters => parameters
			.Add(p => p.LogLevel, expectedLogLevel));

		//Assert
		cut.MarkupMatches(expectedMarkup);
	}

	[Theory]
	[InlineData(LogLevel.Trace)]
	[InlineData(LogLevel.Debug)]
	[InlineData(LogLevel.Information)]
	[InlineData(LogLevel.Warning)]
	[InlineData(LogLevel.Error)]
	[InlineData(LogLevel.Critical)]
	public void LogLevelPill_LogLevelParameter_RendersCorrectly(LogLevel expectedLogLevel)
	{
		//Act
		var cut = RenderComponent<LogLevelPill>(parameters => parameters
			.Add(p => p.LogLevel, expectedLogLevel));

		var classExists = cut.Find(".log-level-pill").ClassList.Contains(expectedLogLevel.ToString().ToLower());
		var pillText = cut.Find(".pill").TextContent;

		//Assert
		classExists.ShouldBeTrue();
		pillText.ShouldBe(expectedLogLevel.ToString());
	}
}
