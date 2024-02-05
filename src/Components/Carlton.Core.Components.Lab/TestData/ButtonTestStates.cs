using Carlton.Core.Components.Buttons;
namespace Carlton.Core.Components.Lab.TestData;

internal static class ButtonTestStates
{
    internal static Dictionary<string, object> ButtonState
    {
        get => new()
        {
            {  nameof(ActionButton.Text), "Click Me" }
        };
    }

    public static Dictionary<string, object> IconButtonState
    {
        get => new()
        {
            { nameof(IconButton.Icon),"delete" }
        };
    }
}
