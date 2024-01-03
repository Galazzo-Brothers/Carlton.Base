namespace Carlton.Core.Lab.State.Mutations;

public class SelectMenuItemMutation : FluxStateMutationBase<LabState, SelectMenuItemCommand>
{
    public override string StateEvent => LabStateEvents.MenuItemSelected.ToString();

    public override LabState Mutate(LabState currentState, SelectMenuItemCommand command)
    {
        var selectedState = currentState.ComponentStates.ToList()
                                        .ElementAtOrDefault(command.ComponentIndex)
                                        ?.ComponentStates
                                        ?.ElementAtOrDefault(command.ComponentStateIndex);

        var isValidState = command.SelectedComponentState.Equals(selectedState);

        if (!isValidState)
            throw new InvalidOperationException("Only a previously existing NavMenutItem can be selected.");

        return currentState with
        {
            SelectedComponentIndex = command.ComponentIndex,
            SelectedComponentStateIndex = command.ComponentStateIndex,
            SelectedComponentParameters = command.SelectedComponentState.ComponentParameters,
        };
    }
}
