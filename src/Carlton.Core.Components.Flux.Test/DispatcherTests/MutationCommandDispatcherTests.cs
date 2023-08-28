using Carlton.Core.Components.Flux.Contracts;
using Carlton.Core.Components.Flux.Dispatchers;
using Carlton.Core.Components.Flux.Models;
using Carlton.Core.Components.Flux.Test.Common;
using Moq;

namespace Carlton.Core.Components.Flux.Test.DispatcherTests;

public class MutationCommandDispatcherTests
{

    private readonly IMutationCommandDispatcher<TestState> _dispatcher;
    private readonly Mock<IServiceProvider> _mockServiceProvider = new();

    public MutationCommandDispatcherTests()
    {
        _dispatcher = new MutationCommandDispatcher<TestState>(_mockServiceProvider.Object);
    }

    [Theory]
    [MemberData(nameof(TestDataGenerator.GetCommandData), MemberType = typeof(TestDataGenerator))]
    public async Task Dispatch_AssertHandlerCalled<TCommand>(TCommand command)
        where TCommand : MutationCommand
    {
        //Arrange
        var sender = new object();
        var handler = new Mock<IMutationCommandHandler<TestState, TCommand>>();
        handler.SetupHandler();
        _mockServiceProvider.SetupServiceProvider<IMutationCommandHandler<TestState, TCommand>>(handler.Object);

        //Act
        await _dispatcher.Dispatch(sender, command, CancellationToken.None);

        //Assert
        handler.VerifyHandler();
    }  
}
