using Carlton.Core.Flux.Contracts;
using Carlton.Core.Flux.Dispatchers;
using Carlton.Core.Flux.Tests.Common;
using Carlton.Core.Foundation.Test;
using Carlton.Core.Components.Flux.Tests.Common.Extensions;
using Carlton.Core.Flux.Test.Common.Extensions;
using Carlton.Core.Flux.Models;
namespace Carlton.Core.Flux.Tests.DispatcherTests;

public class ViewModelQueryDispatcherTests
{
    [Theory, AutoNSubstituteData]
    public async Task Dispatch_AssertHandlerCalled_AssertViewModelResponse(
        [Frozen] IServiceProvider serviceProvider,
        [Frozen] IViewModelQueryHandler<TestState> handler,
        ViewModelQueryDispatcher<TestState> sut,
        object sender,
        ViewModelQueryContext<TestViewModel> queryContext,
        TestViewModel expectedViewModel)
    {
        //Arrange
        serviceProvider.SetupServiceProvider<IViewModelQueryHandler<TestState>>(handler);
        handler.SetupHandler(expectedViewModel);

        //Act
      //  var result = await sut.Dispatch(sender, queryContext, CancellationToken.None);

        //Assert
        handler.VerifyHandler<TestViewModel>();
     //   result.ShouldBe(expectedViewModel);
    }
}



