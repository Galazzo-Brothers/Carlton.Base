using Carlton.Core.Flux.Debug.Components.Logging.EventLogging;

namespace Carlton.Core.Flux.Debug.State.Mutations;

public class EventLogPageIndexMutation : IFluxStateMutation<FluxDebugState, ChangeEventLogPageCommand>
{
	public string StateEvent => FluxDebugStateEvents.EventLogPageChanged.ToString();

	public FluxDebugState Mutate(FluxDebugState state, ChangeEventLogPageCommand command)
	{
		var updatedTableState = state.EventLogTableState with { CurrentPage = command.SelectedPageIndex };
		return state with { EventLogTableState = updatedTableState };
	}
}
