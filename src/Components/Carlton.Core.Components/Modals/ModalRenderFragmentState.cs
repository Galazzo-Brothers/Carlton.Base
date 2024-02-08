using Microsoft.AspNetCore.Components;

namespace Carlton.Core.Components.Modals;

public record ModalRenderFragmentState
{
    public required string ModalPrompt { get; init; }
    public required string ModalMessage { get; init; }
    public required Func<ModalClosedArgs, Task> CloseModalCallback { get; init; }
    public required Func<Task> DissmissCallback { get; init; }
}
