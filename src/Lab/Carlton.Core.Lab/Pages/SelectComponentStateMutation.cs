using Carlton.Core.Lab.Components.ComponentViewer;
using Carlton.Core.Lab.State;
namespace Carlton.Core.Lab.Pages;

internal sealed class SelectComponentStateMutation : IFluxStateMutation<LabState, SelectComponentStateCommand>
{
	public string StateEvent => LabStateEvents.ComponentStateSelected.ToString();

	public LabState Mutate(LabState currentState, SelectComponentStateCommand command)
	{
		//Find the component index
		var componentIndex = currentState.ComponentConfigurations
			.FindIndex(cc => cc.ComponentType.GetDisplayName().ToUpper() == command.ComponentName.ToUpper());

		//Default to first component if not found
		componentIndex = componentIndex == -1 ? 0 : componentIndex; //Default to the first state if not found

		//Find the state index
		var stateIndex = currentState.ComponentConfigurations.ElementAt(componentIndex)
			.ComponentStates.FindIndex(cs => cs.DisplayName.ToUpper() == command.ComponentState.ToUpper());

		//Default to first state if not found
		stateIndex = stateIndex == -1 ? 0 : stateIndex; //Default to the first component if not found

		//Get the selected component and state
		var SelectedComponentConfigurations = currentState.ComponentConfigurations[componentIndex];
		var firstComponentConfigurations = currentState.ComponentConfigurations.FirstOrDefault();

		//selected state parameters
		var parameters = currentState.ComponentConfigurations
							.ElementAt(componentIndex).ComponentStates
							.ElementAt(stateIndex).ComponentParameters;

		//Expand the selected component configuration
		var newComponentConfigurations = currentState.ComponentConfigurations.ToList();
		newComponentConfigurations[componentIndex] = new ComponentConfigurations
		{
			ComponentType = SelectedComponentConfigurations.ComponentType,
			ComponentStates = SelectedComponentConfigurations.ComponentStates,
			IsExpanded = true,
		};

		//Collapse the first component if it is not the selected component
		if (componentIndex != 0)
		{
			newComponentConfigurations[0] = new ComponentConfigurations
			{
				ComponentType = firstComponentConfigurations.ComponentType,
				ComponentStates = firstComponentConfigurations.ComponentStates,
				IsExpanded = false,
			};
		}

		//Return the new state
		return currentState with
		{
			ComponentConfigurations = newComponentConfigurations,
			SelectedComponentIndex = componentIndex,
			SelectedComponentStateIndex = stateIndex,
			SelectedComponentParameters = parameters
		};
	}
}
