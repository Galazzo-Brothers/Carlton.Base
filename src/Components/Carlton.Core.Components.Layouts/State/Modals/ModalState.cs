using Carlton.Core.Components.Modals;

namespace Carlton.Core.Components.Layouts.State.Modals;

public class ModalState : IModalState
{
    public event EventHandler<ModalRaisedEventArgs> ModalRaised;

    public ModalViewModel ModalModel { get; private set; }

    public ModalState()
    {
        ModalModel = new ModalViewModel(false, string.Empty, string.Empty, null, null);
    }

    public void RaiseModal<TModal>(ModalViewModel model)
        where TModal : Modal
    {
        ModalModel = model;
        ModalRaised?.Invoke(this, new ModalRaisedEventArgs(typeof(TModal)));
    }
}


