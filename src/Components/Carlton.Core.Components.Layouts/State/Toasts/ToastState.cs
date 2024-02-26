using Carlton.Core.Components.Layouts.Toasts;
using Carlton.Core.Components.Toasts;
namespace Carlton.Core.Components.Layouts.State.Toasts;

/// <summary>
/// Represents the state management for toast notifications.
/// </summary>
public sealed class ToastState : IToastState
{
    private readonly Stack<ToastViewModel> _toasts = new();

    /// <inheritdoc/>
    public event EventHandler<ToastRaisedEventArgs> ToastAdded;

    /// <inheritdoc/>
    public List<ToastViewModel> Toasts { get => _toasts.ToList(); }

    /// <inheritdoc/>
    public int ToastsIndex { get; private set; }

    /// <summary>
    /// Raises a new toast notification with the specified title, message, and type.
    /// </summary>
    /// <param name="title">The title of the toast.</param>
    /// <param name="message">The message of the toast.</param>
    /// <param name="toastType">The type of the toast.</param>
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
