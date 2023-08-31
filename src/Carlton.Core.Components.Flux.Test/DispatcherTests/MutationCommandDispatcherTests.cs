using AutoFixture.AutoMoq;
using Carlton.Core.Components.Flux.Contracts;
using Carlton.Core.Components.Flux.Dispatchers;
using Carlton.Core.Components.Flux.Models;
using Carlton.Core.Components.Flux.Test.Common;
using Carlton.Core.Components.Flux.Test.Common.Extensions;

namespace Carlton.Core.Components.Flux.Test.DispatcherTests;

public class MutationCommandDispatcherTests
{
    private readonly IFixture _fixture;

    public MutationCommandDispatcherTests()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
    }

    [Theory]
    [MemberData(nameof(TestDataGenerator.GetCommandData), MemberType = typeof(TestDataGenerator))]
    public async Task Dispatch_AssertHandlerCalled<TCommand>(TCommand command)
        where TCommand : MutationCommand
    {
        //Arrange
        var serviceProvider = _fixture.Freeze<Mock<IServiceProvider>>();
        var handler = _fixture.Freeze<Mock<IMutationCommandHandler<TestState, TCommand>>>();
        var sender = _fixture.Create<object>();
        var sut = _fixture.Create<MutationCommandDispatcher<TestState>>();

        serviceProvider.SetupServiceProvider<IMutationCommandHandler<TestState, TCommand>>(handler.Object);
        handler.SetupHandler();
      

        //Act
        await sut.Dispatch(sender, command, CancellationToken.None);

        //Assert
        handler.VerifyHandler();
    }  
}
