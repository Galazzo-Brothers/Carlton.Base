using Carlton.Core.Components.Flux.Mutations;

namespace Carlton.Core.Components.Lab.State.Mutations;

public class UpdateParametersMutation<TState, TCommand> : IStateMutation<LabState, LabStateEvents, UpdateParametersCommand>
{
    public LabStateEvents StateEvent => throw new NotImplementedException();

    public IStateStore<LabStateEvents> Mutate(LabState currentState, object sender, UpdateParametersCommand command)
    {
        var newComponentParameters = new ComponentParameters(command.ComponentParameters, ParameterObjectType.ParameterObject);
        return currentState with { SelectedComponentParameters = newComponentParameters };
    }
}
