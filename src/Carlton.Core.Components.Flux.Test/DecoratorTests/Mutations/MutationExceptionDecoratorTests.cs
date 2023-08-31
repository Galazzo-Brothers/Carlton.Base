using AutoFixture.AutoMoq;
using Carlton.Core.Components.Flux.Contracts;
using Carlton.Core.Components.Flux.Decorators.Commands;
using Carlton.Core.Components.Flux.Models;
using Carlton.Core.Components.Flux.Test.Common;
using Carlton.Core.Components.Flux.Test.Common.Extensions;
using Microsoft.Extensions.Logging;

namespace Carlton.Core.Components.Flux.Test.DecoratorTests.Mutations;

public class MutationExceptionDecoratorTests
{
    private readonly IFixture _fixture;
    private readonly Mock<IMutationCommandDispatcher<TestState>> _decorated;
    private readonly Mock<ILogger<MutationExceptionDecorator<TestState>>> _logger;

    public MutationExceptionDecoratorTests()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
        _decorated = _fixture.Freeze<Mock<IMutationCommandDispatcher<TestState>>>();
        _logger = _fixture.Freeze<Mock<ILogger<MutationExceptionDecorator<TestState>>>>();
    }

    [Theory]
    [MemberData(nameof(TestDataGenerator.GetCommandData), MemberType = typeof(TestDataGenerator))]
    public async Task Dispatch_DispatchCalled<TCommand>(TCommand command)
        where TCommand : MutationCommand
    {
        //Arrange
        var sender = new object();
        var sut = _fixture.Create<MutationExceptionDecorator<TestState>>();

        //Act 
        await sut.Dispatch(sender, command, CancellationToken.None);

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
        var command = _fixture.Create<TestCommand2>();
        var sut = _fixture.Create<MutationExceptionDecorator<TestState>>();
        _decorated.SetupDispatcherException(ex);

        //Act
        var thrownEx = await Assert.ThrowsAsync<MutationCommandFluxException<TestState, TestCommand2>>(async () => await sut.Dispatch(sender, command, CancellationToken.None));

        //Assert
        Assert.Equivalent(expectedMessage, thrownEx.Message);
    }
}
