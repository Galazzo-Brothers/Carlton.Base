namespace Carlton.Core.Components.Library.Lab.TestData;

internal static class DropdownTestStates
{
    public static object Default
    {
        get => new
        {
            Label = "Test",
            Options = new Dictionary<string, int>
            {
                { "Option 1", 1 },
                { "Option 2", 2 },
                { "Option 3", 3 }
            }
        };
    }
}
