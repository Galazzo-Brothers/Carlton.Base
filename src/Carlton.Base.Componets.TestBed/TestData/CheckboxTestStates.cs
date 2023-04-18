namespace Carlton.Base.Components.TestBed;

internal static class CheckboxTestStates
{
    public static Dictionary<string, object> CheckedState
    {
        get => new()
            {
                { "IsChecked", true }
            };
    }

    public static Dictionary<string, object> UncheckedState
    {
        get => new()
            {
                { "IsChecked", false }
            };
    }
}
