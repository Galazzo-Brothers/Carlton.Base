namespace Carlton.Core.Components.Modals;

/// <summary>
/// Represents event data for the modal close event.
/// </summary>
public sealed record ModalCloseEventArgs
(
    /// <summary>
    /// Gets a value indicating whether the modal was closed by user confirmation.
    /// </summary>
    bool UserConfirmed 
);
