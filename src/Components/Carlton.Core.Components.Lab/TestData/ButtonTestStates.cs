namespace Carlton.Core.Components.Lab.TestData;

internal static class ButtonTestStates
{
    public static object ButtonState
    {
        get => new
        {
              Text = "Click Me" 
        };
    }

    public static object IconButtonState
    {
        get => new
        {
            Icon = "delete" 
        };
    }
}
