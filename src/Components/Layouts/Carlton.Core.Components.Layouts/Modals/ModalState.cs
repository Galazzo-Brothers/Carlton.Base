using Carlton.Core.Components.Modals;
namespace Carlton.Core.Components.Layouts.Modals;

/// <summary>
/// Represents the interface for managing modal state.
/// </summary>
public sealed class ModalState : IModalState
{
	/// <summary>
	/// Occurs when the modal state changes.
	/// </summary>
	public event EventHandler<ModalStateChangedEventArgs> ModalStateChanged;

	/// <summary>
	/// Gets a value indicating whether the modal is currently visible.
	/// </summary>
	public bool IsVisible { get; private set; }

	/// <summary>
	/// Gets the type of the modal.
	/// </summary>
	public ModalTypes ModalType { get; private set; }

	/// <summary>
	/// Gets the view model associated with the modal.
	/// </summary>
	public ModalViewModel Model { get; private set; }

	/// Raises a modal with the specified type and view model.
	/// </summary>
	/// <param name="modalType">The type of the modal.</param>
	/// <param name="modalModel">The view model associated with the modal.</param>
	public void RaiseModal(ModalTypes modalType, ModalViewModel modalViewModel)
	{
		IsVisible = true;
		ModalType = modalType;
		Model = modalViewModel with
		{
			CloseModal = WrapCloseModalCallback(modalViewModel.CloseModal),
			DismissModal = WrapDismissModalCallback(modalViewModel.DismissModal)
		};

		RaiseModalStateChangedEvent();
	}

	private Func<ModalCloseEventArgs, Task> WrapCloseModalCallback(Func<ModalCloseEventArgs, Task> closeModalCallback) =>
		async (args) =>
		{
			IsVisible = false;
			RaiseModalStateChangedEvent();
			await closeModalCallback(args);
		};

	private Func<Task> WrapDismissModalCallback(Func<Task> dismissModalCallback) =>
		async () =>
		{
			IsVisible = false;
			RaiseModalStateChangedEvent();
			await dismissModalCallback();
		};

	private void RaiseModalStateChangedEvent()
	{
		ModalStateChanged?.Invoke(this, new ModalStateChangedEventArgs
		{
			IsVisible = IsVisible,
			ModalType = ModalType,
			ModalModel = Model
		});
	}
}


