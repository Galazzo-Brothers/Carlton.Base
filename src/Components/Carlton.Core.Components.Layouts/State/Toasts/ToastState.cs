using Carlton.Core.Components.Layouts.Toasts;
using Carlton.Core.Components.Toasts;
namespace Carlton.Core.Components.Layouts.State.Toasts;

public class ToastState : IToastState
{
    public event EventHandler<ToastRaisedEventArgs> ToastAdded;
    public Stack<ToastViewModel> Toasts { get; private set; } = new Stack<ToastViewModel>();

    public void RaiseToast(string title, string message, ToastTypes toastType)
    {
        var toast = new ToastViewModel
        {
            Title = title,
            Message = message,
            ToastType = toastType,
            FadeOutEnabled = true
        };
        Toasts.Push(toast);
        var args = new ToastRaisedEventArgs(toast);
        ToastAdded?.Invoke(this, args);
    }
}
