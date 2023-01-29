namespace Carlton.Base.Components.TestBed;

internal static class NotificationStates
{
    public static Dictionary<string, object> SuccessState()
    {
        return new Dictionary<string, object>
        {
            {"Title", "Testing" },
            {"Message", "This is a test" }
        };
    }

    public static Dictionary<string, object> InfoState()
    {
        return new Dictionary<string, object>
        {
            {"Title", "Info" },
            {"Message", "Something you should know about" }
        };
    }

    public static Dictionary<string, object> WarningState()
    {
        return new Dictionary<string, object>
        {
            {"Title", "Warning" },
            {"Message", "This is a warning" }
        };
    }

    public static Dictionary<string, object> FailureState()
    {
        return new Dictionary<string, object>
        {
            {"Title", "Error" },
            {"Message", "Something went wrong" }
        };
    }
}
