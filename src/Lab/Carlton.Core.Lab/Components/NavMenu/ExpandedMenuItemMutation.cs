using Carlton.Core.Lab.State;

namespace Carlton.Core.Lab.Components.NavMenu;

public class ExpandedMenuItemMutation : IFluxStateMutation<LabState, SelectMenuExpandedCommand>
{
	public string StateEvent => LabStateEvents.MenuItemExpandedStateChanged.ToString();

	public LabState Mutate(LabState state, SelectMenuExpandedCommand input)
	{
		var newComponentStates = state.ComponentConfigurations.ToList();
		var toUpdate = state.ComponentConfigurations[input.SelectedComponentIndex];
		var updated = toUpdate with { IsExpanded = input.IsExpanded };
		newComponentStates[input.SelectedComponentIndex] = updated;
		return state with { ComponentConfigurations = newComponentStates };
	}
}
