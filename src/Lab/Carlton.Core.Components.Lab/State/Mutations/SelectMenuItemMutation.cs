using Carlton.Core.Components.Flux.Mutations;

namespace Carlton.Core.Components.Lab.State.Mutations;

public class SelectMenutItemMutation : IStateMutation<LabState, LabStateEvents, SelectMenuItemCommand>
{
    public LabStateEvents StateEvent => throw new NotImplementedException();

    public IStateStore<LabStateEvents> Mutate(LabState currentState, object sender, SelectMenuItemCommand command)
    {
        return currentState with { SelectedComponentState = command.ComponentState };
    }
}
