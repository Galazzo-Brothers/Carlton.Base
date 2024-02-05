using Carlton.Core.Components.Dropdowns;
namespace Carlton.Core.Components.Library.Lab.TestData;

internal static class ProfileAvatarTestStates
{
    public static Dictionary<string, object> DefaultState
    {
        get => new()
        {
            { nameof(ProfileAvatar.AvatarImgUrl), "https://www.w3schools.com/w3images/avatar2.png" },
            { nameof(ProfileAvatar.Username), "Stephen" },
            { nameof(ProfileAvatar.DropdownMenuItems), new List<DropdownMenuItem<int>>
            {
                new("Item 1", 1, "account", 1, () => { }),
                new("Item 2", 2, "theme-light-dark", 2, () => { }),
                new("Item 3", 3, "delete", 3, () => { })
            } }
        };
    }
}
