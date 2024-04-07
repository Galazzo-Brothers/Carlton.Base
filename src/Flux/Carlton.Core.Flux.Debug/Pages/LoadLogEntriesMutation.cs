namespace Carlton.Core.Flux.Debug.Pages;

internal sealed class LoadLogMessagesMutation : IFluxStateMutation<FluxDebugState, LoadLogMessagesCommand>
{
	public string StateEvent => FluxDebugStateEvents.LoadLogMessages.ToString();

	public FluxDebugState Mutate(FluxDebugState state, LoadLogMessagesCommand command)
	{
		var id = 0;
		return state with { LogMessages = command.LogMessages.Select(log => new FluxDebugLogMessage(id++, log)).ToList() };
	}
}
