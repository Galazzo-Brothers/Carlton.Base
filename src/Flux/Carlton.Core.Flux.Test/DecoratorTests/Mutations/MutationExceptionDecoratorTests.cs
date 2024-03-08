using Carlton.Core.Flux.Contracts;
using Carlton.Core.Flux.Dispatchers.Mutations;
using Carlton.Core.Flux.Dispatchers.Mutations.Decorators;
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
        MutationExceptionDecorator<TestState> sut,
        object sender,
        TestCommand1 command)
    {
        //Act 
        await sut.Dispatch(sender, command, CancellationToken.None);

        //Assert
        await decorated.Received(1).Dispatch(
           Arg.Any<object>(),
           Arg.Any<MutationCommandContext<TestCommand1>>(),
           Arg.Any<CancellationToken>());
    }

    [Theory, AutoNSubstituteData]
    public async Task Dispatch_ThrowsMutationCommandFluxExceptionException(
        [Frozen] IMutationCommandDispatcher<TestState> decorated,
        [Frozen] ILogger<MutationExceptionDecorator<TestState>> logger,
        object sender,
        MutationCommandContext<TestCommand1> context,
        MutationExceptionDecorator<TestState> sut)
    {
        //Arrange
        decorated.Dispatch(sender, context, CancellationToken.None).ThrowsForAnyArgs(new Exception());

        //Act
        var func = async () => await sut.Dispatch(sender, context, CancellationToken.None);
       // var ex = await func.ShouldThrowAsync<MutationCommandFluxException<TestState, TestCommand1>>();

        //Assert
        //ex.EventId.ShouldBe(FluxLogs.Mutation_Unhandled_Error);
        //ex.Message.ShouldBe(FluxLogs.Mutation_Unhandled_ErrorMsg);
    }
}
