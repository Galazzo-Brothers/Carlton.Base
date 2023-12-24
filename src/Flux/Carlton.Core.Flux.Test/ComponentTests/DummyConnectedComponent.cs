using Microsoft.AspNetCore.Components.Rendering;
using Carlton.Core.Components.Flux.Test.Common;
using Microsoft.AspNetCore.Components;
using Carlton.Core.Components.Flux.Attributes;

namespace Carlton.Core.Components.Flux.Test.ComponentTests;

[ObserveStateEvents("TestEvent")]
[ObserveStateEvents("TestEvent2")]
[ObserveStateEvents("TestEvent3")]
public class DummyConnectedComponent : BaseConnectedComponent<TestViewModel>
{
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        base.BuildRenderTree(builder);
        //Wrapper Div
        builder.OpenElement(0, "div");
        builder.AddAttribute(1, "class", "vm-props");
        //ID Span
        builder.OpenElement(2, "span");
        builder.AddAttribute(3, "class", "id");
        builder.AddContent(4, ViewModel.ID);
        builder.CloseElement();
        //Name Span
        builder.OpenElement(5, "span");
        builder.AddAttribute(6, "class", "name");
        builder.AddContent(7, ViewModel.Name);
        builder.CloseElement();
        //Description Span
        builder.OpenElement(8, "span");
        builder.AddAttribute(9, "class", "description");
        builder.AddContent(10, ViewModel.Description);
        builder.CloseElement();
        // Add event callback
        var command = new TestCommand1("Test Command", "This is a test");
        var eventCallback = EventCallback.Factory.Create(this, () => base.OnComponentEvent.InvokeAsync(command));
        builder.OpenElement(11, "button");
        builder.AddAttribute(12, "onclick", eventCallback);
        builder.AddContent(13, "Command Event Test");
        builder.CloseElement();
        //Close Wrapper Div
        builder.CloseElement();
    }
}

