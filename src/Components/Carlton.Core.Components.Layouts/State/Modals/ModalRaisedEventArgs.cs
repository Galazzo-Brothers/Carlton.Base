using Carlton.Core.Components.Modals;
namespace Carlton.Core.Components.Layouts.State.Modals;

public class ModalRaisedEventArgs : EventArgs
{
    public Type ModalType { get; private set; }

    public ModalRaisedEventArgs(Type modalType)
    {
        if (!modalType.IsAssignableTo(typeof(Modal)))
            throw new ArgumentException($"{modalType} is not a valid Modal type.");

        ModalType = modalType;
    }
}
