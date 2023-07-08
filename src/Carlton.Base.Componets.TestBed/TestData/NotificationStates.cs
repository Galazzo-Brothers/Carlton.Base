namespace Carlton.Base.Components.TestBed;

internal static class NotificationStates
{
    public static object NotificationBarFadeOutDisabledStated
    {
        get => new
        {
            IsTestMode = true,
            FadeOutEnabled = false,
            Top = 0,
            Right = -500
        };
    }

    public static object NotificationBarFadeOutEnabledStated
    {
        get => new
        {
            IsTestMode = true,
            FadeOutEnabled = true,
            Top = 0,
            Right = -500
        };
    }

    public static object SuccessFadeOutEnabledState
    {
        get => new
        {
            Title = "Testing",
            Message = "This is a test",
            FadeOutEnabled = true
        };
    }

    public static object SuccessFadeOutDisabledState
    {
        get => new
        {
            Title = "Testing",
            Message = "This is a test",
            FadeOutEnabled = false
        };
    }

    public static object InfoState
    {
        get => new
        {
            Title = "Info",
            Message = "Something you should know about"
        };
    }

    public static object WarningState
    {
        get => new
        {
            Title = "Warning",
            Message = "This is a warning"
        };
    }

    public static object ErrorState
    {
        get => new
        {
            Title = "Error",
            Message = "Something went wrong"
        };
    }
}
