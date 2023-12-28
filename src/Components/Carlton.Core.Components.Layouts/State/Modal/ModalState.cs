using Carlton.Core.Components.Modals;

namespace Carlton.Core.Components.Layouts.State.Modal;

public class ModalState : IModalState
{
    public event EventHandler<ModelRaisedEventArgs> ModalRaised;

    public ModalViewModel ModalModel { get; private set; }

    public ModalState()
    {
        ModalModel = new ModalViewModel(false, string.Empty, string.Empty, null, null);
    }

    public void RaiseModal(ModalViewModel model)
    {
        ModalModel = model;
        ModalRaised?.Invoke(this, new ModelRaisedEventArgs());
    }
}


