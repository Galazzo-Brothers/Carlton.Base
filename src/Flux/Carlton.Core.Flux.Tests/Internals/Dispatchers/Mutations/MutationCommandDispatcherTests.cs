using Carlton.Core.Flux.Internals.Contracts;
using Carlton.Core.Flux.Internals.Dispatchers.Mutations;
using Carlton.Core.Flux.Test.Common.Extensions;
using Carlton.Core.Foundation.Tests;
namespace Carlton.Core.Flux.Tests.Internals.Dispatchers.Mutations;

public class MutationCommandDispatcherTests
{
	[Theory, AutoNSubstituteData]
	internal async Task Dispatch_ShouldCallHandler(
		[Frozen] IServiceProvider serviceProvider,
		[Frozen] IMutationCommandHandler<TestState> handler,
		MutationCommandDispatcher<TestState> sut,
		object sender,
		MutationCommandContext<TestCommand> context,
		string stateEvent)
	{
		//Arrange
		serviceProvider.SetupServiceProvider<IMutationCommandHandler<TestState>>(handler);
		handler.SetupHandler<TestCommand>(stateEvent);

		//Act
		await sut.Dispatch(sender, context, CancellationToken.None);

		//Assert
		handler.VerifyHandler(context.MutationCommand);
	}
}
