using Carlton.Core.Components.Dropdowns;
namespace Carlton.Core.Components.Lab.TestData;

internal static class KebabMenuTestStates
{
    public static Dictionary<string, object> Default
    {
        get => new()
        {
            { nameof(KebabMenu<int>.IsDisabled), false },
            { nameof(KebabMenu<int>.MenuItems), new List<DropdownMenuItem<int>>
                {
                    new("Item 1", 1, "account", 1, () => { }),
                    new("Item 2", 2, "theme-light-dark", 2, () => { }),
                    new("Item 3", 3, "delete", 3, () => { })
                }
            }
        };
    }
}
