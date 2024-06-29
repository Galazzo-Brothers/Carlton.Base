using Carlton.Core.Components.Dropdowns;
namespace Carlton.Core.Components.Lab.TestData;

internal static class KebabMenuTestStates
{
	public static object Default
	{
		get => new
		{
			IsDisabled = false,
			MenuItems = new List<DropdownMenuItem<int>>
			{
				new() { MenuItemName = "Item 1", Value = 1, MenuIcon = "account", AccentColorIndex = 1, MenuItemSelected = () => {} },
				new() { MenuItemName = "Item 2", Value = 2, MenuIcon = "theme-light-dark", AccentColorIndex = 2, MenuItemSelected = () => {} },
				new() { MenuItemName = "Item 3", Value = 3, MenuIcon = "delete", AccentColorIndex = 3, MenuItemSelected = () => { } }
			}
		};
	}
}
