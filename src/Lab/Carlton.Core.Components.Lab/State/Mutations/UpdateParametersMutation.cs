using Carlton.Core.Components.Flux.State;

namespace Carlton.Core.Components.Lab.State.Mutations;

public class UpdateParametersMutation : IFluxStateMutation<LabState, UpdateParametersCommand>
{
    public string StateEvent => LabStateEvents.ParametersUpdated.ToString();

    public LabState Mutate(LabState currentState, UpdateParametersCommand command)
    {
        var newComponentParameters = new ComponentParameters(command.ComponentParameters, ParameterObjectType.ParameterObject);
        return currentState with { SelectedComponentParameters = newComponentParameters };
    }
}
