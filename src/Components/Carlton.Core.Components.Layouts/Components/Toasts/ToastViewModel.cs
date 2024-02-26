using Carlton.Core.Components.Toasts;
namespace Carlton.Core.Components.Layouts.Toasts;

/// <summary>
/// Represents a view model for toast notifications.
/// </summary>
public sealed class ToastViewModel
{
    /// <summary>
    /// Gets the unique identifier of the toast notification.
    /// </summary>
    public required int Id { get; init; }

    /// <summary>
    /// Gets the title of the toast notification.
    /// </summary>
    public required string Title { get; init; }

    /// <summary>
    /// Gets the message content of the toast notification.
    /// </summary>
    public required string Message { get; init; }

    /// <summary>
    /// Gets the type of toast notification (e.g., Success, Error, Warning).
    /// </summary>
    public required ToastTypes ToastType { get; init; }

    /// <summary>
    /// Gets whether fade-out effect is enabled for the toast notification.
    /// </summary>
    public required bool FadeOutEnabled { get; init; }

    /// <summary>
    /// Gets a value indicating whether the toast notification has been dismissed.
    /// </summary>
    public bool IsDismissed { get; private set; }

    /// <summary>
    /// Marks the toast notification as dismissed.
    /// </summary>
    public void MarkAsDismissed()
        => IsDismissed = true;
}
