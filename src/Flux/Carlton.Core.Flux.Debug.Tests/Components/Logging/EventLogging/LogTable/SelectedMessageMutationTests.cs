using Carlton.Core.Flux.Debug.Components.Logging.EventLogging.LogTable;
using Carlton.Core.Flux.Debug.Extensions;
using Carlton.Core.Flux.Debug.Models.Commands;
using Carlton.Core.Flux.Debug.Models.Common;
using Carlton.Core.Flux.Debug.State;
using Carlton.Core.Flux.Debug.Tests.Common;
using Carlton.Core.Foundation.Test;
using Shouldly;
using Xunit;
namespace Carlton.Core.Flux.Debug.Tests.Components.Logging.EventLogging.LogTable;

public class SelectedMessageMutationTests
{
	[Theory, AutoNSubstituteData]
	internal void ClearLogsMutation_MutatesCorrectly(
		IFluxStateWrapper fluxStateWrapper,
		IReadOnlyList<FluxDebugLogMessage> logMessages,
		ChangeSelectedLogMessageCommand command)
	{
		//Arrange
		var state = new FluxDebugState(FluxTypes.Create<TestState>(), fluxStateWrapper)
		{
			LogMessages = logMessages,
			SelectedLogMessageIndex = -1
		};
		var sut = new SelectedLogMessageMutation();

		//Act
		var mutatedState = sut.Mutate(state, command);

		//Assert
		mutatedState.SelectedLogMessageIndex.ShouldBe(command.SelectedLogMessageIndex);
	}
}
