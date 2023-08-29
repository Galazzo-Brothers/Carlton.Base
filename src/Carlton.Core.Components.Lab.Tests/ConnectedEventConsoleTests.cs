using Bunit;
using Carlton.Core.Components.Flux.Models;
using Carlton.Core.Components.Lab.Models;

namespace Carlton.Core.Components.Lab.Test;

public class ConnectedEventConsoleTests : TestContext
{
    [Fact]
    public void ConnectedEventConsoleComponentRendersCorrectly()
    {
        //Arrange
        var vm = new EventConsoleViewModel(new List<ComponentRecordedEvent>
        {
            new ComponentRecordedEvent("Event 1", new {Prop1 = "test", Prop2 = 7, Prop3 = false}),
            new ComponentRecordedEvent("Event 2", new {Prop1 =  7.77 }),
            new ComponentRecordedEvent("Event 3", new {Prop1 = true})
        });
        
        //Act
        var cut = RenderComponent<ConnectedEventConsole>(parameters => parameters
                .Add(p => p.ViewModel, vm));

        //Assert
        cut.MarkupMatches(@"
 <div class=""event-console"" >
      <div class=""console"" >
        <textarea rows=""15"" disabled="""" class="""" value=
""Event 1: {&quot;Prop1&quot;:&quot;test&quot;,&quot;Prop2&quot;:7,&quot;Prop3&quot;:false}
Event 2: {&quot;Prop1&quot;:7.77}
Event 3: {&quot;Prop1&quot;:true}"">
        </textarea>
      </div>
    <div class=""fab mdi mdi-24px mdi-delete"" style=""--fab-bottom:5%;--fab-right:3%;"" ></div>
</div>");
    }

    [Fact]
    public void ConnectedEventViewerComponent_EventCallback()
    {
        //Arrange
        var  command = new MutationCommand();
        var onComponentEventCalled = false;
        var vm = new EventConsoleViewModel(new List<ComponentRecordedEvent>
        {
            new ComponentRecordedEvent("Event 1", new {Prop1 = "test", Prop2 = 7, Prop3 = false}),
            new ComponentRecordedEvent("Event 2", new {Prop1 =  7.77 }),
            new ComponentRecordedEvent("Event 3", new {Prop1 = true})
        });

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
}
