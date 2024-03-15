using Carlton.Core.Lab.Components.EventConsole;
using System.Text.Json;
using System.Web;

namespace Carlton.Core.Lab.Test.Components.EventConsole;

public class ConnectedEventConsole_Markup_RendersCorrectly : TestContext
{
	[Theory, AutoData]
	public void ConnectedEventViewerComponentRendersCorrectly(
		EventConsoleViewModel expectedViewModel)
	{
		//Arrange
		var expectedMarkup = BuildExpectedMarkup(expectedViewModel);

		//Act
		var cut = RenderComponent<ConnectedEventConsole>(parameters => parameters
			.Add(p => p.ViewModel, expectedViewModel));


		//Assert
		cut.MarkupMatches(expectedMarkup);
	}

	[Theory, AutoData]
	public void ConnectedEventViewer_Parameter_EventCallback(
		EventConsoleViewModel expectedViewModel)
	{
		//Arrange
		var eventFired = false;
		var actualArgs = (object)null;
		var cut = RenderComponent<ConnectedEventConsole>(parameters => parameters
				.Add(p => p.ViewModel, expectedViewModel)
				.Add(p => p.OnComponentEvent, args =>
				{
					eventFired = true;
					actualArgs = args;
				}));

		var btn = cut.Find(".icon-btn");

		//Act
		btn.Click();

		//Assert
		eventFired.ShouldBeTrue();
		Assert.IsType<ClearEventsCommand>(actualArgs);
	}

	private static string BuildExpectedMarkup(EventConsoleViewModel viewModel)
	{
		var recordedEventsText = string.Join(Environment.NewLine, viewModel.RecordedEvents.Select(evt =>
		  $"{evt.Name}: {JsonSerializer.Serialize(evt.EventObj)}"));

		//Assert
		return @$"
 <div class=""event-console"" >
      <div class=""console"" >
        <textarea rows=""15"" disabled="""" class="""" value=""{HttpUtility.HtmlEncode(recordedEventsText)}""></textarea>
      </div>
    <div class=""icon-btn"" >
		<span class=""mdi mdi-24px mdi-delete""></span>
	</div>
</div>";
	}
}
