namespace Carlton.Core.Lab.State.Mutations;

public class UpdateParametersMutation : FluxStateMutationBase<LabState, UpdateParametersCommand>
{
    public override string StateEvent => LabStateEvents.ParametersUpdated.ToString();

    public override LabState Mutate(LabState currentState, UpdateParametersCommand command)
    {       
        return currentState with { SelectedComponentParameters = command.Parameters };
    }
}
