using Carlton.Core.Components.Modals;

namespace Carlton.Core.Components.Layouts.State.Modal;

public interface IModalState
{
    public event EventHandler<ModelRaisedEventArgs> ModalRaised;

    public ModalViewModel ModalModel { get; }
    public void RaiseModal(ModalViewModel model);
}

public class ModelRaisedEventArgs() : EventArgs;
