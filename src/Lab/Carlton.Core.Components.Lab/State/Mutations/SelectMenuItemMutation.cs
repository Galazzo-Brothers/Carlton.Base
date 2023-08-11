using Carlton.Core.Components.Flux.State;

namespace Carlton.Core.Components.Lab.State.Mutations;

public class SelectMenutItemMutation : IFluxStateMutation<LabState, SelectMenuItemCommand>
{
    public string StateEvent => LabStateEvents.MenuItemSelected.ToString();

    public LabState Mutate(LabState currentState, SelectMenuItemCommand command)
    {
        return currentState with
        {
            SelectedComponentState = command.ComponentState,
            SelectedComponentParameters = command.ComponentState.ComponentParameters
        };
    }
}
