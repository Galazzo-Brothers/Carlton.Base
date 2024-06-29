using Carlton.Core.Flux.Debug.Components.Header;
using Carlton.Core.Flux.Debug.Extensions;
using Carlton.Core.Flux.Debug.Models.Commands;
using Carlton.Core.Flux.Debug.Models.Common;
using Carlton.Core.Flux.Debug.State;
using Carlton.Core.Flux.Debug.Tests.Common;
using Carlton.Core.Foundation.Test;
using Shouldly;
using Xunit;

namespace Carlton.Core.Flux.Debug.Tests.Components.Header;

public class ClearLogsMutationTests
{
	[Theory, AutoNSubstituteData]
	internal void ClearLogsMutation_MutatesCorrectly(
		IFluxStateWrapper fluxStateWrapper,
		IReadOnlyList<FluxDebugLogMessage> logMessages,
		ClearLogsCommand command)
	{
		//Arrange
		var state = new FluxDebugState(FluxTypes.Create<TestState>(), fluxStateWrapper)
		{
			LogMessages = logMessages
		};
		var sut = new ClearLogsMutation();

		//Act
		var mutatedState = sut.Mutate(state, command);

		//Assert
		mutatedState.LogMessages.ShouldBeEmpty();
	}
}