namespace Carlton.Core.Lab.State.Mutations;

public class ExpandedMenuItemMutation : FluxStateMutationBase<LabState, SelectMenuExpandedCommand>
{
    public override string StateEvent => LabStateEvents.MenuItemExpandedStateChanged.ToString();

    public override LabState Mutate(LabState state, SelectMenuExpandedCommand input)
    {
        var newComponentStates = state.ComponentStates.ToList();
        var toUpdate = state.ComponentStates[input.SelectedComponentIndex];
        var updated = toUpdate with { IsExpanded = input.IsExpanded };
        newComponentStates[input.SelectedComponentIndex] = updated;
        return state with { ComponentStates = newComponentStates };
    }
}
