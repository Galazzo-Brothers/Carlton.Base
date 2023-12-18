using Carlton.Core.Components.Flux.Contracts;
using Carlton.Core.Components.Flux.Models;

namespace Carlton.Core.Components.Flux.Test.Common.Extensions;

public static class MockHandlerExtensions
{
    public static void SetupHandler<T>(this Mock<IViewModelQueryHandler<TestState>> handler, T response)
    {
        handler.Setup(_ => _.Handle<T>(It.IsAny<ViewModelQuery>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(response));
    }

    public static void SetupHandler<T>(this Mock<IMutationCommandHandler<TestState>> handler)
        where T : MutationCommand
    {
        handler.Setup(_ => _.Handle(It.IsAny<T>(), CancellationToken.None)).Returns(Task.FromResult(Unit.Value));
    }

    public static void VerifyHandler<TViewModel>(this Mock<IViewModelQueryHandler<TestState>> handler)
    {
        handler.Verify(_ => _.Handle<TViewModel>(It.IsAny<ViewModelQuery>(), CancellationToken.None), Times.Once);
    }

    public static void VerifyHandler<TCommand>(this Mock<IMutationCommandHandler<TestState>> handler)
      where TCommand : MutationCommand
    {
        handler.Verify(_ => _.Handle(It.IsAny<TCommand>(), CancellationToken.None), Times.Once);
    }
}
