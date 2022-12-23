namespace Carlton.Base.Components.TestStates;

public static class CheckboxTestStates
{
    public static Dictionary<string, object> CheckedState()
    {
        return new Dictionary<string, object>
            {
                { "IsChecked", true }
            };
    }

    public static Dictionary<string, object> UncheckedState()
    {
        return new Dictionary<string, object>
            {
                { "IsChecked", false }
            };
    }
}
