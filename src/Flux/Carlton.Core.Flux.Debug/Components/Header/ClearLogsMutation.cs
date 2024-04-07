namespace Carlton.Core.Flux.Debug.Components.Header;

internal sealed class ClearLogsMutation : IFluxStateMutation<FluxDebugState, LogsClearedCommand>
{
	public string StateEvent => FluxDebugStateEvents.LogsCleared.ToString();

	public FluxDebugState Mutate(FluxDebugState originalState, LogsClearedCommand command)
	{
		return originalState with { LogMessages = new List<FluxDebugLogMessage>() };
	}
}



