using Carlton.Core.Components.Flux.Contracts;
using Carlton.Core.Components.Flux.Dispatchers;
using Carlton.Core.Components.Flux.Models;
using Carlton.Core.Components.Flux.Test.Common;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Carlton.Core.Components.Flux.Test.DispatcherTests;

public class MutationCommandDispatcherTests
{

    private readonly IMutationCommandDispatcher<object> _dispatcher;
    private readonly Mock<IMutationCommandHandler<object, TestCommand1>> _handler = new();
    private readonly Mock<IMutationCommandHandler<object, TestCommand2>> _handler2 = new();
    private readonly Mock<IServiceProvider> _mockServiceProvider = new();

    public MutationCommandDispatcherTests()
    {
        _handler.Setup(_ => _.Handle(It.IsAny<TestCommand1>(), CancellationToken.None)).Returns(Task.FromResult(Unit.Value));
        _handler2.Setup(_ => _.Handle(It.IsAny<TestCommand2>(), CancellationToken.None)).Returns(Task.FromResult(Unit.Value));
        _mockServiceProvider.Setup(_ => _.GetService(typeof(Mock<IMutationCommandHandler<object, TestCommand1>>))).Returns(_handler);
        _mockServiceProvider.Setup(_ => _.GetService(typeof(Mock<IMutationCommandHandler<object, TestCommand2>>))).Returns(_handler2);
        _mockServiceProvider.Setup(_ => _.GetService(typeof(IMutationCommandHandler<object, TestCommand1>))).Returns(_handler.Object);
        _mockServiceProvider.Setup(_ => _.GetService(typeof(IMutationCommandHandler<object, TestCommand2>))).Returns(_handler2.Object);
        _dispatcher = new MutationCommandDispatcher<object>(_mockServiceProvider.Object);
    }

    [Theory]
    [MemberData(nameof(TestDataGenerator.GetCommandData), MemberType = typeof(TestDataGenerator))]
    public async Task Dispatch_AssertHandlerCalled<TCommand>(TCommand command)
        where TCommand : MutationCommand
    {
        //Arrange
        var handler = _mockServiceProvider.Object.GetRequiredService<Mock<IMutationCommandHandler<object, TCommand>>>();

        //Act
        await _dispatcher.Dispatch(command, CancellationToken.None);

        //Assert
        Assert.Equal(1, handler.Invocations.Count);
    }  
}
