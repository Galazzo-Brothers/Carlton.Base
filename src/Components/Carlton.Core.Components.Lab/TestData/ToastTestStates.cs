using Carlton.Core.Components.Tables;
using Carlton.Core.Components.Toasts;

namespace Carlton.Core.Components.Lab.TestData;

public static class ToastTestStates
{
    public static Dictionary<string, object> Success
    {
        get => new()
        {
            { nameof(Toast.Title), "Test" },
            { nameof(Toast.Message), "Message" },
            { nameof(Toast.ToastType), ToastTypes.Success }
        };
    }

    public static Dictionary<string, object> Info
    {
        get => new()
        {
            { nameof(Toast.Title), "Test" },
            { nameof(Toast.Message), "Message" },
            { nameof(Toast.ToastType), ToastTypes.Info }
        };
    }

    public static Dictionary<string, object> Warning
    {
        get => new()
        {
            { nameof(Toast.Title), "Test" },
            { nameof(Toast.Message), "Message" },
            { nameof(Toast.ToastType), ToastTypes.Warning }
        };
    }

    public static Dictionary<string, object> Error
    {
        get => new()
        {
            { nameof(Toast.Title), "Test" },
            { nameof(Toast.Message), "Message" },
            { nameof(Toast.ToastType), ToastTypes.Error }
        };
    }
}
