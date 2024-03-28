using Carlton.Core.Components.Dropdowns;
namespace Carlton.Core.Components.Lab.TestData;

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
			new() { MenuItemName = "Option 1", Value = 1, MenuIcon = "mdi-icon1", AccentColorIndex = 1, MenuItemSelected = () => { } },
			new() { MenuItemName = "Option 2", Value = 2, MenuIcon = "mdi-icon2", AccentColorIndex = 2, MenuItemSelected = () => { } },
			new() { MenuItemName = "Option 3", Value = 3, MenuIcon = "mdi-icon3", AccentColorIndex = 3, MenuItemSelected = () => { } }
		};
	}
}

