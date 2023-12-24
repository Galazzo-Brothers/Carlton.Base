using AutoFixture.AutoMoq;
using Carlton.Core.Components.Flux.Contracts;
using Carlton.Core.Components.Flux.Handlers.Mutations;
using Carlton.Core.Components.Flux.Models;
using Carlton.Core.Components.Flux.Test.Common;
using Carlton.Core.Components.Flux.Test.Common.Extensions;
using Microsoft.Extensions.Logging;

namespace Carlton.Core.Components.Flux.Test.DecoratorTests.Mutations;

public class MutationExceptionDecoratorTests
{
    private readonly IFixture _fixture;
    private readonly Mock<IMutationCommandDispatcher<TestState>> _decorated;
 
    public MutationExceptionDecoratorTests()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
        _decorated = _fixture.Freeze<Mock<IMutationCommandDispatcher<TestState>>>();
        _fixture.Freeze<Mock<ILogger<MutationExceptionDecorator<TestState>>>>();
    }

    [Theory, AutoData]
    public async Task Dispatch_DispatchCalled(MutationCommand command)
    {
        //Arrange
        var sender = new object();
        var sut = _fixture.Create<MutationExceptionDecorator<TestState>>();

        //Act 
        await sut.Dispatch(sender, command, CancellationToken.None);

        //Assert
        _decorated.VerifyDispatchCalled(command);
    }

    [Fact]
    public async Task Dispatch_ThrowsMutationCommandFluxExceptionException()
    {
        //Arrange
        var sender = new object();
        var command = _fixture.Create<TestCommand2>();
        var sut = _fixture.Create<MutationExceptionDecorator<TestState>>();
        _decorated.SetupDispatcherException(new Exception());

        //Act
        var ex = await Assert.ThrowsAsync<MutationCommandFluxException<TestState, TestCommand2>>(async () => await sut.Dispatch(sender, command, CancellationToken.None));

        //Assert
        Assert.Equal(LogEvents.Mutation_Unhandled_Error, ex.EventID);
        Assert.Equal(LogEvents.Mutation_Unhandled_ErrorMsg, ex.Message);
    }
}
