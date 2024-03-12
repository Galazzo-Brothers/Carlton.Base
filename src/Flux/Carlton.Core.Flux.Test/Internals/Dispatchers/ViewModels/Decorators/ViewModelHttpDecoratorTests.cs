using System.Net;
using System.Text.Json;
using Carlton.Core.Flux.Internals.Contracts;
using Carlton.Core.Flux.Internals.Dispatchers.ViewModels;
using Carlton.Core.Foundation.Tests;
namespace Carlton.Core.Flux.Tests.Internals.Dispatchers.ViewModels.Decorators;

public class ViewModelHttpDecoratorTests
{
	[Theory, AutoNSubstituteData]
	internal async Task HttpDecoratorDispatch_NoHttpRefreshAttribute_ShouldNotMakeHttpCall(
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
		context.HttpCallMade.ShouldBeFalse();
		context.HttpCallSucceeded.ShouldBeFalse();
		context.StateModifiedByHttpRefresh.ShouldBeFalse();
		actualResult.ShouldBe(expectedResult);
	}

	[Theory, AutoNSubstituteData]
	internal async Task HttpDecoratorDispatch_WithNeverHttpRefreshAttribute_ShouldNotMakeHttpCall(
		[Frozen] IViewModelQueryDispatcher<TestState> decorated,
		[Frozen] MockHttpMessageHandler mockHttp,
		[Frozen] IMutableFluxState<TestState> fluxState,
		ViewModelHttpDecorator<TestState> sut,
		FluxServerCommunicationNever sender,
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
		context.HttpCallMade.ShouldBeFalse();
		context.HttpCallSucceeded.ShouldBeFalse();
		context.StateModifiedByHttpRefresh.ShouldBeFalse();
		actualResult.ShouldBe(expectedResult);
	}

	[Theory, AutoNSubstituteData]
	internal async Task HttpDecoratorDispatch_WithHttpRefreshAttribute_ShouldMakeHttpCall(
		[Frozen] IViewModelQueryDispatcher<TestState> decorated,
		[Frozen] MockHttpMessageHandler mockHttp,
		[Frozen] IMutableFluxState<TestState> fluxState,
		ViewModelHttpDecorator<TestState> sut,
		FluxServerCommunicationAlwaysGet sender,
		ViewModelQueryContext<TestViewModel> context,
		TestViewModel expectedResult)
	{
		//Arrange
		fluxState.ApplyMutationCommand(expectedResult).Returns(expectedResult);
		var request = mockHttp.When(FluxServerCommunicationAlwaysGet.MockRefreshUrl)
			 .Respond(HttpStatusCode.OK, "application/json", JsonSerializer.Serialize(expectedResult));
		decorated.SetupQueryDispatcher(expectedResult);

		//Act 
		var actualResult = await sut.Dispatch(sender, context, CancellationToken.None);

		//Assert
		decorated.VerifyQueryDispatcher<TestViewModel>(1);
		mockHttp.GetMatchCount(request).ShouldBe(1);
		await fluxState.Received().ApplyMutationCommand(Arg.Is(expectedResult));
		context.RequiresHttpRefresh.ShouldBeTrue();
		context.HttpCallMade.ShouldBeTrue();
		context.HttpCallSucceeded.ShouldBeTrue();
		context.StateModifiedByHttpRefresh.ShouldBeTrue();
		actualResult.ShouldBe(expectedResult);
	}

