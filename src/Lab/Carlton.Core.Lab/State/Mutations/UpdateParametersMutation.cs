namespace Carlton.Core.Lab.State.Mutations;

public class UpdateParametersMutation : FluxStateMutationBase<LabState, UpdateParametersCommand>
{
    public override string StateEvent => LabStateEvents.ParametersUpdated.ToString();

    public override LabState Mutate(LabState currentState, UpdateParametersCommand command)
    {
        var newComponentParameters = new ComponentParameters { ParameterObj = command.Parameters };        
        return currentState with { SelectedComponentParameters = newComponentParameters };
    }
}
