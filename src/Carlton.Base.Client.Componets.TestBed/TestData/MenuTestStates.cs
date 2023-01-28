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

    private static IEnumerable<DropdownMenuItem> DefaultMenuItems()
    {
        return new List<DropdownMenuItem>
              {
                      //new MenuItem("Option 1", () => Task.CompletedTask),
                      //new MenuItem("Option 2", () => Task.CompletedTask),
                      //new MenuItem("Option 3", () => Task.CompletedTask)
              };
    }
}

