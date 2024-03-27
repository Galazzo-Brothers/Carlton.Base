namespace Carlton.Core.LayoutServices.Modals;

/// <summary>
/// Represents the state for rendering a modal component.
/// </summary>
public sealed record ModalViewModel
{
	/// <summary>
	/// Gets the prompt message for the modal.
	/// </summary>
	public required string Prompt { get; init; }

	/// <summary>
	/// Gets the message displayed in the modal.
	/// </summary>
	public required string Message { get; init; }

	/// <summary>
	/// Gets the function to handle closing the modal.
	/// </summary>
	public required Func<ModalCloseEventArgs, Task> CloseModal { get; init; }

	/// <summary>
	/// Gets the function to handle dismissing the modal.
	/// </summary>
	public required Func<Task> DismissModal { get; init; }
}

