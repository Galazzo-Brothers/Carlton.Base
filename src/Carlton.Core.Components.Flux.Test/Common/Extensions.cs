using Carlton.Core.Components.Flux.Contracts;
using Carlton.Core.Components.Flux.Models;
using Moq;

namespace Carlton.Core.Components.Flux.Test.Common;

public static class Extensions
{
    public static void VerifyDispatchCalled<TViewModel>(this Mock<IViewModelQueryDispatcher<TestState>> dispatcher, ViewModelQuery query)
    {
        dispatcher.Verify(mock => mock.Dispatch<TViewModel>(query, It.IsAny<CancellationToken>()), Times.Once);
    }
}
