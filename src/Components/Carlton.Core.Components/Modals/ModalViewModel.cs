namespace Carlton.Core.Components.Modals;

public class ModalViewModel(
    bool isVisible,
    string modalPropmpt,
    string modalMessage,
    Func<Task> modalDismissedFunc,
    Func<object, Task> modalClosedFunc)
{
    private readonly Func<Task> _modalDismissedFunc = modalDismissedFunc;
    private readonly Func<object, Task> _modalClosedFunc = modalClosedFunc;

    public bool IsVisible { get; private set; } = isVisible;
    public string ModalPrompt { get; init; } = modalPropmpt;
    public string ModalMessage { get; init; } = modalMessage;

    public async Task DismissModal()
    {
        IsVisible = false;
        await _modalDismissedFunc?.Invoke();
    }

    public async Task CloseModal(object args)
    {
        IsVisible = false;
        await _modalClosedFunc?.Invoke(args);
    }
}

