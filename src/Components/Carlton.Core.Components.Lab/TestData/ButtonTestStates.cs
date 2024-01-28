namespace Carlton.Core.Components.Lab.TestData;

internal static class ButtonTestStates
{
    public static object DefaultState
    {
        get => new
        {
            Text = "Click Me"
        };
    }

    private static RenderFragment ButtonContentFragmentBuilder(string text)
    {
        return
           (builder) =>
           {
               builder.OpenElement(1, "span");
               builder.AddContent(2, text);
               builder.CloseElement();
           };
    }
}
