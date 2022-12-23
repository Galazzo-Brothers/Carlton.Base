namespace Carlton.Base.Components.TestStates;

public static class MenuTestStates
{
    public static Dictionary<string, object> Default()
    {
        return new Dictionary<string, object>
        {
            {"MenuItems", DefaultMenuItems() }
        };
    }

    private static IEnumerable<MenuItem> DefaultMenuItems()
    {
        return new List<MenuItem>
              {
                      new MenuItem("Option 1", () => Task.CompletedTask),
                      new MenuItem("Option 2", () => Task.CompletedTask),
                      new MenuItem("Option 3", () => Task.CompletedTask)
              };
    }
}

