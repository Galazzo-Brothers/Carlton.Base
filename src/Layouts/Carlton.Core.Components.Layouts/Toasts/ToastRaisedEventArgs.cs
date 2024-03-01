namespace Carlton.Core.Components.Layouts.Toasts;

/// <summary>
/// Represents the event arguments for toast raised events.
/// </summary>
/// <remarks>
/// The properties of this record are initialized upon creation and cannot be modified after instantiation.
/// </remarks>
/// <param name="raisedToast">The toast that was raised.</param>
public record ToastRaisedEventArgs(ToastViewModel RaisedToast);