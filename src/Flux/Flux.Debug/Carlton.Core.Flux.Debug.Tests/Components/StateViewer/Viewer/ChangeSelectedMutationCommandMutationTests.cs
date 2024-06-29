using Carlton.Core.Flux.Debug.Components.StateViewer.Viewer;
using Carlton.Core.Flux.Debug.Extensions;
using Carlton.Core.Flux.Debug.Models.Commands;
using Carlton.Core.Flux.Debug.Models.Common;
using Carlton.Core.Flux.Debug.State;
using Carlton.Core.Flux.Debug.Tests.Common;
using Carlton.Core.Foundation.Test;
using Shouldly;
using Xunit;

namespace Carlton.Core.Flux.Debug.Tests.Components.StateViewer.Viewer;

public class ChangeSelectedMutationCommandMutationTests
{
	[Theory, AutoNSubstituteData]
	internal void ChangeSelectedMutationCommandMutation_MutatesCorrectly(
		IFluxStateWrapper fluxStateWrapper,
		IReadOnlyList<FluxDebugLogMessage> logMessages,
		ChangeSelectedCommandMutationCommand command)
	{
		//Arrange
		var state = new FluxDebugState(FluxTypes.Create<TestState>(), fluxStateWrapper)
		{
			LogMessages = logMessages,
			SelectedMutationIndex = -1
		};
		var sut = new ChangeSelectedMutationCommandMutation();

		//Act
		var mutatedState = sut.Mutate(state, command);

		//Assert
		mutatedState.SelectedMutationIndex.ShouldBe(command.SelectedMutationCommandIndex);
	}
}
