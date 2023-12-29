using Carlton.Core.Components.Toasts;

namespace Carlton.Core.Components.Layouts.State.Toasts;

public class ToastRaisedEventArgs(ToastViewModel raisedToast) : EventArgs
{
    public ToastViewModel RaisedToast { get; set; } = raisedToast;
}