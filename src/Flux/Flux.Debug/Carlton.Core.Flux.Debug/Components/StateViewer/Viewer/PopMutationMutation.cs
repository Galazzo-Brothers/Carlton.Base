namespace Carlton.Core.Flux.Debug.Components.StateViewer.Viewer;

internal class PopMutationMutation(IFluxStateWrapper fluxStateWrapper) : IFluxStateMutation<FluxDebugState, PopMutationCommand>
{
    public string StateEvent => FluxDebugStateEvents.MutationPopped.ToString();

    public FluxDebugState Mutate(FluxDebugState state, PopMutationCommand command)
    {
        fluxStateWrapper.PopMutation();
        return state with { SelectedMutationIndex = fluxStateWrapper.RecordedMutations.Count() - 1 };
    }
}
