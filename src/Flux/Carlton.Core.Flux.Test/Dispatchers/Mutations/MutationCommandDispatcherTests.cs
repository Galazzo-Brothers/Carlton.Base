using Carlton.Core.Components.Flux.Tests.Common.Extensions;
using Carlton.Core.Flux.Contracts;
using Carlton.Core.Flux.Dispatchers.Mutations;
using Carlton.Core.Flux.Test.Common.Extensions;
using Carlton.Core.Foundation.Test;
namespace Carlton.Core.Flux.Tests.Dispatchers.Mutations;

public class MutationCommandDispatcherTests
{
    [Theory, AutoNSubstituteData]
    public async Task Dispatch_AssertHandlerCalled(
        [Frozen] IServiceProvider serviceProvider,
        [Frozen] IMutationCommandHandler<TestState> handler,
        MutationCommandDispatcher<TestState> sut,
        object sender,
        TestCommand1 command)
    {
        //Arrange
        serviceProvider.SetupServiceProvider<IMutationCommandHandler<TestState>>(handler);
        handler.SetupHandler<TestCommand1>();

        //Act
        await sut.Dispatch(sender, command, CancellationToken.None);

        //Assert
        handler.VerifyHandler(command);
    }
}
