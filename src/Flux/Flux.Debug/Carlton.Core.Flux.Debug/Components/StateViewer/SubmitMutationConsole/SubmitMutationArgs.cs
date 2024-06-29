namespace Carlton.Core.Flux.Debug.Components.StateViewer.SubmitMutationConsole;

/// <summary>
/// Provides data for the mutation submission event.
/// </summary>
/// <param name="MutationCommand">The mutation command to be submitted.</param>
public sealed record SubmitMutationArgs(object MutationCommand);
