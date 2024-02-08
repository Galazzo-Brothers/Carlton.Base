using Carlton.Core.Components.Modals;
namespace Carlton.Core.Components.Layouts.State.Modals;

public interface IModalState
{
    public event EventHandler<ModalStateChangedEventArgs> ModalStateChanged;

    public bool IsVisible { get; }
    public ModalTypes ModalType { get; }
    public ModalViewModel ModalModel { get; }

    public void RaiseModal(ModalTypes modalType, ModalViewModel modalModel);
}
