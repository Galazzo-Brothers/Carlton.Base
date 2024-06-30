namespace Carlton.Core.Flux.Debug.Components.StateViewer.Viewer;

internal sealed class ChangeSelectedMutationCommandMutation : IFluxStateMutation<FluxDebugState, ChangeSelectedCommandMutationCommand>
{
	public string StateEvent => FluxDebugStateEvents.SelectedMutationCommandChanged.ToString();

	public FluxDebugState Mutate(FluxDebugState fluxState, ChangeSelectedCommandMutationCommand command)
	{
		return fluxState with
		{
			SelectedMutationIndex = command.SelectedMutationCommandIndex,
		};
	}
}
