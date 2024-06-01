namespace Carlton.Core.Flux.Debug.Components.StateViewer;

/// <summary>
/// Represents arguments for the event when the selected mutation command changes.
/// </summary>
/// <param name="SelectedMutationCommandIndex">The index of the selected mutation.</param>
public sealed record SelectedMutationCommandChangedArgs(int SelectedMutationCommandIndex);
