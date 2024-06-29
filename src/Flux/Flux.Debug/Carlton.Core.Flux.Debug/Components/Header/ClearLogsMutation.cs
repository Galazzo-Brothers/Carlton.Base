namespace Carlton.Core.Flux.Debug.Components.Header;

internal sealed class ClearLogsMutation : IFluxStateMutation<FluxDebugState, ClearLogsCommand>
{
	public string StateEvent => FluxDebugStateEvents.LogsCleared.ToString();

	public FluxDebugState Mutate(FluxDebugState originalState, ClearLogsCommand command)
	{
		return originalState with
		{
			LogMessages = new List<FluxDebugLogMessage>(),
			SelectedLogMessageIndex = null,
			SelectedTraceLogMessageIndex = null
		};
	}
}



