using Carlton.Core.Components.Flux.Contracts;
using Carlton.Core.Components.Flux.Decorators.Commands;
using Carlton.Core.Components.Flux.Models;
using Carlton.Core.Components.Flux.Test.Common;
using Microsoft.Extensions.Logging;
using Moq;

namespace Carlton.Core.Components.Flux.Test.DecoratorTests.Mutations;

public class MutationExceptionDecoratorTests
{
    private readonly Mock<IMutationCommandDispatcher<TestState>> _decorated = new();
    private readonly Mock<ILogger<MutationExceptionDecorator<TestState>>> _logger = new();
    private readonly MutationExceptionDecorator<TestState> _dispatcher;

    public MutationExceptionDecoratorTests()
    {
        _dispatcher = new MutationExceptionDecorator<TestState>(_decorated.Object, _logger.Object);
    }

    [Theory]
    [MemberData(nameof(TestDataGenerator.GetCommandData), MemberType = typeof(TestDataGenerator))]
    public async Task Dispatch_DispatchCalled<TCommand>(TCommand command)
        where TCommand : MutationCommand
    {
        //Arrange
        var sender = new object();

        //Act 
        await _dispatcher.Dispatch(sender, command, CancellationToken.None);

        //Assert
        _decorated.VerifyDispatchCalled(command);
    }

    [Theory]
    [MemberData(nameof(TestDataGenerator.GetMutationExceptionData), MemberType = typeof(TestDataGenerator))]
    public async Task Dispatch_ThrowsException<TException>(TException ex, string expectedMessage)
        where TException : Exception
    {
        //Arrange
        var sender = new object();
        _decorated.Setup(_ => _.Dispatch(It.IsAny<object>(), It.IsAny<MutationCommand>(), CancellationToken.None)).ThrowsAsync(ex);
        var command = new TestCommand2(2, "Testing Again", 17);

        //Act
        var thrownEx = await Assert.ThrowsAsync<MutationCommandFluxException<TestState, TestCommand2>>(async () => await _dispatcher.Dispatch(sender, command, CancellationToken.None));

        //Assert
        Assert.Equivalent(expectedMessage, thrownEx.Message);
    }
}
