using Carlton.Core.Flux.Contracts;
using Carlton.Core.Flux.Dispatchers.ViewModels.Decorators;
using Carlton.Core.Flux.Dispatchers.ViewModels;
using Carlton.Core.Foundation.Test;
using System.Net;
using System.Text.Json;
namespace Carlton.Core.Flux.Tests.Dispatchers.ViewModels.Decorators;

public class ViewModelHttpDecoratorTests
{
    [Theory, AutoNSubstituteData]
    public async Task HttpDecoratorDispatch_NoHttpRefreshAttribute_DoesNotMakeHttpCall(
        [Frozen] IViewModelQueryDispatcher<TestState> decorated,
        [Frozen] MockHttpMessageHandler mockHttp,
        [Frozen] IMutableFluxState<TestState> state,
        ViewModelHttpDecorator<TestState> sut,
        object sender,
        ViewModelQueryContext<TestViewModel> context,
        TestViewModel expectedResult)
    {
        //Arrange
        var request = mockHttp.When("*");
        decorated.SetupQueryDispatcher(expectedResult);

        //Act 
        var actualResult = await sut.Dispatch(sender, context, CancellationToken.None);
        
        //Assert
        decorated.VerifyQueryDispatcher(1);
        mockHttp.GetMatchCount(request).ShouldBe(0);
        context.RequiresHttpRefresh.ShouldBeFalse();
        context.HttpRefreshOccurred.ShouldBeFalse();
        actualResult.ShouldBe(expectedResult);
    }

    [Theory, AutoNSubstituteData]
        public async Task HttpDecoratorDispatch_WithNeverHttpRefreshAttribute_DoesNotMakeHttpCall(
        [Frozen] IViewModelQueryDispatcher<TestState> decorated,
        [Frozen] MockHttpMessageHandler mockHttp,
        [Frozen] IMutableFluxState<TestState> state,
        ViewModelHttpDecorator<TestState> sut,
        HttpNeverRefreshCaller sender,
        ViewModelQueryContext<TestViewModel> context,
        TestViewModel expectedResult)
    {
        //Arrange
        var request = mockHttp.When("*");
        decorated.SetupQueryDispatcher(expectedResult);

        //Act 
        var actualResult = await sut.Dispatch(sender, context, CancellationToken.None);

        //Assert
        decorated.VerifyQueryDispatcher(1);
        mockHttp.GetMatchCount(request).ShouldBe(0);
        context.RequiresHttpRefresh.ShouldBeFalse();
        context.HttpRefreshOccurred.ShouldBeFalse();
        actualResult.ShouldBe(expectedResult);
    }

    [Theory, AutoNSubstituteData]
    public async Task HttpDecoratorDispatch_WithInvalidHttpRefreshAttribute_DoesNotMakeHttpCall(
      [Frozen] IViewModelQueryDispatcher<TestState> decorated,
      [Frozen] MockHttpMessageHandler mockHttp,
      [Frozen] IMutableFluxState<TestState> state,
      ViewModelHttpDecorator<TestState> sut,
      HttpRefreshWithInvalidHttpUrlCaller sender,
      ViewModelQueryContext<TestViewModel> context)
    {
        //Arrange
        var expectedError = new HttpUrlConstructionError("http://test.#%$@#carlton.com/clients/");
        var request = mockHttp.When("*");
        decorated.SetupQueryDispatcherError(expectedError);

        //Act 
        var actualResult = await sut.Dispatch(sender, context, CancellationToken.None);

        //Assert
        decorated.VerifyQueryDispatcher(0);
        mockHttp.GetMatchCount(request).ShouldBe(0);
        context.RequiresHttpRefresh.ShouldBeTrue();
        context.HttpRefreshOccurred.ShouldBeFalse();
        actualResult.ShouldBe(expectedError);
    }

    [Theory, AutoNSubstituteData]
    public async Task HttpDecoratorDispatch_WithHttpRefreshAttribute_DoesNotMakeHttpCall(
       [Frozen] IViewModelQueryDispatcher<TestState> decorated,
       [Frozen] MockHttpMessageHandler mockHttp,
       [Frozen] HttpClient httpClient,
       [Frozen] IMutableFluxState<TestState> state,
       ViewModelHttpDecorator<TestState> sut,
       HttpRefreshCaller sender,
       ViewModelQueryContext<TestViewModel> context,
       TestViewModel expectedResult)
    {
        //Arrange
        var request = mockHttp.When("http://test.carlton.com/")
             .Respond(HttpStatusCode.OK, "application/json", JsonSerializer.Serialize(expectedResult));
        decorated.SetupQueryDispatcher(expectedResult);

        //Act 
        var actualResult = await sut.Dispatch(sender, context, CancellationToken.None);

        //Assert
        decorated.VerifyQueryDispatcher(1);
        mockHttp.GetMatchCount(request).ShouldBe(1);
        context.RequiresHttpRefresh.ShouldBeTrue();
        context.HttpRefreshOccurred.ShouldBeTrue();
        actualResult.ShouldBe(expectedResult);
    }
}
