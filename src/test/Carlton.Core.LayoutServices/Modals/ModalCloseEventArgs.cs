namespace Carlton.Core.LayoutServices.Modals;

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
