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

    [Theory, AutoData]
    public async Task Dispatch_AssertHandlerCalled(MutationCommand command)
    {
        //Arrange
        var serviceProvider = _fixture.Freeze<Mock<IServiceProvider>>();
        var handler = _fixture.Freeze<Mock<IMutationCommandHandler<TestState, MutationCommand>>>();
        var sender = _fixture.Create<object>();
        var sut = _fixture.Create<MutationCommandDispatcher<TestState>>();

        serviceProvider.SetupServiceProvider<IMutationCommandHandler<TestState, MutationCommand>>(handler.Object);
        handler.SetupHandler();
      

        //Act
        await sut.Dispatch(sender, command, CancellationToken.None);

        //Assert
        handler.VerifyHandler();
    }  
}
