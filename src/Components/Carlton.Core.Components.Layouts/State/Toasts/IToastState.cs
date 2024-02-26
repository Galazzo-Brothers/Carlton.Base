using Carlton.Core.Components.Layouts.Toasts;
using Carlton.Core.Components.Toasts;
namespace Carlton.Core.Components.Layouts.State.Toasts;

/// <summary>
/// Represents the interface for managing toast notifications.
/// </summary>
public interface IToastState
{
    /// <summary>
    /// Occurs when a new toast is added.
    /// </summary>
    public event EventHandler<ToastRaisedEventArgs> ToastAdded;

    /// <summary>
    /// Gets the list of toast view models.
    /// </summary>
    public List<ToastViewModel> Toasts { get; }

    /// <summary>
    /// Gets the index of the current toast.
    /// </summary>
    public int ToastsIndex { get; }

    /// <summary>
    /// Raises a new toast with the specified title, message, and type.
    /// </summary>
    /// <param name="title">The title of the toast.</param>
    /// <param name="message">The message of the toast.</param>
    /// <param name="toastType">The type of the toast.</param>
    public void RaiseToast(string title, string message, ToastTypes toastType);
}

