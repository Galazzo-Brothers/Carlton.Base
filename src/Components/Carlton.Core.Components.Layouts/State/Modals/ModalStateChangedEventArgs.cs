using Carlton.Core.Components.Modals;
namespace Carlton.Core.Components.Layouts.State.Modals;

/// <summary>
/// Represents the event arguments for modal state change events.
/// </summary>
public sealed record ModalStateChangedEventArgs 
{
    /// <summary>
    /// Gets a value indicating whether the modal is visible.
    /// </summary>
    public required bool IsVisible { get; init; }

    /// <summary>
    /// Gets the type of the modal.
    /// </summary>
    public required ModalTypes ModalType { get; init; }

    /// <summary>
    /// Gets the view model associated with the modal.
    /// </summary>
    public required ModalViewModel ModalModel { get; init; }
}
