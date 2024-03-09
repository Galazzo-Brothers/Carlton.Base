using Carlton.Core.Components.Flux.Tests.Common.Extensions;
using Carlton.Core.Flux.Contracts;
using Carlton.Core.Flux.Dispatchers.Mutations;
using Carlton.Core.Flux.Test.Common.Extensions;
using Carlton.Core.Foundation.Test;
namespace Carlton.Core.Flux.Tests.Dispatchers.Mutations;

public class MutationCommandDispatcherTests
{
	[Theory, AutoNSubstituteData]
	public async Task Dispatch_ShouldCallHandler(
		[Frozen] IServiceProvider serviceProvider,
		[Frozen] IMutationCommandHandler<TestState> handler,
		MutationCommandDispatcher<TestState> sut,
		object sender,
		MutationCommandContext<TestCommand1> context)
	{
		//Arrange
		serviceProvider.SetupServiceProvider<IMutationCommandHandler<TestState>>(handler);
		handler.SetupHandler<TestCommand1>();

		//Act
		await sut.Dispatch(sender, context, CancellationToken.None);

		//Assert
		handler.VerifyHandler(context.MutationCommand);
	}
}
