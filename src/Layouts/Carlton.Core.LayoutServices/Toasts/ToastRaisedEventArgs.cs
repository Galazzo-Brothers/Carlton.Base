namespace Carlton.Core.LayoutServices.Toasts;

/// <summary>
/// Represents the event arguments for toast raised events.
/// </summary>
/// <param name="raisedToast">The toast that was raised.</param>
public record ToastRaisedEventArgs(ToastViewModel RaisedToast);