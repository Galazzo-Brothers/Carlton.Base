using Carlton.Core.Components.Modals;
namespace Carlton.Core.Components.Layouts.State.Modals;

public class ModalState : IModalState
{
    public event EventHandler<ModalStateChangedEventArgs> ModalStateChanged;

    public bool IsVisible { get; private set; }
    public ModalTypes ModalType { get; private set; }
    public ModalViewModel ModalModel { get; private set; }


    public void RaiseModal(ModalTypes modalType, ModalViewModel modalViewModel)
    {
        IsVisible = true;
        ModalType = modalType;
        ModalModel = modalViewModel with
        {
            CloseModal = WrapCloseModalCallback(modalViewModel.CloseModal),
            DismissModal = WrapDismissModalCallback(modalViewModel.DismissModal)
        };

        RaiseModalStateChangedEvent();
    }

    private Func<object, Task> WrapCloseModalCallback(Func<object, Task> closeModalCallback) =>
        async (obj) =>
        {
            IsVisible = false;
            RaiseModalStateChangedEvent();
            await closeModalCallback(obj);
        };

    private Func<Task> WrapDismissModalCallback(Func<Task> dismissModalCallback) =>
        async () =>
        {
            IsVisible = false;
            RaiseModalStateChangedEvent();
            await dismissModalCallback();
        };

    private void RaiseModalStateChangedEvent()
    {
        ModalStateChanged?.Invoke(this, new ModalStateChangedEventArgs
        {
            IsVisible = IsVisible,
            ModalType = ModalType,
            ModalModel = ModalModel
        });
    }
}


