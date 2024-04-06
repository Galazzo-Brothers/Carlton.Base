namespace Carlton.Core.Flux.Debug.Components.Logging.EventLogging.LogTable;

internal sealed class SelectedLogMessageMutation : IFluxStateMutation<FluxDebugState, ChangeSelectedLogMessageCommand>
{
	public string StateEvent => FluxDebugStateEvents.SelectedLogMessageChanged.ToString();

	public FluxDebugState Mutate(FluxDebugState state, ChangeSelectedLogMessageCommand command)
	{
		return state with { SelectedLogMessage = command.SelectedLogMessage };
	}
}
