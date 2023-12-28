using Carlton.Core.Components.Modals;

namespace Carlton.Core.Components.Layouts.State.Modals;

public interface IModalState
{
    public event EventHandler<ModalRaisedEventArgs> ModalRaised;

    public ModalViewModel ModalModel { get; }
    public void RaiseModal<TModal>(ModalViewModel model)
        where TModal : Modal;
}

