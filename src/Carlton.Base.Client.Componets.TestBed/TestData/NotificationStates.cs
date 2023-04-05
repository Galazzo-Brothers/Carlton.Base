namespace Carlton.Base.Components.TestBed;

internal static class NotificationStates
{
    public static Dictionary<string, object> NotificationBarFadeOutDisabledStated
    {
        get => new()
        {
            { "IsTestMode", true },
            { "FadeOutEnabled", false },
            { "Top", 0 },
            { "Right", -500 }
        };
    }

    public static Dictionary<string, object> NotificationBarFadeOutEnabledStated
    {
        get => new()
        {
            { "IsTestMode", true },
            { "FadeOutEnabled", true },
            { "Top", 0 },
            { "Right", -500 }
        };
    }

    public static Dictionary<string, object> SuccessFadeOutEnabledState
    {
        get => new()
        {
            {"Title", "Testing" },
            {"Message", "This is a test" },
            {"FadeOutEnabled", true }
        };
    }

    public static Dictionary<string, object> SuccessFadeOutDisabledState
    {
        get => new()
        {
            {"Title", "Testing" },
            {"Message", "This is a test" },
            {"FadeOutEnabled", false }
        };
    }

    public static Dictionary<string, object> InfoState
    {
        get => new()
        {
            {"Title", "Info" },
            {"Message", "Something you should know about" }
        };
    }

    public static Dictionary<string, object> WarningState
    {
        get => new()
        {
            {"Title", "Warning" },
            {"Message", "This is a warning" }
        };
    }

    public static Dictionary<string, object> ErrorState
    {
        get => new()
        {
            {"Title", "Error" },
            {"Message", "Something went wrong" }
        };
    }
}
