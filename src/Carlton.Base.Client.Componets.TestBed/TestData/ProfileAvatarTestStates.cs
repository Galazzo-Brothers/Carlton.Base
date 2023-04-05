namespace Carlton.Base.Components.TestBed;

internal static class ProfileAvatarTestStates
{
    public static Dictionary<string, object> DefaultState
    {
        get => new()
        {
            { "AvatarImgUrl", "https://www.w3schools.com/w3images/avatar2.png" },
            { "UserName", "Stephen" }
        };
    }
}
