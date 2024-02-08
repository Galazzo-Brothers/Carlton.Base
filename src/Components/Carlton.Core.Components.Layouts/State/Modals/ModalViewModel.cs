using Carlton.Core.Components.Modals;

namespace Carlton.Core.Components.Layouts.State.Modals;

public record ModalViewModel
{
    public required string ModalPrompt { get; init; }
    public required string ModalMessage { get; init; }
    public required Func<ModalCloseEventArgs, Task> CloseModal { get; init; }
    public required Func<Task> DismissModal { get; init; }   
}

