using Carlton.Core.Components.Modals;

namespace Carlton.Core.Components.Layouts.State.Modals;

public class ModalState : IModalState
{
    public event EventHandler<ModalRaisedEventArgs> ModalRaised;

    public ModalViewModel ModalModel { get; private set; }
    public Type ModalType { get; private set; }

    public ModalState()
    {
        ModalModel = new ModalViewModel(string.Empty, string.Empty, null, null);
    }

    public void RaiseModal<TModal>(ModalViewModel model)
        where TModal : Modal
    {
        if (!typeof(TModal).IsAssignableTo(typeof(Modal)))
            throw new ArgumentException($"{typeof(TModal)} is not a valid Modal type.");

        ModalModel = model;
        ModalType = typeof(TModal);
        ModalRaised?.Invoke(this, new ModalRaisedEventArgs(typeof(TModal)));
    }
}


