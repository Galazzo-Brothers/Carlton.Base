namespace Carlton.Core.LayoutServices.Modals;

/// <summary>
/// Represents event data for the modal close event.
/// </summary>
/// <param name="UserConfirmed">Gets a value indicating whether the modal was closed by user confirmation.</param>
public sealed record ModalCloseEventArgs(bool UserConfirmed);
