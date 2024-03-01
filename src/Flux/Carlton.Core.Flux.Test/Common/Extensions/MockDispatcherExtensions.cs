//using Carlton.Core.Flux.Contracts;
//using Carlton.Core.Flux.Models;
//namespace Carlton.Core.Flux.Tests.Common.Extensions;

//public static class MockDispatcherExtensions
//{
//    public static void SetupDispatcher<TViewModel>(this Mock<IViewModelQueryDispatcher<TestState>> dispatcher, TViewModel response)
//    {
//        dispatcher.Setup(_ => _.Dispatch<TViewModel>(It.IsAny<object>(), It.IsAny<ViewModelQuery>(), It.IsAny<CancellationToken>()))
//                               .Returns(Task.FromResult(response));
//    }

//    public static void SetupDispatcherException<TViewModel, TException>(this Mock<IViewModelQueryDispatcher<TestState>> dispatcher, TException ex)
//      where TException : Exception
//    {
//        dispatcher.Setup(_ => _.Dispatch<TViewModel>(It.IsAny<object>(), It.IsAny<ViewModelQuery>(), It.IsAny<CancellationToken>()))
//                  .ThrowsAsync(ex);
//    }

//    public static void VerifyDispatch<TViewModel>(this Mock<IViewModelQueryDispatcher<TestState>> dispatcher, ViewModelQuery query)
//    {
//        dispatcher.Verify(mock => mock.Dispatch<TViewModel>(It.IsAny<object>(), query, It.IsAny<CancellationToken>()), Times.Once);
//    }

//    public static void VerifyDispatcher<TViewModel>(this Mock<IViewModelQueryDispatcher<TestState>> dispatcher, int times)
//    {
//        dispatcher.Verify(_ => _.Dispatch<TViewModel>(It.IsAny<object>(), It.IsAny<ViewModelQuery>(), It.IsAny<CancellationToken>()), Times.Exactly(times));
//    }

//    public static void VerifyDispatchCalled<TCommand>(this Mock<IMutationCommandDispatcher<TestState>> dispatcher, TCommand command)
//        where TCommand : MutationCommand
//    {
//        dispatcher.Verify(mock => mock.Dispatch(It.IsAny<object>(), command, It.IsAny<CancellationToken>()), Times.Once);
//    }

//    public static void VerifyDispatcher<TCommand>(this Mock<IMutationCommandDispatcher<TestState>> dispatcher)
//        where TCommand : MutationCommand
//    {
//        dispatcher.Verify(_ => _.Dispatch(It.IsAny<object>(), It.IsAny<TCommand>(), It.IsAny<CancellationToken>()), Times.Once());
//    }

//    public static void VerifyDispatcherNotCalled<TCommand>(this Mock<IMutationCommandDispatcher<TestState>> dispatcher)
//      where TCommand : MutationCommand
//    {
//        dispatcher.Verify(_ => _.Dispatch(It.IsAny<object>(), It.IsAny<TCommand>(), It.IsAny<CancellationToken>()), Times.Never);
//    }

//    public static void SetupDispatcherException<TException>(this Mock<IMutationCommandDispatcher<TestState>> dispatcher, TException ex)
//      where TException : Exception
//    {
//        dispatcher.Setup(_ => _.Dispatch(It.IsAny<object>(), It.IsAny<MutationCommand>(), It.IsAny<CancellationToken>()))
//                  .ThrowsAsync(ex);
//    }
//}


