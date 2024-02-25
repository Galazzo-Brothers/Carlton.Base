using Carlton.Core.Components.Layouts.Toasts;
using Carlton.Core.Components.Toasts;
namespace Carlton.Core.Components.Layouts.State.Toasts;

public class ToastState : IToastState
{
    private readonly Stack<ToastViewModel> _toasts = new();

    public event EventHandler<ToastRaisedEventArgs> ToastAdded;
    public List<ToastViewModel> Toasts { get => _toasts.ToList(); }
    public int ToastsIndex { get; private set; }

    public void RaiseToast(string title, string message, ToastTypes toastType)
    {
        var toast = new ToastViewModel
        {
            Id = ToastsIndex,
            Title = title,
            Message = message,
            ToastType = toastType,
            FadeOutEnabled = true
        };
        _toasts.Push(toast);
        var args = new ToastRaisedEventArgs(toast);
        ToastAdded?.Invoke(this, args);
        ToastsIndex++;
    }
}
