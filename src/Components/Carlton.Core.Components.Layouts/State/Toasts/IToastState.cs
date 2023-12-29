using Carlton.Core.Components.Toasts;
namespace Carlton.Core.Components.Layouts.State.Toasts;

public interface IToastState
{
    public event EventHandler<ToastRaisedEventArgs> ToastAdded;
    public Stack<ToastViewModel> Toasts { get; }
    public int LatestIndex { get; }
    public void AddToast(string Title, string Message, ToastTypes ToastType);
}

