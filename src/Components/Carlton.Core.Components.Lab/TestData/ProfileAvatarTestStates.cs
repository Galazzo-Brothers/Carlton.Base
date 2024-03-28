using Carlton.Core.Components.Dropdowns;
namespace Carlton.Core.Components.Lab.TestData;

internal static class ProfileAvatarTestStates
{
	public static object DefaultState
	{
		get => new
		{
			AvatarImgUrl = "https://www.w3schools.com/w3images/avatar2.png",
			Username = "Stephen",
			DropdownMenuItems = new List<DropdownMenuItem<int>>
				{
					new(){ MenuItemName = "Item 1", Value = 1, MenuIcon = "account", AccentColorIndex = 1, MenuItemSelected = () => { } },
					new(){ MenuItemName = "Item 2", Value = 2, MenuIcon = "theme-light-dark", AccentColorIndex = 2, MenuItemSelected = () => { } },
					new(){ MenuItemName = "Item 3", Value = 3, MenuIcon = "delete", AccentColorIndex = 3, MenuItemSelected = () => { } }
				}
		};
	}
}
