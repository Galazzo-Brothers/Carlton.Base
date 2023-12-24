using AutoFixture;
using Bunit;
using Carlton.Core.Components.Flux.Models;
using Carlton.Core.Components.Lab.Test.Mocks;
using System.Text.Json;
using System.Web;

namespace Carlton.Core.Components.Lab.Test.ComponentTests;

public class ConnectedEventConsoleTests : TestContext
{
    [Theory]
    [InlineData(3)]
    [InlineData(5)]
    public void ConnectedEventViewerComponentRendersCorrectly(int numOfEvents)
    {
        //Arrange
        var vm = BuildViewModel(numOfEvents);
        var expectedMarkup = BuildExpectedMarkup(vm.RecordedEvents);

        //Act
        var cut = RenderComponent<ConnectedEventConsole>(parameters => parameters
                .Add(p => p.ViewModel, vm));


        //Assert
        cut.MarkupMatches(expectedMarkup);
    }

    [Theory]
    [InlineData(3)]
    [InlineData(5)]
    public void ConnectedEventViewerComponent_EventCallback(int numOfEvents)
    {
        //Arrange
        var command = new MutationCommand();
        var onComponentEventCalled = false;
        var vm = BuildViewModel(numOfEvents);
        var cut = RenderComponent<ConnectedEventConsole>(parameters => parameters
                .Add(p => p.ViewModel, vm)
                .Add(p => p.OnComponentEvent, cmd =>
                {
                    onComponentEventCalled = true;
                    command = cmd;
                }));

        var btn = cut.Find(".fab");

        //Act
        btn.Click();

        //Assert
        Assert.True(onComponentEventCalled);
        Assert.IsType<ClearEventsCommand>(command);
    }

    private static EventConsoleViewModel BuildViewModel(int numOfEvents)
    {
        var fixture = new Fixture();
        var recordedEvents = new List<ComponentRecordedEvent>();
        var events = fixture.CreateMany<MockObject>(numOfEvents);
        events.ToList().ForEach(_ => recordedEvents.Add(new ComponentRecordedEvent(fixture.Create<string>(), _)));
        return new EventConsoleViewModel(recordedEvents);
    }

    private static string BuildExpectedMarkup(IEnumerable<ComponentRecordedEvent> recordedEvents)
    {
        var recordedEventsText = string.Join(Environment.NewLine, recordedEvents.Select(evt =>
          $"{evt.Name}: {JsonSerializer.Serialize(evt.EventObj)}"));

        //Assert
        return @$"
 <div class=""event-console"" >
      <div class=""console"" >
        <textarea rows=""15"" disabled="""" class="""" value=""{HttpUtility.HtmlEncode(recordedEventsText)}""></textarea>
      </div>
    <div class=""fab mdi mdi-24px mdi-delete"" style=""--fab-bottom:5%;--fab-right:3%;"" ></div>
</div>";
    }
}
