using Carlton.Core.Flux.Internals.Dispatchers.Mutations;
using Carlton.Core.Flux.Internals.Dispatchers.ViewModels;
using Carlton.Core.Utilities.Results;
using NSubstitute.ExceptionExtensions;
namespace Carlton.Core.Flux.Tests.Common.Extensions;

internal static class DispatcherExtensions
{
	public static void SetupQueryDispatcher<TViewModel>(this IViewModelQueryDispatcher<TestState> dispatcher, TViewModel vm)
	{
		dispatcher.Dispatch(
		 Arg.Any<object>(),
		 Arg.Any<ViewModelQueryContext<TViewModel>>(),
		 Arg.Any<CancellationToken>())
		.Returns(Task.FromResult((Result<TViewModel, FluxError>)vm));
	}

	public static void SetupQueryDispatcherError<TViewModel>(this IViewModelQueryDispatcher<TestState> dispatcher, FluxError error)
	{
		dispatcher.Dispatch(
		 Arg.Any<object>(),
		 Arg.Any<ViewModelQueryContext<TViewModel>>(),
		 Arg.Any<CancellationToken>())
		.Returns(Task.FromResult((Result<TViewModel, FluxError>)error));
	}

	public static void SetupQueryDispatcherException<TViewModel>(this IViewModelQueryDispatcher<TestState> dispatcher, Exception ex)
	{
		dispatcher.Dispatch(
		 Arg.Any<object>(),
		 Arg.Any<ViewModelQueryContext<TViewModel>>(),
		 Arg.Any<CancellationToken>())
		.Throws(ex);
	}

	public static void SetupCommandDispatcher<TCommand>(this IMutationCommandDispatcher<TestState> dispatcher, TCommand command, MutationCommandResult result)
	{
		dispatcher.Dispatch(
		 Arg.Any<object>(),
		 Arg.Is<MutationCommandContext<TCommand>>(context => context.MutationCommand.Equals(command)),
		 Arg.Any<CancellationToken>())
		.Returns(Task.FromResult((Result<MutationCommandResult, FluxError>)result));
	}

	public static void SetupCommandDispatcherError<TCommand>(this IMutationCommandDispatcher<TestState> dispatcher, TCommand command, FluxError error)
	{
		dispatcher.Dispatch(
		 Arg.Any<object>(),
		 Arg.Is<MutationCommandContext<TCommand>>(c => c.MutationCommand.Equals(command)),
		 Arg.Any<CancellationToken>())
		.Returns(error);
	}

	public static void SetupCommandDispatcherException<TCommand>(this IMutationCommandDispatcher<TestState> dispatcher, Exception ex)
	{
		dispatcher.Dispatch(
		 Arg.Any<object>(),
		 Arg.Any<MutationCommandContext<TCommand>>(),
		 Arg.Any<CancellationToken>())
		.Throws(ex);
	}

	public static void VerifyQueryDispatcher<TViewModel>(this IViewModelQueryDispatcher<TestState> dispatcher, int receivedNumCalls)
	{
		dispatcher.Received(receivedNumCalls).Dispatch(
			Arg.Any<object>(),
			Arg.Any<ViewModelQueryContext<TViewModel>>(),
			Arg.Any<CancellationToken>());
	}

	public static void VerifyCommandDispatcher<TCommand>(this IMutationCommandDispatcher<TestState> dispatcher, int receivedNumCalls, TCommand command)
	{
		dispatcher.Received(receivedNumCalls).Dispatch(
			Arg.Any<object>(),
			Arg.Is<MutationCommandContext<TCommand>>(_ => _.MutationCommand.Equals(command)),
			Arg.Any<CancellationToken>());
	}
}


