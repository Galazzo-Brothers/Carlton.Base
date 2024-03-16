using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;
namespace Carlton.Core.Lab.Test.Common;

public class DummyComponent : ComponentBase
{
	[Parameter]
	public string SomeText { get; set; } = string.Empty;

	[Parameter]
	public int SomeNumber { get; set; }

	[Parameter]
	public bool SomeBoolean { get; set; }

	[Parameter]
	public EventCallback TestCallback { get; set; }

	[Parameter]
	public EventCallback<TestArgs> TestGenericCallback { get; set; }

	public TestArgs CallbackTypedArgs { get; set; }

	protected override void BuildRenderTree(RenderTreeBuilder builder)
	{
		base.BuildRenderTree(builder);
		//Wrapper Div
		builder.OpenElement(0, "div");
		builder.AddAttribute(1, "class", "view-model-test-wrapper");
		//ID Span
		builder.OpenElement(2, "span");
		builder.AddAttribute(3, "class", "view-model-id");
		builder.AddContent(4, SomeNumber);
		builder.CloseElement();
		//Text Span
		builder.OpenElement(5, "span");
		builder.AddAttribute(6, "class", "view-model-text");
		builder.AddContent(7, SomeText);
		builder.CloseElement();
		//bool Span
		builder.OpenElement(8, "span");
		builder.AddAttribute(9, "class", "view-model-boolean");
		builder.AddContent(10, SomeBoolean);
		builder.CloseElement();
		//Button
		builder.OpenElement(11, "button");
		builder.AddAttribute(12, "class", "event-callback-test");
		builder.AddAttribute(13, "onclick", () => TestCallback.InvokeAsync());
		builder.AddContent(14, "EventCallback Test");
		builder.CloseElement();
		//Button
		builder.OpenElement(15, "button");
		builder.AddAttribute(16, "class", "generic-event-callback-test");
		builder.AddAttribute(17, "onclick", () => TestGenericCallback.InvokeAsync(CallbackTypedArgs));
		builder.AddContent(18, "EventCallback Test");
		builder.CloseElement();
		//Close Wrapper Div
		builder.CloseElement();
	}
}

