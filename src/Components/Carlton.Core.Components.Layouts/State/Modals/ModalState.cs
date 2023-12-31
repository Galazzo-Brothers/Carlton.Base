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

    public void RaiseModal<TModal>(string modalPrompt, string modalMessage, Func<Task> modalDismissedFunc, Func<object, Task> modelClosedFunc)
        where TModal : Modal
    {
        if (!typeof(TModal).IsAssignableTo(typeof(Modal)))
            throw new ArgumentException($"{typeof(TModal)} is not a valid Modal type.");

        ModalModel = new ModalViewModel(modalPrompt, modalMessage, modalDismissedFunc, modelClosedFunc);
        ModalType = typeof(TModal);
        ModalRaised?.Invoke(this, new ModalRaisedEventArgs(typeof(TModal)));
    }

    public void RaiseModal<TModal>(string modalPrompt, string modalMessage)
        where TModal : Modal
    {
        RaiseModal<TModal>(modalPrompt, modalMessage, null, null);
    }
}


