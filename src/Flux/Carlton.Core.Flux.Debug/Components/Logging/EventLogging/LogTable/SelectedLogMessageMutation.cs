namespace Carlton.Core.Flux.Debug.Components.Logging.EventLogging.LogTable;

public sealed class SelectedLogMessageMutation : IFluxStateMutation<FluxDebugState, ChangeSelectedLogMessageCommand>
{
	public string StateEvent => FluxDebugStateEvents.SelectedLogMessageChanged.ToString();

	public FluxDebugState Mutate(FluxDebugState state, ChangeSelectedLogMessageCommand command)
	{
		return state with { SelectedLogMessageIndex = command.SelectedLogMessageIndex };
	}
}