	[Theory, AutoNSubstituteData]
	internal async Task HttpDecoratorDispatch_WithHttpRefreshAttribute_And_ComponentParameters_ShouldMakeHttpCall(
		[Frozen] IViewModelQueryDispatcher<TestState> decorated,
		[Frozen] MockHttpMessageHandler mockHttp,
		[Frozen] IMutableFluxState<TestState> fluxState,
		ViewModelHttpDecorator<TestState> sut,
		FluxServerCommunicationWithComponentParametersGet sender,
		ViewModelQueryContext<TestViewModel> context,
		TestViewModel expectedResult,
		int clientId,
		int userId)
	{
		//Arrange
		fluxState.ApplyMutationCommand(expectedResult).Returns(expectedResult);
		sender.ClientId = clientId;
		sender.UserId = userId;
		var httpUrl = FluxServerCommunicationWithComponentParametersGet.MockRefreshUrlTemplate
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
		context.HttpCallMade.ShouldBeTrue();
		context.HttpCallSucceeded.ShouldBeTrue();
		context.StateModifiedByHttpRefresh.ShouldBeTrue();
		actualResult.ShouldBe(expectedResult);
	}

	[Theory, AutoNSubstituteData]
	internal async Task HttpDecoratorDispatch_WithInvalidHttpRefreshAttribute_ShouldNotNotMakeHttpCall(
		[Frozen] IViewModelQueryDispatcher<TestState> decorated,
		[Frozen] MockHttpMessageHandler mockHttp,
		[Frozen] IMutableFluxState<TestState> fluxState,
		ViewModelHttpDecorator<TestState> sut,
		FluxServerCommunicationWithInvalidUrlGet sender,
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
		context.HttpCallMade.ShouldBeFalse();
		context.HttpCallSucceeded.ShouldBeFalse();
		context.StateModifiedByHttpRefresh.ShouldBeFalse();
		actualResult.ShouldBe(expectedError);
	}

	[Theory, AutoNSubstituteData]
	internal async Task HttpDecoratorDispatch_WithHttpRefreshAttribute_And_UnreplacedParameters_ShouldNotMakeHttpCall(
		[Frozen] IViewModelQueryDispatcher<TestState> decorated,
		[Frozen] MockHttpMessageHandler mockHttp,
		[Frozen] IMutableFluxState<TestState> fluxState,
		ViewModelHttpDecorator<TestState> sut,
		FluxServerCommunicationWithUnreplacedParametersGet sender,
		ViewModelQueryContext<TestViewModel> context,
		TestViewModel expectedResult,
		int clientId,
		int userId)
	{
		//Arrange
		var expectedError = new HttpUrlConstructionUnreplacedTokensError(
			FluxServerCommunicationWithComponentParametersGet.MockRefreshUrlTemplate,
			new List<string> { "{ClientId}", "{UserId}" });
		sender.ClientId = clientId;
		sender.UserId = userId;
		var httpUrl = FluxServerCommunicationWithComponentParametersGet.MockRefreshUrlTemplate
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
		context.HttpCallMade.ShouldBeFalse();
		context.HttpCallSucceeded.ShouldBeFalse();
		context.StateModifiedByHttpRefresh.ShouldBeFalse();
		actualResult.IsSuccess.ShouldBeFalse();
		actualResult.GetError().ShouldBeOfType<HttpUrlConstructionUnreplacedTokensError>();
		actualResult.GetError().Message.ShouldBe(expectedError.Message);
	}

	[Theory, AutoNSubstituteData]
	internal async Task HttpDecoratorDispatch_With500Response_ShouldReturnError(
		[Frozen] IViewModelQueryDispatcher<TestState> decorated,
		[Frozen] MockHttpMessageHandler mockHttp,
		[Frozen] IMutableFluxState<TestState> fluxState,
		ViewModelHttpDecorator<TestState> sut,
		FluxServerCommunicationAlwaysGet sender,
		ViewModelQueryContext<TestViewModel> context,
		TestViewModel expectedResult)
	{
		//Arrange

		var request = mockHttp.When(FluxServerCommunicationAlwaysGet.MockRefreshUrl)
				.Respond(HttpStatusCode.InternalServerError, "application/json", JsonSerializer.Serialize(expectedResult));
		decorated.SetupQueryDispatcher(expectedResult);

		//Act 
		var actualResult = await sut.Dispatch(sender, context, CancellationToken.None);

		//Assert
		decorated.VerifyQueryDispatcher<TestViewModel>(0);
		mockHttp.GetMatchCount(request).ShouldBe(1);
		await fluxState.DidNotReceiveWithAnyArgs().ApplyMutationCommand<TestViewModel>(default);
		context.RequiresHttpRefresh.ShouldBeTrue();
		context.HttpCallMade.ShouldBeTrue();
		context.HttpCallSucceeded.ShouldBeFalse();
		context.StateModifiedByHttpRefresh.ShouldBeFalse();
		actualResult.IsSuccess.ShouldBeFalse();
		actualResult.GetError().ShouldBeOfType<HttpRequestFailedError>()
			.HttpResponse.StatusCode.ShouldBe(HttpStatusCode.InternalServerError);
	}

