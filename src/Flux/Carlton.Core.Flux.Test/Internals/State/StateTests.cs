using Carlton.Core.Flux.Internals.State;
using NSubstitute.ExceptionExtensions;
using Carlton.Core.Foundation.Tests;
using System.Reflection;
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
		sut.RecordedMutations.Last().Command.ShouldBe(command);
		sut.RecordedMutations.Last().StateEvent.ShouldBe(mutation.StateEvent);
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
		sut.RecordedMutations.Count().ShouldBe(1);
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
		sut.RecordedMutations.Count().ShouldBe(1);
	}

	[Theory, AutoNSubstituteData]
	internal async Task FluxState_MutateStateException_ShouldRollbackState(
		[Frozen] IServiceProvider provider,
		IFluxStateMutation<TestState, TestCommand> mutation,
		FluxState<TestState> sut,
		TestCommand command)
	{
		//Arrange
		var currentStateProp = typeof(FluxState<TestState>).GetProperty("CurrentState", BindingFlags.Public | BindingFlags.Instance);
		var rollbackStateProp = typeof(FluxState<TestState>).GetProperty("RollbackState", BindingFlags.NonPublic | BindingFlags.Instance);
		mutation.Mutate(Arg.Any<TestState>(), Arg.Any<TestCommand>()).Throws<Exception>();
		provider.SetupServiceProvider<IFluxStateMutation<TestState, TestCommand>>(mutation);
		currentStateProp.SetValue(sut, null);

		//Act
		await sut.ApplyMutationCommand(command);

		//Assert
		sut.CurrentState.ShouldBe(rollbackStateProp.GetValue(sut));
	}

	[Theory, AutoNSubstituteData]
	internal async Task FluxState_MutateStateException_ShouldRollbackStateAndDequeueMutation(
		[Frozen] IServiceProvider provider,
		TestMutation mutation,
		FluxState<TestState> sut,
		TestCommand command)
	{
		//Arrange
		var currentStateProp = typeof(FluxState<TestState>).GetProperty("CurrentState", BindingFlags.Public | BindingFlags.Instance);
		var rollbackStateProp = typeof(FluxState<TestState>).GetProperty("RollbackState", BindingFlags.NonPublic | BindingFlags.Instance);
		var expectedCount = sut.RecordedMutations.Count();
		provider.SetupServiceProvider<IFluxStateMutation<TestState, TestCommand>>(mutation);
		sut.StateChanged += (args) =>
		{
			throw new Exception();
		};

		//Act
		await sut.ApplyMutationCommand(command);

		//Assert
		sut.RecordedMutations.Count().ShouldBe(expectedCount);
		sut.CurrentState.ShouldBe(rollbackStateProp.GetValue(sut));
	}
}
