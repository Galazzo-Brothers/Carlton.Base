using Carlton.Core.Components.Dropdowns;
namespace Carlton.Core.Components.Library.Lab.TestData;

internal static class MenuTestStates
{
    public static object Default
    {
        get => new
        {
            MenuItems = DefaultMenuItems
        };
    }

    private static IEnumerable<DropdownMenuItem<int>> DefaultMenuItems
    {
        get => new List<DropdownMenuItem<int>>
        {
            new("Option 1", 1, "mdi-icon1", 1, () => { }),
            new("Option 2", 2, "mdi-icon2", 2, () => { }),
            new("Option 3", 3, "mdi-icon3", 3, () => { })
        };
    }
}

