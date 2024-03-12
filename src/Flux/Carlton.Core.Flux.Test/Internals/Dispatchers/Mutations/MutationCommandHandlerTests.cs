using Carlton.Core.Flux.Internals.Contracts;
using Carlton.Core.Flux.Internals.Dispatchers.Mutations;
namespace Carlton.Core.Flux.Tests.Internals.Dispatchers.Mutations;

public class MutationCommandHandlerTests
{
	[Theory, AutoNSubstituteData]
	internal async Task Handle_ShouldCallStateMutation(
		[Frozen] IMutableFluxState<TestState> state,
		MutationCommandHandler<TestState> sut,
		TestCommand command)
	{
		//Arrange
		state.ApplyMutationCommand(command).Returns(command);
		var context = new MutationCommandContext<TestCommand>(command);

		//Act
		await sut.Handle(context, CancellationToken.None);

		//Assert
		await state.Received().ApplyMutationCommand(command);
	}

	[Theory, AutoNSubstituteData]
	internal async Task Handle_Error_ShouldCallStateMutation(
	   [Frozen] IMutableFluxState<TestState> state,
	   MutationCommandHandler<TestState> sut,
	   TestCommand command,
	   MutationError error)
	{
		//Arrange
		state.ApplyMutationCommand(command).Returns(error);
		var context = new MutationCommandContext<TestCommand>(command);

		//Act
		var actualError = await sut.Handle(context, CancellationToken.None);

		//Assert
		await state.Received().ApplyMutationCommand(command);
		actualError.ShouldBe(error);
	}
}
