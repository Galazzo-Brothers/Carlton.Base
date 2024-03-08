namespace Carlton.Core.Lab.State.Mutations;

public class UpdateParametersMutation : IFluxStateMutation<LabState, UpdateParametersCommand>
{
    public string StateEvent => LabStateEvents.ParametersUpdated.ToString();

    public LabState Mutate(LabState currentState, UpdateParametersCommand command)
    {       
        return currentState with { SelectedComponentParameters = command.Parameters };
    }
}
