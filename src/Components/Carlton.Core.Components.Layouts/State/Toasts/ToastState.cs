using Carlton.Core.Components.Toasts;
namespace Carlton.Core.Components.Layouts.State.Toasts;

public class ToastState : IToastState
{
    public event EventHandler<ToastRaisedEventArgs> ToastAdded;
    public Stack<ToastViewModel> Toasts { get; private set; } = new Stack<ToastViewModel>();

    public int LatestIndex { get => Toasts.Count != 0 ? Toasts.Peek().ToastIndex : 0; }

    public void RaiseToast(string Title, string Message, ToastTypes ToastType)
    {
        var toastIndex = LatestIndex + 1;
        var toast = new ToastViewModel(toastIndex, Title, Message, ToastType);
        Toasts.Push(toast);
        var args = new ToastRaisedEventArgs(toast);
        ToastAdded?.Invoke(this, args);
    }
}
