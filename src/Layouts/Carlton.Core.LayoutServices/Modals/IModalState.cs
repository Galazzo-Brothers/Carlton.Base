namespace Carlton.Core.LayoutServices.Modals;

/// <summary>
/// Represents the interface for managing modal state.
/// </summary>
public interface IModalState
{
	/// <summary>
	/// Occurs when the modal state changes.
	/// </summary>
	public event EventHandler<ModalStateChangedEventArgs> ModalStateChanged;

	/// <summary>
	/// Gets a value indicating whether the modal is currently visible.
	/// </summary>
	public bool IsVisible { get; }

	// <summary>
	/// Gets the type of the modal.
	/// </summary>
	public string ModalType { get; }

	/// <summary>
	/// Gets the view model associated with the modal.
	/// </summary>
	public ModalViewModel Model { get; }

	/// <summary>
	/// Raises a modal with the specified type and view model.
	/// </summary>
	/// <param name="modalType">The type of the modal.</param>
	/// <param name="modalModel">The view model associated with the modal.</param>
	public void RaiseModal(string modalType, ModalViewModel modalModel);
}
