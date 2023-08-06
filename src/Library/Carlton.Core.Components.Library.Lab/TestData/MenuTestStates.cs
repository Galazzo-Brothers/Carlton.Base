namespace Carlton.Core.Components.Library.Lab.TestData;

internal static class MenuTestStates
{
    public static object Default
    {
        get => new
        {
            MenuItems=DefaultMenuItems
        };
    }

    private static IEnumerable<DropdownMenuItem<int>> DefaultMenuItems
    {
        get => new List<DropdownMenuItem<int>>
        {
            new DropdownMenuItem<int>("Option 1", 1, "mdi-icon1", 1),
            new DropdownMenuItem<int>("Option 2", 2, "mdi-icon2", 2),
            new DropdownMenuItem<int>("Option 3", 3, "mdi-icon3", 3)
        };
    }
}

