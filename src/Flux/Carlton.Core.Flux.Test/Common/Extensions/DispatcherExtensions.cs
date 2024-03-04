using Carlton.Core.Flux.Contracts;
using Carlton.Core.Flux.Models;
using Carlton.Core.Utilities.Results;
namespace Carlton.Core.Flux.Tests.Common.Extensions;

public static class DispatcherExtensions
{
    public static void SetupQueryDispatcher(this IViewModelQueryDispatcher<TestState> dispatcher, TestViewModel vm)
    {
        dispatcher.Dispatch(
         Arg.Any<object>(),
         Arg.Any<ViewModelQueryContext<TestViewModel>>(),
         Arg.Any<CancellationToken>())
        .Returns(Task.FromResult((Result<TestViewModel, ViewModelFluxError>) vm));
    }

    public static void VerifyQueryDispatcher(this IViewModelQueryDispatcher<TestState> dispatcher, int receivedNumCalls)
    {
        dispatcher.Received(receivedNumCalls).Dispatch(
            Arg.Any<object>(),
            Arg.Any<ViewModelQueryContext<TestViewModel>>(),
            Arg.Any<CancellationToken>());
    }

    public static void VerifyCommandDispatcher(this IMutationCommandDispatcher<TestState> dispatcher, int receivedNumCalls, object command)
    {
        dispatcher.Received(receivedNumCalls).Dispatch(
            Arg.Any<object>(),
            Arg.Is<MutationCommandContext<object>>(_ => _.MutationCommand == command),
            Arg.Any<CancellationToken>());
    }
}


