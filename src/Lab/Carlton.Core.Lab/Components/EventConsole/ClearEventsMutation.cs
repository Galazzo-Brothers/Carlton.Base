using Carlton.Core.Lab.State;

namespace Carlton.Core.Lab.Components.EventConsole;

public class ClearEventsMutation : IFluxStateMutation<LabState, ClearEventsCommand>
{
	public string StateEvent => LabStateEvents.EventsCleared.ToString();

	public LabState Mutate(LabState originalState, ClearEventsCommand command)
	{
		return originalState with { ComponentEvents = new List<ComponentRecordedEvent>() };
	}
}



