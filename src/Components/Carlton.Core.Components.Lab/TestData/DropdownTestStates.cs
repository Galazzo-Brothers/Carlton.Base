using Carlton.Core.Components.Dropdowns;
namespace Carlton.Core.Components.Library.Lab.TestData;

internal static class DropdownTestStates
{
    public static Dictionary<string, object> Default
    {
        get => new()
        {
            { nameof(Dropdown<int>.Label), "Test" },
            { nameof(Dropdown<int>.Options), new Dictionary<string, int>
                    {
                      { "Option 1", 1 },
                      { "Option 2", 2 },
                      { "Option 3", 3 }
                    }
            }
        };
    }
}
