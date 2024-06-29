namespace Carlton.Core.Flux.Debug.Components.StateViewer.SubmitMutationConsole;

internal sealed class SubmitMutationCommandMutation(IFluxStateWrapper fluxStateWrapper) : IFluxStateMutation<FluxDebugState, SubmitMutationCommand>
{
	public string StateEvent => FluxDebugStateEvents.NewMutationSubmitted.ToString();

	public FluxDebugState Mutate(FluxDebugState state, SubmitMutationCommand command)
	{
		fluxStateWrapper.ApplyMutation((dynamic)command.MutationCommandToSubmit);
		return state;
	}
}
