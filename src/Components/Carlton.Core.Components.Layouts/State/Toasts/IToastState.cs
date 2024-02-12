using Carlton.Core.Components.Layouts.Toasts;
using Carlton.Core.Components.Toasts;
namespace Carlton.Core.Components.Layouts.State.Toasts;

public interface IToastState
{
    public event EventHandler<ToastRaisedEventArgs> ToastAdded;
    public Stack<ToastViewModel> Toasts { get; }
    public void RaiseToast(string title, string message, ToastTypes toastType);
}

