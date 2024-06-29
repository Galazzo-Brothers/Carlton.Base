using Carlton.Core.Flux.Debug.Components.StateViewer.SubmitMutationConsole;
using Carlton.Core.Flux.Debug.Extensions;
using Carlton.Core.Flux.Debug.Models.Commands;
using Carlton.Core.Flux.Debug.Models.Common;
using Carlton.Core.Flux.Debug.State;
using Carlton.Core.Flux.Debug.Tests.Common;
using Carlton.Core.Foundation.Test;
using NSubstitute;
using Xunit;
namespace Carlton.Core.Flux.Debug.Tests.Components.StateViewer.SubmitMutationConsole;

public class SubmitMutationCommandMutationTests
{
	[Theory, AutoNSubstituteData]
	internal void SubmitMutationCommandMutation_MutatesCorrectly(
		IFluxStateWrapper fluxStateWrapper,
		IReadOnlyList<FluxDebugLogMessage> logMessages,
		SubmitMutationCommand command)
	{
		//Arrange
		var state = new FluxDebugState(FluxTypes.Create<TestState>(), fluxStateWrapper)
		{
			LogMessages = logMessages
		};
		var sut = new SubmitMutationCommandMutation(fluxStateWrapper);

		//Act
		var mutatedState = sut.Mutate(state, command);

		//Assert
		fluxStateWrapper.Received().ApplyMutation(Arg.Is<object>(x => x.Equals(command.MutationCommandToSubmit)));
	}
}
