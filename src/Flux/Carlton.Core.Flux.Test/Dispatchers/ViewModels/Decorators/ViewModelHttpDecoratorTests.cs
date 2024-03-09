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
    public async Task HttpDecoratorDispatch_NoHttpRefreshAttribute_ShouldNotMakeHttpCall(
        [Frozen] IViewModelQueryDispatcher<TestState> decorated,
        [Frozen] MockHttpMessageHandler mockHttp,
        [Frozen] IMutableFluxState<TestState> fluxState,
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
        decorated.VerifyQueryDispatcher<TestViewModel>(1);
        mockHttp.GetMatchCount(request).ShouldBe(0);
        await fluxState.DidNotReceiveWithAnyArgs().ApplyMutationCommand<TestViewModel>(default);
        context.RequiresHttpRefresh.ShouldBeFalse();
        context.HttpRefreshOccurred.ShouldBeFalse();
        context.StateModifiedByHttpRefresh.ShouldBeFalse();
        actualResult.ShouldBe(expectedResult);
    }

    [Theory, AutoNSubstituteData]
        public async Task HttpDecoratorDispatch_WithNeverHttpRefreshAttribute_ShouldNotMakeHttpCall(
        [Frozen] IViewModelQueryDispatcher<TestState> decorated,
        [Frozen] MockHttpMessageHandler mockHttp,
        [Frozen] IMutableFluxState<TestState> fluxState,
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
        decorated.VerifyQueryDispatcher<TestViewModel>(1);
        mockHttp.GetMatchCount(request).ShouldBe(0);
        await fluxState.DidNotReceiveWithAnyArgs().ApplyMutationCommand<TestViewModel>(default);
        context.RequiresHttpRefresh.ShouldBeFalse();
        context.HttpRefreshOccurred.ShouldBeFalse();
        context.StateModifiedByHttpRefresh.ShouldBeFalse();
        actualResult.ShouldBe(expectedResult);
    }

    [Theory, AutoNSubstituteData]
    public async Task HttpDecoratorDispatch_WithHttpRefreshAttribute_ShouldMakeHttpCall(
       [Frozen] IViewModelQueryDispatcher<TestState> decorated,
       [Frozen] MockHttpMessageHandler mockHttp,
       [Frozen] IMutableFluxState<TestState> fluxState,
       ViewModelHttpDecorator<TestState> sut,
       HttpRefreshCaller sender,
       ViewModelQueryContext<TestViewModel> context,
       TestViewModel expectedResult)
    {
        //Arrange
        fluxState.ApplyMutationCommand(expectedResult).Returns(expectedResult);
        var request = mockHttp.When(HttpRefreshCaller.MockRefreshUrl)
             .Respond(HttpStatusCode.OK, "application/json", JsonSerializer.Serialize(expectedResult));
        decorated.SetupQueryDispatcher(expectedResult);

        //Act 
        var actualResult = await sut.Dispatch(sender, context, CancellationToken.None);

        //Assert
        decorated.VerifyQueryDispatcher<TestViewModel>(1);
        mockHttp.GetMatchCount(request).ShouldBe(1);
        await fluxState.Received().ApplyMutationCommand(Arg.Is(expectedResult));
        context.RequiresHttpRefresh.ShouldBeTrue();
        context.HttpRefreshOccurred.ShouldBeTrue();
        context.StateModifiedByHttpRefresh.ShouldBeTrue();
        actualResult.ShouldBe(expectedResult);
    }

    [Theory, AutoNSubstituteData]
    public async Task HttpDecoratorDispatch_WithHttpRefreshAttribute_And_ComponentParameters_ShouldMakeHttpCall(
      [Frozen] IViewModelQueryDispatcher<TestState> decorated,
      [Frozen] MockHttpMessageHandler mockHttp,
      [Frozen] IMutableFluxState<TestState> fluxState,
      ViewModelHttpDecorator<TestState> sut,
      HttpRefreshWithComponentParametersCaller sender,
      ViewModelQueryContext<TestViewModel> context,
      TestViewModel expectedResult,
    int clientId,
      int userId)
    {
        //Arrange
        fluxState.ApplyMutationCommand(expectedResult).Returns(expectedResult);
        sender.ClientId = clientId;
        sender.UserId = userId;
        var httpUrl = HttpRefreshWithComponentParametersCaller.MockRefreshUrlTemplate
            .Replace("{ClientId}", clientId.ToString())
            .Replace("{UserId}", userId.ToString());
        var request = mockHttp.When(httpUrl)
            .Respond(HttpStatusCode.OK, "application/json", JsonSerializer.Serialize(expectedResult));
        decorated.SetupQueryDispatcher(expectedResult);

        //Act 
        var actualResult = await sut.Dispatch(sender, context, CancellationToken.None);

        //Assert
        decorated.VerifyQueryDispatcher<TestViewModel>(1);
        mockHttp.GetMatchCount(request).ShouldBe(1);
        await fluxState.Received(1).ApplyMutationCommand(expectedResult);
        context.RequiresHttpRefresh.ShouldBeTrue();
        context.HttpRefreshOccurred.ShouldBeTrue();
        context.StateModifiedByHttpRefresh.ShouldBeTrue();
        actualResult.ShouldBe(expectedResult);
    }

    [Theory, AutoNSubstituteData]
    public async Task HttpDecoratorDispatch_WithInvalidHttpRefreshAttribute_ShouldNotNotMakeHttpCall(
     [Frozen] IViewModelQueryDispatcher<TestState> decorated,
     [Frozen] MockHttpMessageHandler mockHttp,
     [Frozen] IMutableFluxState<TestState> fluxState,
     ViewModelHttpDecorator<TestState> sut,
     FluxServerCommunicationWithInvalidUrl sender,
     ViewModelQueryContext<TestViewModel> context)
    {
        //Arrange
        var expectedError = new HttpUrlConstructionError("http://test.#%$@#carlton.com/clients/");
        var request = mockHttp.When("*");
        decorated.SetupQueryDispatcherError<TestViewModel>(expectedError);

        //Act 
        var actualResult = await sut.Dispatch(sender, context, CancellationToken.None);

        //Assert
        decorated.VerifyQueryDispatcher<TestViewModel>(0);
        mockHttp.GetMatchCount(request).ShouldBe(0);
        await fluxState.DidNotReceiveWithAnyArgs().ApplyMutationCommand<TestViewModel>(default);
        context.RequiresHttpRefresh.ShouldBeTrue();
        context.HttpRefreshOccurred.ShouldBeFalse();
        context.StateModifiedByHttpRefresh.ShouldBeFalse();
        actualResult.ShouldBe(expectedError);
    }

    [Theory, AutoNSubstituteData]
    public async Task HttpDecoratorDispatch_WithHttpRefreshAttribute_And_UnreplacedParameters_ShouldNotMakeHttpCall(
     [Frozen] IViewModelQueryDispatcher<TestState> decorated,
     [Frozen] MockHttpMessageHandler mockHttp,
     [Frozen] IMutableFluxState<TestState> fluxState,
     ViewModelHttpDecorator<TestState> sut,
     HttpRefreshWithComponentUnreplacedParametersCaller sender,
     ViewModelQueryContext<TestViewModel> context,
     TestViewModel expectedResult,
     int clientId,
     int userId)
    {
        //Arrange
        var expectedError = new HttpUrlConstructionUnreplacedTokensError(
            HttpRefreshWithComponentParametersCaller.MockRefreshUrlTemplate,
            new List<string> { "{ClientId}", "{UserId}" });
        sender.ClientId = clientId;
        sender.UserId = userId;
        var httpUrl = HttpRefreshWithComponentParametersCaller.MockRefreshUrlTemplate
            .Replace("{ClientId}", clientId.ToString())
            .Replace("{UserId}", userId.ToString());
        var request = mockHttp.When(httpUrl)
            .Respond(HttpStatusCode.OK, "application/json", JsonSerializer.Serialize(expectedResult));
        decorated.SetupQueryDispatcher(expectedError);

        //Act 
        var actualResult = await sut.Dispatch(sender, context, CancellationToken.None);

        //Assert
        decorated.VerifyQueryDispatcher<TestViewModel>(0);
        mockHttp.GetMatchCount(request).ShouldBe(0);
        await fluxState.DidNotReceiveWithAnyArgs().ApplyMutationCommand<TestViewModel>(default);
        context.RequiresHttpRefresh.ShouldBeTrue();
        context.HttpRefreshOccurred.ShouldBeFalse();
        context.StateModifiedByHttpRefresh.ShouldBeFalse();
        actualResult.IsSuccess.ShouldBeFalse();
        actualResult.GetError().ShouldBeOfType<HttpUrlConstructionUnreplacedTokensError>();
        actualResult.GetError().Message.ShouldBe(expectedError.Message);
    }

    [Theory, AutoNSubstituteData]
    public async Task HttpDecoratorDispatch_With500Response_ShouldReturnError(
       [Frozen] IViewModelQueryDispatcher<TestState> decorated,
       [Frozen] MockHttpMessageHandler mockHttp,
       [Frozen] IMutableFluxState<TestState> fluxState,
       ViewModelHttpDecorator<TestState> sut,
       HttpRefreshCaller sender,
       ViewModelQueryContext<TestViewModel> context,
       TestViewModel expectedResult)
    {
        //Arrange
        
        var request = mockHttp.When(HttpRefreshCaller.MockRefreshUrl)
             .Respond(HttpStatusCode.InternalServerError, "application/json", JsonSerializer.Serialize(expectedResult));
        decorated.SetupQueryDispatcher(expectedResult);

        //Act 
        var actualResult = await sut.Dispatch(sender, context, CancellationToken.None);

        //Assert
        decorated.VerifyQueryDispatcher<TestViewModel>(0);
        mockHttp.GetMatchCount(request).ShouldBe(1);
        await fluxState.DidNotReceiveWithAnyArgs().ApplyMutationCommand<TestViewModel>(default);
        context.RequiresHttpRefresh.ShouldBeTrue();
        context.HttpRefreshOccurred.ShouldBeFalse();
        context.StateModifiedByHttpRefresh.ShouldBeFalse();
        actualResult.IsSuccess.ShouldBeFalse();
        actualResult.GetError().ShouldBeOfType<HttpRequestFailedError>()
            .HttpResponse.StatusCode.ShouldBe(HttpStatusCode.InternalServerError);
    }

    [Theory, AutoNSubstituteData]
    public async Task HttpDecoratorDispatch_WithInvalidJsonResponse_ShouldReturnError(
      [Frozen] IViewModelQueryDispatcher<TestState> decorated,
      [Frozen] MockHttpMessageHandler mockHttp,
      [Frozen] IMutableFluxState<TestState> fluxState,
      ViewModelHttpDecorator<TestState> sut,
      HttpRefreshCaller sender,
      ViewModelQueryContext<TestViewModel> context,
      TestViewModel expectedResult)
    {
        //Arrange

        var request = mockHttp.When(HttpRefreshCaller.MockRefreshUrl)
             .Respond(HttpStatusCode.OK, "application/json", "{3243423;'sdf");
        decorated.SetupQueryDispatcher(expectedResult);

        //Act 
        var actualResult = await sut.Dispatch(sender, context, CancellationToken.None);

        //Assert
        decorated.VerifyQueryDispatcher<TestViewModel>(0);
        mockHttp.GetMatchCount(request).ShouldBe(1);
        await fluxState.DidNotReceiveWithAnyArgs().ApplyMutationCommand<TestViewModel>(default);
        context.RequiresHttpRefresh.ShouldBeTrue();
        context.HttpRefreshOccurred.ShouldBeFalse();
        context.StateModifiedByHttpRefresh.ShouldBeFalse();
        actualResult.IsSuccess.ShouldBeFalse();
        actualResult.GetError().ShouldBeOfType<JsonError>();
    }

    [Theory, AutoNSubstituteData]
    public async Task HttpDecoratorDispatch_WithStateMutationError_ShouldReturnError(
     [Frozen] IViewModelQueryDispatcher<TestState> decorated,
     [Frozen] MockHttpMessageHandler mockHttp,
     [Frozen] IMutableFluxState<TestState> fluxState,
     ViewModelHttpDecorator<TestState> sut,
     HttpRefreshCaller sender,
     ViewModelQueryContext<TestViewModel> context,
     TestViewModel expectedResult,
     MutationNotRegisteredError error)
    {
        //Arrange
        fluxState.ApplyMutationCommand(expectedResult).Returns(error);
        var request = mockHttp.When(HttpRefreshCaller.MockRefreshUrl)
             .Respond(HttpStatusCode.OK, "application/json", JsonSerializer.Serialize(expectedResult));
        decorated.SetupQueryDispatcher(expectedResult);

        //Act 
        var actualResult = await sut.Dispatch(sender, context, CancellationToken.None);

        //Assert
        decorated.VerifyQueryDispatcher<TestViewModel>(0);
        mockHttp.GetMatchCount(request).ShouldBe(1);
        await fluxState.Received(1).ApplyMutationCommand(expectedResult);
        context.RequiresHttpRefresh.ShouldBeTrue();
        context.HttpRefreshOccurred.ShouldBeTrue();
        context.StateModifiedByHttpRefresh.ShouldBeFalse();
        actualResult.IsSuccess.ShouldBeFalse();
        actualResult.GetError().ShouldBeOfType<MutationNotRegisteredError>();
    }
}
