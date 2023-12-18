namespace Carlton.Core.Components.Library.Lab.TestData;
internal static class ProfileAvatarTestStates
{
    public static object DefaultState
    {
        get => new
        {
            AvatarImgUrl = "https://www.w3schools.com/w3images/avatar2.png",
            UserName = "Stephen",
            DropdownMenuItems = new List<DropdownMenuItem<int>>
            {
                new("Item 1", 1, "account", 1),
                new("Item 2", 2, "theme-light-dark", 2),
                new("Item 3", 3, "delete", 3)
            }
        };
    }
}
