namespace Carlton.Core.Flux.Debug.Pages;

internal sealed class LoadLogMessagesMutation : IFluxStateMutation<FluxDebugState, LoadLogMessagesCommand>
{
	public string StateEvent => FluxDebugStateEvents.LoadLogMessages.ToString();

	public FluxDebugState Mutate(FluxDebugState state, LoadLogMessagesCommand command)
	{
		return state with { LogMessages = command.LogMessages };
	}
}
