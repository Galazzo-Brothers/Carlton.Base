namespace Carlton.Core.Components.Library.Lab.TestData;

internal static class CheckboxTestStates
{
    public static Dictionary<string, object> CheckedState
    {
        get => new()
            {
                { nameof(Checkbox.IsChecked), true }
            };
    }

    public static Dictionary<string, object> UncheckedState
    {
        get => new()
        {
            { nameof(Checkbox.IsChecked), false }
        };
    }
}
