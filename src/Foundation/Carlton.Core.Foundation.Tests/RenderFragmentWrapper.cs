using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;
namespace Carlton.Core.Foundation.Tests.Common;


public class RenderFragmentWrapper : ComponentBase
{
    public const string Render_Fragment_Wrapper_Class = ".render-fragment-wrapper";

    [Parameter]
    public RenderFragment TestFragment { get; set; }
   
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        base.BuildRenderTree(builder);
        //Wrapper Div
        builder.OpenElement(0, "div");
        builder.AddAttribute(1, "class", "render-fragment-wrapper");
        //Child Content
        builder.AddContent(2, TestFragment);
        //Close Wrapper Div
        builder.CloseElement();
    }
}


public class TypedRenderFragmentWrapper<T> : ComponentBase
{
    public const string Render_Fragment_Wrapper_Class = ".render-fragment-wrapper";

    [Parameter]
    public RenderFragment<T> TestFragment { get; set; }
    [Parameter]
    public T ContentData { get; set; }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        base.BuildRenderTree(builder);
        //Wrapper Div
        builder.OpenElement(0, "div");
        builder.AddAttribute(1, "class", "render-fragment-wrapper");
        //Child Content
        builder.AddContent(2, TestFragment, ContentData);
        //Close Wrapper Div
        builder.CloseElement();
    }
}

