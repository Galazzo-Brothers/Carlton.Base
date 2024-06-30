using AutoFixture.Xunit2;
using Bunit;
using Carlton.Core.Flux.Debug.Components.Logging.EventLogging;
using Microsoft.AspNetCore.Components;
using Shouldly;
using Xunit;

namespace Carlton.Core.Flux.Debug.Tests.Components.Logging.EventLogging.Filtering;

public class EventLogTextFilterComponentTests : TestContext
{
	[Theory, InlineAutoData]
	public void EventLogTextFilter_Markup_RendersCorrectly(string expectedText)
	{
		//Arrange
		var expectedMarkup = @$"
		<div class=""log-text-filter"" >
			<input class=""search-text"" type=""text""  value=""{expectedText}"" >
		</div>";

		//Act
		var cut = RenderComponent<EventLogTextFilter>(parameters => parameters
			.Add(p => p.Text, expectedText));

		//Assert
		cut.MarkupMatches(expectedMarkup);
	}

	[Theory, InlineAutoData]
	public void EventLogTextFilter_TextParameter_TextRendersCorrectly(string expectedText)
	{
		//Act
		var cut = RenderComponent<EventLogTextFilter>(parameters => parameters
			.Add(p => p.Text, expectedText));

		var searchText = cut.Find(".search-text").TextContent;

		//Assert
		searchText.ShouldBe(searchText);
	}

	[Theory, InlineAutoData]
	public void EventLogTextFilter_OnTextChangedParameter_ShouldFireEvent(string expectedText)
	{
		//Arrange
		var eventFired = false;
		var eventText = string.Empty;
		var cut = RenderComponent<EventLogTextFilter>(parameters => parameters
			.Add(p => p.Text, "old text")
			.Add(p => p.OnTextChanged, txt =>
			{
				eventFired = true;
				eventText = txt;
			}));

		//Act
		var textBox = cut.Find(".search-text");
		textBox.Change(new ChangeEventArgs { Value = expectedText });

		//Assert
		eventFired.ShouldBeTrue();
		eventText.ShouldBe(expectedText);
	}
}
