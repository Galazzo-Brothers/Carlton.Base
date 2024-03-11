using Carlton.Core.Components.Flux.Tests.Common.Extensions;
using Carlton.Core.Flux.Dispatchers;
using Carlton.Core.Flux.Internals.Contracts;
using Carlton.Core.Flux.Internals.Dispatchers.Mutations;
using Carlton.Core.Flux.Test.Common.Extensions;
using Carlton.Core.Foundation.Test;
namespace Carlton.Core.Flux.Tests.Internals.Dispatchers.Mutations;

public class MutationCommandDispatcherTests
{
	[Theory, AutoNSubstituteData]
	internal async Task Dispatch_ShouldCallHandler(
		[Frozen] IServiceProvider serviceProvider,
		[Frozen] IMutationCommandHandler<TestState> handler,
		MutationCommandDispatcher<TestState> sut,
		object sender,
		MutationCommandContext<TestCommand> context)
	{
		//Arrange
		serviceProvider.SetupServiceProvider<IMutationCommandHandler<TestState>>(handler);
		handler.SetupHandler<TestCommand>();

		//Act
		await sut.Dispatch(sender, context, CancellationToken.None);

		//Assert
		handler.VerifyHandler(context.MutationCommand);
	}
}
