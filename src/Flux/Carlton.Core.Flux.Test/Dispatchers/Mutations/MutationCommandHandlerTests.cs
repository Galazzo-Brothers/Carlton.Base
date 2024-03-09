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
        //Act
        await sut.Handle(new MutationCommandContext<TestCommand1>(command), CancellationToken.None);

        //Assert
        await state.Received().ApplyMutationCommand(command);
    }
}
