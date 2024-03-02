using Carlton.Core.Flux.Contracts;
using Carlton.Core.Flux.Exceptions;
using Carlton.Core.Flux.Handlers.Mutations;
using Carlton.Core.Flux.Logging;
using Carlton.Core.Flux.Models;
using Carlton.Core.Flux.Tests.Common;
using Carlton.Core.Foundation.Test;
using Microsoft.Extensions.Logging;
using NSubstitute.ExceptionExtensions;
namespace Carlton.Core.Flux.Tests.DecoratorTests.Mutations;

public class MutationExceptionDecoratorTests
{
    [Theory, AutoNSubstituteData]
    public async Task Dispatch_DispatchCalled(
        [Frozen] IMutationCommandDispatcher<TestState> decorated,
        [Frozen] ILogger<MutationExceptionDecorator<TestState>> logger,
        object sender,
        object command,
        MutationExceptionDecorator<TestState> sut)
    {
        //Act 
        await sut.Dispatch(sender, command, CancellationToken.None);

        //Assert
        await decorated.Received(1).Dispatch(
           Arg.Any<object>(),
           Arg.Any<MutationCommandContext<object>>(),
           Arg.Any<CancellationToken>());
    }

    [Theory, AutoNSubstituteData]
    public async Task Dispatch_ThrowsMutationCommandFluxExceptionException(
        [Frozen] IMutationCommandDispatcher<TestState> decorated,
        [Frozen] ILogger<MutationExceptionDecorator<TestState>> logger,
        object sender,
        TestCommand1 command,
        MutationExceptionDecorator<TestState> sut)
    {
        //Arrange
        decorated.Dispatch(sender, command, CancellationToken.None).ThrowsForAnyArgs(new Exception());

        //Act
        var func = async () => await sut.Dispatch(sender, command, CancellationToken.None);
        var ex = await func.ShouldThrowAsync<MutationCommandFluxException<TestState, TestCommand1>>();

        //Assert
        ex.EventId.ShouldBe(FluxLogs.Mutation_Unhandled_Error);
        ex.Message.ShouldBe(FluxLogs.Mutation_Unhandled_ErrorMsg);
    }
}
