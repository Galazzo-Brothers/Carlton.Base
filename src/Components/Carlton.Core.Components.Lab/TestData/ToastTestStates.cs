using Carlton.Core.Components.Toasts;
namespace Carlton.Core.Components.Lab.TestData;

internal static class ToastTestStates
{
    public static object Success
    {
        get => new
        {
            Title = "Test",
            Message = "Message",
            ToastType = ToastTypes.Success
        };
    }

    public static object Info
    {
        get => new
        {
            Title = "Test",
            Message = "Message",
            ToastType = ToastTypes.Info
        };
    }

    public static object Warning
    {
        get => new
        {
            Title = "Test",
            Message = "Message",
            ToastType = ToastTypes.Warning
        };
    }

    public static object Error
    {
        get => new
        {
            Title = "Test",
            Message = "Message",
            ToastType = ToastTypes.Error
        };
    }
}
