using AutoFixture.Xunit2;
using Bunit;
using Carlton.Core.Components.Flux.Models;
using Carlton.Core.Components.Lab.Test.Common;
using Carlton.Core.Components.Lab.Test.Mocks;

namespace Carlton.Core.Components.Lab.Test.ComponentTests;

public class ConnectedComponentViewerTests : TestContext
{
    [Theory, AutoData]
    public void ConnectedComponentViewerComponentRendersCorrectly(string testText)
    {
        //Arrange
        var componentParameters = new ComponentParameters(new { TestText = testText }, ParameterObjectType.ParameterObject);
        var vm = new ComponentViewerViewModel(typeof(DummyComponent), componentParameters);

        //Act
        var cut = RenderComponent<ConnectedComponentViewer>(parameters => parameters
                .Add(p => p.ViewModel, vm));

        //Assert
        cut.MarkupMatches(@$"
<div class=""component-viewer"" >
    <div class=""vm-props"">
        <span class=""test-text"">{testText}</span>
        <button class=""event-callback-test"">EventCallback Test</button>
        <button class=""generic-event-callback-test"" >EventCallback Test</button>
    </div>
</div>");
    }

    [Fact]
    public void ConnectedComponentViewerComponent_EventCallback()
    {
        //Arrange
        var command = new MutationCommand();
        var onComponentEventCalled = false;
        var castedCommand = command as RecordEventCommand;
        var componentParameters = new ComponentParameters(new { TestText = "Hello World!" }, ParameterObjectType.ParameterObject);
        var vm = new ComponentViewerViewModel(typeof(DummyComponent), componentParameters);

        var cut = RenderComponent<ConnectedComponentViewer>(parameters => parameters
                .Add(p => p.ViewModel, vm)
                .Add(p => p.OnComponentEvent, cmd =>
                {
                    command = cmd;
                    onComponentEventCalled = true;
                }));


        cut.Render();
        var btn = cut.Find(".event-callback-test");

        //Act
        btn.Click();

        //Assert
        Assert.True(onComponentEventCalled);
        Assert.IsType<RecordEventCommand>(command);
        Assert.Null(command.Cast<RecordEventCommand>().EventArgs);
    }

    [Fact]
    public void ConnectedComponentViewerComponent_EventCallbackGeneric()
    {
        //Arrange
        var command = new MutationCommand();
        var onComponentEventCalled = false;
        var componentParameters = new ComponentParameters(new { TestText = "Hello World!" }, ParameterObjectType.ParameterObject);
        var vm = new ComponentViewerViewModel(typeof(DummyComponent), componentParameters);

        var cut = RenderComponent<ConnectedComponentViewer>(parameters => parameters
                .Add(p => p.ViewModel, vm)
                .Add(p => p.OnComponentEvent, cmd =>
                {
                    command = cmd;
                    onComponentEventCalled = true;
                }));


        cut.Render();
        var btn = cut.Find(".generic-event-callback-test");

        //Act
        btn.Click();

        //Assert
        Assert.True(onComponentEventCalled);
        Assert.IsType<RecordEventCommand>(command);
        Assert.NotNull(command.Cast<RecordEventCommand>().EventArgs);
    }
}
