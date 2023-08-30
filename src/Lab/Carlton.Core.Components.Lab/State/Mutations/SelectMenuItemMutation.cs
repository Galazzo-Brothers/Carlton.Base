namespace Carlton.Core.Components.Lab.State.Mutations;

public class SelectMenuItemMutation : IFluxStateMutation<LabState, SelectMenuItemCommand>
{
    public bool IsRefreshMutation => false;
    public string StateEvent => LabStateEvents.MenuItemSelected.ToString();

    public LabState Mutate(LabState currentState, SelectMenuItemCommand command)
    {
        var index = currentState.ComponentStates.ToList().FindIndex(_ => _.Equals(command.ComponentState));

        if (index == -1)
            throw new InvalidOperationException("Only a previously existing NavMenutItem can be selected.");

        return currentState with
        {
            SelectedComponentIndex = index,
            SelectedComponentParameters = command.ComponentState.ComponentParameters
        };
    }
}
