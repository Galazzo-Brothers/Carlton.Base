using Carlton.Core.Flux.Contracts;
using Carlton.Core.Flux.Dispatchers.Mutations;
using Carlton.Core.Flux.Handlers;
using Carlton.Core.Flux.Tests.Common;
using Carlton.Core.Foundation.Test;
using Microsoft.Extensions.Logging;
namespace Carlton.Core.Flux.Tests.HandlerTests;

public class MutationCommandHandlerTests
{
    [Theory, AutoNSubstituteData]
    public async Task Handle_ShouldCallStateMutation(
        [Frozen] IMutableFluxState<TestState> state,
        [Frozen] ILogger<MutationCommandHandler<TestState>> logger,
        MutationCommandHandler<TestState> sut,
        TestCommand1 command)
    {
        //Act
        await sut.Handle(new MutationCommandContext<TestCommand1>(command), CancellationToken.None);

        //Assert
        await state.Received().ApplyMutationCommand(command);
    }
}
