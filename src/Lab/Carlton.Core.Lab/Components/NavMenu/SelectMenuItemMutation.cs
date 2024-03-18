using Carlton.Core.Lab.State;
namespace Carlton.Core.Lab.Components.NavMenu;

internal sealed class SelectMenuItemMutation : IFluxStateMutation<LabState, SelectMenuItemCommand>
{
	public string StateEvent => LabStateEvents.MenuItemSelected.ToString();

	public LabState Mutate(LabState currentState, SelectMenuItemCommand command)
	{
		var selectedState = currentState.ComponentConfigurations.ToList()
										.ElementAtOrDefault(command.ComponentIndex)
										?.ComponentStates
										?.ElementAtOrDefault(command.ComponentStateIndex);

		return currentState with
		{
			SelectedComponentIndex = command.ComponentIndex,
			SelectedComponentStateIndex = command.ComponentStateIndex,
			SelectedComponentParameters = selectedState.ComponentParameters
		};
	}
}
