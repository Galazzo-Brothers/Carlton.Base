namespace Carlton.Core.Lab.State.Mutations;

public class UpdateParametersMutation : IFluxStateMutation<LabState, UpdateParametersCommand>
{
    public bool IsRefreshMutation => false;
    public string StateEvent => LabStateEvents.ParametersUpdated.ToString();

    public LabState Mutate(LabState currentState, UpdateParametersCommand command)
    {
        var newComponentParameters = new ComponentParameters(command.Parameters, ParameterObjectType.ParameterObject);        
        return currentState with { SelectedComponentParameters = newComponentParameters };
    }
}
