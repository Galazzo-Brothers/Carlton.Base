using Carlton.Core.Flux.Debug.Extensions;
using Carlton.Core.Flux.Debug.Layouts;
using Carlton.Core.Flux.Debug.Models.Commands;
using Carlton.Core.Flux.Debug.State;
using Carlton.Core.Flux.Debug.Tests.Common;
using Carlton.Core.Foundation.Test;
using Shouldly;
using Xunit;

namespace Carlton.Core.Flux.Debug.Tests;

public class LoadLogMessagesMutationTests
{
	[Theory, AutoNSubstituteData]
	internal void LoadLogsMutation_MutatesCorrectly(
		IFluxStateWrapper fluxStateWrapper,
		LoadLogMessagesCommand command)
	{
		//Arrange
		var state = new FluxDebugState(FluxTypes.Create<TestState>(), fluxStateWrapper)
		{
			LogMessages = []
		};
		var sut = new LoadLogMessagesMutation();

		//Act
		var mutatedState = sut.Mutate(state, command);

		//Assert
		mutatedState.LogMessages.Count.ShouldBe(command.LogMessages.Count);
	}
}
