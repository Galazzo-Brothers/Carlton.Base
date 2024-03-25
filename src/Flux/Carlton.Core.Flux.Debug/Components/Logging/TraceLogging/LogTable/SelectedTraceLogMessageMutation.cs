namespace Carlton.Core.Flux.Debug.Components.Logging.TraceLogging.LogTable;

internal sealed class SelectedTraceLogMessageMutation : IFluxStateMutation<FluxDebugState, ChangeSelectedTraceLogMessageCommand>
{
	public string StateEvent => FluxDebugStateEvents.SelectedTraceLogMessageChanged.ToString();

	public FluxDebugState Mutate(FluxDebugState state, ChangeSelectedTraceLogMessageCommand command)
	{
		return state with { SelectedTraceLogMessageIndex = command.SelectedTraceLogMessageIndex };
	}
}
