using AutoFixture.Xunit2;
using Bunit;
using Carlton.Core.Flux.Debug.Components.Logging.EventLogging;
using Microsoft.Extensions.Logging;
using Shouldly;
using Xunit;

namespace Carlton.Core.Flux.Debug.Tests.Components.Logging.EventLogging.Filtering;

public class EventLogLevelFilterComponentTests : TestContext
{
	[Theory]
	[InlineAutoData(true)]
	[InlineAutoData(false)]
	public void EventLogLevelFilter_Markup_RendersCorrectly(bool expectedLogLevelIncluded, LogLevel expectedLogLevel, int expectedCount)
	{
		//Arrange
		var expectedMarkup = @$"
			<div class=""log-level-filter trace""  >
				<div class=""checkbox mdi mdi-24px {(expectedLogLevelIncluded ? "mdi-check-circle" : "mdi-checkbox-blank-circle-outline")}""  ></div>
				<span class=""log-level-label"" >{expectedLogLevel.ToString()}:</span>
				<span class=""log-level-count"" >{expectedCount}</span>
			</div>";

		//Act
		var cut = RenderComponent<EventLogLevelFilter>(parameters => parameters
			.Add(p => p.LogLevel, expectedLogLevel)
			.Add(p => p.LogLevelIncluded, expectedLogLevelIncluded)
			.Add(p => p.Count, expectedCount));

		//Assert
		cut.MarkupMatches(expectedMarkup);
	}

	[Theory]
	[InlineAutoData(true)]
	[InlineAutoData(false)]
	public void EventLogLevelFilter_LogLevelIncludedParameter_RendersCorrectly(bool expectedLogLevelIncluded, LogLevel expectedLogLevel, int expectedCount)
	{
		//Arrange
		var expectedClass = (expectedLogLevelIncluded ? "mdi-check-circle" : "mdi-checkbox-blank-circle-outline");

		//Act
		var cut = RenderComponent<EventLogLevelFilter>(parameters => parameters
			.Add(p => p.LogLevel, expectedLogLevel)
			.Add(p => p.LogLevelIncluded, expectedLogLevelIncluded)
			.Add(p => p.Count, expectedCount));

		var classes = cut.Find(".checkbox").ClassList;


		//Assert
		classes.ShouldContain(expectedClass);
	}

	[Theory]
	[InlineAutoData(LogLevel.Trace)]
	[InlineAutoData(LogLevel.Debug)]
	[InlineAutoData(LogLevel.Information)]
	[InlineAutoData(LogLevel.Warning)]
	[InlineAutoData(LogLevel.Error)]
	[InlineAutoData(LogLevel.Critical)]
	public void EventLogLevelFilter_LogLevelParameter_RendersCorrectly(LogLevel expectedLogLevel, bool expectedLogLevelIncluded, int expectedCount)
	{
		//Act
		var cut = RenderComponent<EventLogLevelFilter>(parameters => parameters
			.Add(p => p.LogLevel, expectedLogLevel)
			.Add(p => p.LogLevelIncluded, expectedLogLevelIncluded)
			.Add(p => p.Count, expectedCount));

		var actualContent = cut.Find(".log-level-label").TextContent;


		//Assert
		actualContent.ShouldContain(GetLogLevelLabel(expectedLogLevel));
	}

	[Theory, AutoData]
	public void EventLogLevelFilter_CountParameter_RendersCorrectly(bool expectedLogLevelIncluded, LogLevel expectedLogLevel, int expectedCount)
	{
		//Act
		var cut = RenderComponent<EventLogLevelFilter>(parameters => parameters
			.Add(p => p.LogLevel, expectedLogLevel)
			.Add(p => p.LogLevelIncluded, expectedLogLevelIncluded)
			.Add(p => p.Count, expectedCount));

		var actualCount = int.Parse(cut.Find(".log-level-count").TextContent);


		//Assert
		actualCount.ShouldBe(expectedCount);
	}

	[Theory]
	[InlineAutoData(true)]
	[InlineAutoData(false)]
	public void EventLogLevelFilter_OnIncludedLogLevelChangeParameter_ShouldFireEvent(bool expectedLogLevelIncluded, LogLevel expectedLogLevel, int expectedCount)
	{
		//Arrange
		var eventFired = false;
		var actualIsIncluded = false;

		var cut = RenderComponent<EventLogLevelFilter>(parameters => parameters
			.Add(p => p.LogLevel, expectedLogLevel)
			.Add(p => p.LogLevelIncluded, expectedLogLevelIncluded)
			.Add(p => p.Count, expectedCount)
			.Add(p => p.OnIncludeLogLevelChange, args =>
			{
				eventFired = true;
				actualIsIncluded = args;
			}));

		var checkBox = cut.Find(".checkbox");

		//Act
		checkBox.Click();

		//Assert
		eventFired.ShouldBeTrue();
		actualIsIncluded.ShouldBe(!expectedLogLevelIncluded);
	}

	private static string GetLogLevelLabel(LogLevel logLevel)
	{
		switch (logLevel)
		{
			case LogLevel.Trace:
				return "Trace";
			case LogLevel.Debug:
				return "Debug";
			case LogLevel.Information:
				return "Info";
			case LogLevel.Warning:
				return "Warn";
			case LogLevel.Error:
				return "Error";
			case LogLevel.Critical:
				return "Critical";
			default:
				throw new ArgumentException("Invalid LogLevel specified");
		}
	}
}
