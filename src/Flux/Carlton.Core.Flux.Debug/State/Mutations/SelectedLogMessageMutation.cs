namespace Carlton.Core.Flux.Debug.State.Mutations;

public class SelectedLogMessageMutation : IFluxStateMutation<FluxDebugState, ChangeSelectedLogMessageCommand>
{
	public string StateEvent => FluxDebugStateEvents.SelectedLogMessageChanged.ToString();

	public FluxDebugState Mutate(FluxDebugState state, ChangeSelectedLogMessageCommand command)
	{
		return state with { SelectedLogMessageIndex = command.SelectedLogMessageIndex };
	}
}
