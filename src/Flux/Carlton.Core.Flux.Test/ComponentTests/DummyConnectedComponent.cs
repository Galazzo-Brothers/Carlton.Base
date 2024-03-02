using Microsoft.AspNetCore.Components.Rendering;
using Carlton.Core.Flux.Attributes;
using Carlton.Core.Flux.Components;
using Carlton.Core.Flux.Tests.Common;

namespace Carlton.Core.Flux.Tests.ComponentTests;

[ObserveStateEvents("TestEvent")]
[ObserveStateEvents("TestEvent2")]
[ObserveStateEvents("TestEvent3")]
public class DummyConnectedComponent : BaseConnectedComponent<TestViewModel>
{
    public async Task RaiseComponentEvent(TestCommand1 command)
    {
        await base.OnComponentEvent.InvokeAsync(command);
    }

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
        builder.OpenElement(11, "button");
        builder.AddContent(12, "Command Event Test");
        builder.CloseElement();
        //Close Wrapper Div
        builder.CloseElement();
    }
}


