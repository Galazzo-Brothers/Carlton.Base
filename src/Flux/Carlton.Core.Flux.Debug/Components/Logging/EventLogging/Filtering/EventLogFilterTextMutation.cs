namespace Carlton.Core.Flux.Debug.Components.Logging.EventLogging.Filtering;

internal sealed class EventLogFilterTextMutation : IFluxStateMutation<FluxDebugState, ChangeEventLogFilterTextCommand>
{
	public string StateEvent => FluxDebugStateEvents.EventLogFilterTextChanged.ToString();

	public FluxDebugState Mutate(FluxDebugState state, ChangeEventLogFilterTextCommand command)
	{
		var updatedFilterState = state.EventLogViewerFilterState with { FilterText = command.FilterText.ToLower() };
		return state with { EventLogViewerFilterState = updatedFilterState };
	}
}
