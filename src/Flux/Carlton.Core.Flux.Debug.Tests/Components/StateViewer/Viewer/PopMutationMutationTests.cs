using Carlton.Core.Flux.Debug.Components.StateViewer.Viewer;
using Carlton.Core.Flux.Debug.Extensions;
using Carlton.Core.Flux.Debug.Models.Commands;
using Carlton.Core.Flux.Debug.Models.Common;
using Carlton.Core.Flux.Debug.State;
using Carlton.Core.Flux.Debug.Tests.Common;
using Carlton.Core.Foundation.Test;
using NSubstitute;
using Shouldly;
using Xunit;
namespace Carlton.Core.Flux.Debug.Tests.Components.StateViewer.Viewer;

public class PopMutationMutationTests
{
	[Theory, AutoNSubstituteData]
	internal void ChangeSelectedMutationCommandMutation_MutatesCorrectly(
		IFluxStateWrapper fluxStateWrapper,
		IReadOnlyList<FluxDebugLogMessage> logMessages,
		PopMutationCommand command,
		IEnumerable<RecordedMutation> recordedMutations)
	{
		//Arrange
		fluxStateWrapper.RecordedMutations.Returns(recordedMutations);
		var expectedMutationIndex = recordedMutations.Count() - 1;
		var state = new FluxDebugState(FluxTypes.Create<TestState>(), fluxStateWrapper)
		{
			LogMessages = logMessages,
			SelectedMutationIndex = -1
		};
		var sut = new PopMutationMutation(fluxStateWrapper);

		//Act
		var mutatedState = sut.Mutate(state, command);

		//Assert
		fluxStateWrapper.Received().PopMutation();
		mutatedState.SelectedMutationIndex.ShouldBe(expectedMutationIndex);
	}
}
