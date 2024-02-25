using Carlton.Core.Components.Modals;
namespace Carlton.Core.Components.Layouts.State.Modals;

public sealed record ModalStateChangedEventArgs 
{
    public required bool IsVisible { get; init; }
    public required ModalTypes ModalType { get; init; }
    public required ModalViewModel ModalModel { get; init; }
}
