namespace Carlton.Core.Components.Lab.State.Mutations;

public class SelectMenuItemMutation : IFluxStateMutation<LabState, SelectMenuItemCommand>
{
    public bool IsRefreshMutation => false;
    public string StateEvent => LabStateEvents.MenuItemSelected.ToString();

    public LabState Mutate(LabState currentState, SelectMenuItemCommand command)
    {
        if (!currentState.ComponentStates.Any(_ => _.Equals(command.ComponentState)))
            throw new InvalidOperationException("Only a previously existing NavMenutItem can be selected.");

        return currentState with
        {
            SelectedComponentState = command.ComponentState,
            SelectedComponentParameters = command.ComponentState.ComponentParameters
        };
    }
}
