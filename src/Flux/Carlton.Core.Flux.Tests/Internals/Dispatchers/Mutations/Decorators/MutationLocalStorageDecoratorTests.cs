using Carlton.Core.Flux.Internals.Dispatchers.Mutations;
using Carlton.Core.Foundation.Tests;
namespace Carlton.Core.Flux.Tests.Internals.Dispatchers.Mutations.Decorators;

public class MutationLocalStorageDecoratorTests
{
	[Theory, AutoNSubstituteData]
	internal async Task LocalStorageDecoratorDispatch_DispatchCalled_ReturnsViewModel(
		[Frozen] IBrowserStorageService<TestState> browserStorage,
		[Frozen] IFluxState<TestState> fluxState,
		[Frozen] IMutationCommandDispatcher<TestState> decorated,
		MutationLocalStorageDecorator<TestState> sut,
		object sender,
		MutationCommandContext<TestCommand> context,
		MutationCommandResult expectedResult)
	{
		//Arrange
		decorated.SetupCommandDispatcher(context.MutationCommand, expectedResult);

		//Act 
		var actualResult = await sut.Dispatch(sender, context, CancellationToken.None);

		//Assert
		decorated.VerifyCommandDispatcher(1, context.MutationCommand);
		await browserStorage.Received().SaveState(fluxState.CurrentState);
		actualResult.IsSuccess.ShouldBeTrue();
		actualResult.ShouldBe(expectedResult);
		context.StateCommittedToLocalStorage.ShouldBeTrue();
	}

	[Theory, AutoNSubstituteData]
	internal async Task LocalStorageDecoratorDispatch_DispatchCalled_WithException_ReturnsError(
		[Frozen] IBrowserStorageService<TestState> browserStorage,
		[Frozen] IMutationCommandDispatcher<TestState> decorated,
		MutationLocalStorageDecorator<TestState> sut,
		object sender,
		MutationCommandContext<TestCommand> context,
		Exception exception)
	{
		//Arrange
		decorated.SetupCommandDispatcherException<TestCommand>(exception);

		//Act 
		var actualResult = await sut.Dispatch(sender, context, CancellationToken.None);

		//Assert
		decorated.VerifyCommandDispatcher(1, context.MutationCommand);
		await browserStorage.Received(0).SaveState(Arg.Any<TestState>());
		actualResult.IsSuccess.ShouldBeFalse();
		actualResult.GetError().ShouldBeOfType<LocalStorageError>();
		context.StateCommittedToLocalStorage.ShouldBeFalse();
	}
}