	[Theory, AutoNSubstituteData]
	internal async Task HttpDecoratorDispatch_WithInvalidJsonResponse_ShouldReturnError(
		[Frozen] IViewModelQueryDispatcher<TestState> decorated,
		[Frozen] MockHttpMessageHandler mockHttp,
		[Frozen] IMutableFluxState<TestState> fluxState,
		ViewModelHttpDecorator<TestState> sut,
		FluxServerCommunicationAlwaysGet sender,
		ViewModelQueryContext<TestViewModel> context,
		TestViewModel expectedResult)
	{
		//Arrange

		var request = mockHttp.When(FluxServerCommunicationAlwaysGet.MockRefreshUrl)
				.Respond(HttpStatusCode.OK, "application/json", "{3243423;'sdf");
		decorated.SetupQueryDispatcher(expectedResult);

		//Act 
		var actualResult = await sut.Dispatch(sender, context, CancellationToken.None);

		//Assert
		decorated.VerifyQueryDispatcher<TestViewModel>(0);
		mockHttp.GetMatchCount(request).ShouldBe(1);
		await fluxState.DidNotReceiveWithAnyArgs().ApplyMutationCommand<TestViewModel>(default);
		context.RequiresHttpRefresh.ShouldBeTrue();
		context.HttpCallMade.ShouldBeTrue();
		context.HttpCallSucceeded.ShouldBeFalse();
		context.StateModifiedByHttpRefresh.ShouldBeFalse();
		actualResult.IsSuccess.ShouldBeFalse();
		actualResult.GetError().ShouldBeOfType<JsonError>();
	}

	[Theory, AutoNSubstituteData]
	internal async Task HttpDecoratorDispatch_WithStateMutationError_ShouldReturnError(
		[Frozen] IViewModelQueryDispatcher<TestState> decorated,
		[Frozen] MockHttpMessageHandler mockHttp,
		[Frozen] IMutableFluxState<TestState> fluxState,
		ViewModelHttpDecorator<TestState> sut,
		FluxServerCommunicationAlwaysGet sender,
		ViewModelQueryContext<TestViewModel> context,
		TestViewModel expectedResult,
		MutationNotRegisteredError error)
	{
		//Arrange
		fluxState.ApplyMutationCommand(expectedResult).Returns(error);
		var request = mockHttp.When(FluxServerCommunicationAlwaysGet.MockRefreshUrl)
			 .Respond(HttpStatusCode.OK, "application/json", JsonSerializer.Serialize(expectedResult));
		decorated.SetupQueryDispatcher(expectedResult);

		//Act 
		var actualResult = await sut.Dispatch(sender, context, CancellationToken.None);

		//Assert
		decorated.VerifyQueryDispatcher<TestViewModel>(0);
		mockHttp.GetMatchCount(request).ShouldBe(1);
		await fluxState.Received(1).ApplyMutationCommand(expectedResult);
		context.RequiresHttpRefresh.ShouldBeTrue();
		context.HttpCallMade.ShouldBeTrue();
		context.HttpCallSucceeded.ShouldBeTrue();
		context.StateModifiedByHttpRefresh.ShouldBeFalse();
		actualResult.IsSuccess.ShouldBeFalse();
		actualResult.GetError().ShouldBeOfType<MutationNotRegisteredError>();
	}
}
