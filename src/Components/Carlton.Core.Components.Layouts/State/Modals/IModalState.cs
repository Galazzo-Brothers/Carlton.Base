using Carlton.Core.Components.Modals;
namespace Carlton.Core.Components.Layouts.State.Modals;

public interface IModalState
{
    public event EventHandler<ModalRaisedEventArgs> ModalRaised;

    public ModalViewModel ModalModel { get; }
    public Type ModalType { get; }
    public void RaiseModal<TModal>(string modalPrompt, string modalMessage)
        where TModal : Modal;
    public void RaiseModal<TModal>(string modalPrompt, string modalMessage, Func<Task> modalDismissedFunc, Func<object, Task> modelClosedFunc)
        where TModal : Modal;
}
