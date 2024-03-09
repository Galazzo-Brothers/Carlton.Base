using Carlton.Core.Flux.Contracts;
using Carlton.Core.Flux.Dispatchers.Mutations;
using Carlton.Core.Foundation.Test;
namespace Carlton.Core.Flux.Tests.Dispatchers.Mutations;

public class MutationCommandHandlerTests
{
    [Theory, AutoNSubstituteData]
    public async Task Handle_ShouldCallStateMutation(
        [Frozen] IMutableFluxState<TestState> state,
        MutationCommandHandler<TestState> sut,
        TestCommand1 command)
    {
        //Arrange
        state.ApplyMutationCommand(command).Returns(command);
        var context = new MutationCommandContext<TestCommand1>(command);

        //Act
        await sut.Handle(context, CancellationToken.None);

        //Assert
        await state.Received().ApplyMutationCommand(command);
    }

    [Theory, AutoNSubstituteData]
    public async Task Handle_Error_ShouldCallStateMutation(
       [Frozen] IMutableFluxState<TestState> state,
       MutationCommandHandler<TestState> sut,
       TestCommand1 command,
       MutationError error)
    {
        //Arrange
        state.ApplyMutationCommand(command).Returns(error);
        var context = new MutationCommandContext<TestCommand1>(command);

        //Act
        var actualError = await sut.Handle(context, CancellationToken.None);

        //Assert
        await state.Received().ApplyMutationCommand(command);
        actualError.ShouldBe(error);
    }
}
