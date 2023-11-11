using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;
using Carlton.Core.Components.Flux.Models;

namespace Carlton.Core.Components.Lab.Test.Mocks;

public class DummyComponent : ComponentBase
{
    [Parameter]
    public string TestText { get; set; } = string.Empty;

    [Parameter]
    public EventCallback TestCallback { get; set; }

    [Parameter]
    public EventCallback<MutationCommand> TestGenericCallback { get; set; }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        base.BuildRenderTree(builder);
        //Wrapper Div
        builder.OpenElement(0, "div");
        builder.AddAttribute(1, "class", "vm-props");
        //ID Span
        builder.OpenElement(2, "span");
        builder.AddAttribute(3, "class", "test-text");
        builder.AddContent(4, TestText);
        builder.CloseElement();
        //Button
        builder.OpenElement(5, "button");
        builder.AddAttribute(6, "class", "event-callback-test");
        builder.AddAttribute(7, "onclick", TestCallback);
        builder.AddContent(8, "EventCallback Test");
        builder.CloseElement();
        //Button
        builder.OpenElement(9, "button");
        builder.AddAttribute(10, "class", "generic-event-callback-test");
        builder.AddAttribute(11, "onclick", () => TestGenericCallback.InvokeAsync(new MutationCommand()));
        builder.AddContent(12, "EventCallback Test");
        builder.CloseElement();
        //Close Wrapper Div
        builder.CloseElement();
    }
}

