using Carlton.Core.Components.Flux.Tests.Common.Extensions;
using Carlton.Core.Flux.Contracts;
using Carlton.Core.Flux.Internals.Errors;
using Carlton.Core.Flux.Internals.State;
using Carlton.Core.Foundation.Test;
using NSubstitute.ExceptionExtensions;
namespace Carlton.Core.Flux.Tests.Internals.StateTests;

public class StateTests
{
	[Theory, AutoNSubstituteData]
	internal async Task FluxState_MutateState_UpdatesState(
		[Frozen] IServiceProvider provider,
		TestMutation mutation,
		FluxState<TestState> sut,
		TestCommand command)
	{
		//Arrange
		var eventRaised = false;
		var actualStateEventRaised = string.Empty;
		var expectedState = mutation.Mutate(sut.CurrentState, command);
		provider.SetupServiceProvider<IFluxStateMutation<TestState, TestCommand>>(mutation);
		sut.StateChanged += (args) =>
		{
			eventRaised = true;
			actualStateEventRaised = args.StateEvent;
			return Task.CompletedTask;
		};

		//Act
		await sut.ApplyMutationCommand(command);

		//Assert
		sut.CurrentState.ShouldBe(expectedState);
		sut.RecordedMutations.ShouldNotBeEmpty();
		sut.RecordedMutations.First().Command.ShouldBe(command);
		sut.RecordedMutations.First().StateEvent.ShouldBe(mutation.StateEvent);
		eventRaised.ShouldBeTrue();
		actualStateEventRaised.ShouldBe(mutation.StateEvent);
	}

	[Theory, AutoNSubstituteData]
	internal async Task FluxState_MutateStateWithoutRegisteredMutation_ShouldReturnMutationNotRegisteredError(
		FluxState<TestState> sut,
		TestCommand command)
	{
		//Act
		var result = await sut.ApplyMutationCommand(command);

		//Assert
		result.IsSuccess.ShouldBeFalse();
		result.GetError().ShouldBeOfType<MutationNotRegisteredError>();
		sut.RecordedMutations.ShouldBeEmpty();
	}

	[Theory, AutoNSubstituteData]
	internal async Task FluxState_MutateStateException_ShouldReturnMutationError(
		[Frozen] IServiceProvider provider,
		FluxState<TestState> sut,
		TestCommand command)
	{
		//Arrange
		provider.GetService(typeof(IFluxStateMutation<TestState, TestCommand>)).Throws<Exception>();

		//Act
		var result = await sut.ApplyMutationCommand(command);

		//Assert
		result.IsSuccess.ShouldBeFalse();
		result.GetError().ShouldBeOfType<MutationError>();
		sut.RecordedMutations.ShouldBeEmpty();
	}
}
