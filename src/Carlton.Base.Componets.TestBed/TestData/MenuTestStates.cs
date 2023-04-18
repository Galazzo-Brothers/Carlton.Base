namespace Carlton.Base.Components.TestBed;

internal static class MenuTestStates
{
    public static Dictionary<string, object> Default()
    {
        return new Dictionary<string, object>
        {
            {"MenuItems", DefaultMenuItems() }
        };
    }

    private static IEnumerable<DropdownMenuItem<int>> DefaultMenuItems()
    {
        return new List<DropdownMenuItem<int>>
        {
            new DropdownMenuItem<int>("Option 1", 1, "mdi-icon1", 1),
            new DropdownMenuItem<int>("Option 2", 2, "mdi-icon2", 2),
            new DropdownMenuItem<int>("Option 3", 3, "mdi-icon3", 3)
        };
    }
}

