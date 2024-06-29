using System.Net;
using System.Text.Json;
using Carlton.Core.Flux.Internals.Dispatchers.Mutations;
using Carlton.Core.Foundation.Tests;
namespace Carlton.Core.Flux.Tests.Internals.Dispatchers.Mutations.Decorators;

public class MutationCommandHttpDecoratorTests
{
	[Theory, AutoNSubstituteData]
	internal async Task HttpDecoratorDispatch_NoHttpRefreshAttribute_ShouldNotMakeHttpCall(
	   [Frozen] IMutationCommandDispatcher<TestState> decorated,
	   [Frozen] MockHttpMessageHandler mockHttp,
	   MutationHttpDecorator<TestState> sut,
	   object sender,
	   MutationCommandContext<TestCommand> context,
	   MutationCommandResult expectedResult)
	{
		//Arrange
		var request = mockHttp.When("*");
		decorated.SetupCommandDispatcher(context.MutationCommand, expectedResult);

		//Act 
		var actualResult = await sut.Dispatch(sender, context, CancellationToken.None);

		//Assert
		decorated.VerifyCommandDispatcher(1, context.MutationCommand);
		mockHttp.GetMatchCount(request).ShouldBe(0);
		context.RequiresHttpRefresh.ShouldBeFalse();
		context.HttpCallMade.ShouldBeFalse();
		context.HttpCallSucceeded.ShouldBeFalse();
		context.CommandReplacedByResponse.ShouldBeFalse();
		actualResult.ShouldBe(expectedResult);
	}

	[Theory, AutoNSubstituteData]
	internal async Task HttpDecoratorDispatch_WithNeverHttpRefreshAttribute_ShouldNotMakeHttpCall(
	  [Frozen] IMutationCommandDispatcher<TestState> decorated,
	  [Frozen] MockHttpMessageHandler mockHttp,
	  MutationHttpDecorator<TestState> sut,
	  FluxServerCommunicationNever sender,
	  MutationCommandContext<FluxServerCommunicationNever> context,
	  MutationCommandResult expectedResult)
	{
		//Arrange
		var request = mockHttp.When("*");
		decorated.SetupCommandDispatcher(context.MutationCommand, expectedResult);

		//Act 
		var actualResult = await sut.Dispatch(sender, context, CancellationToken.None);

		//Assert
		decorated.VerifyCommandDispatcher(1, context.MutationCommand);
		mockHttp.GetMatchCount(request).ShouldBe(0);
		context.RequiresHttpRefresh.ShouldBeFalse();
		context.HttpCallMade.ShouldBeFalse();
		context.HttpCallSucceeded.ShouldBeFalse();
		context.CommandReplacedByResponse.ShouldBeFalse();
		actualResult.ShouldBe(expectedResult);
	}

	[Theory, AutoNSubstituteData]
	internal async Task HttpDecoratorDispatch_WithUnsupportedHttpVerb_ShouldReturnError(
		[Frozen] IMutationCommandDispatcher<TestState> decorated,
		[Frozen] MockHttpMessageHandler mockHttp,
		MutationHttpDecorator<TestState> sut,
		FluxServerCommunicationAlwaysGet sender,
		MutationCommandContext<TestCommand> context,
		MutationCommandResult expectedResult)
	{
		//Arrange
		var request = mockHttp.When("*");
		decorated.SetupCommandDispatcher(context.MutationCommand, expectedResult);

		//Act 
		var actualResult = await sut.Dispatch(sender, context, CancellationToken.None);

		//Assert
		decorated.VerifyCommandDispatcher(0, context.MutationCommand);
		mockHttp.GetMatchCount(request).ShouldBe(0);
		context.RequiresHttpRefresh.ShouldBeTrue();
		context.HttpCallMade.ShouldBeFalse();
		context.HttpCallSucceeded.ShouldBeFalse();
		context.CommandReplacedByResponse.ShouldBeFalse();
		actualResult.GetError().ShouldBeOfType<UnsupportedHttpVerbError>();
	}


	[Theory, AutoNSubstituteData]
	internal async Task HttpDecoratorDispatch_WithHttpRefreshAttribute_ShouldMakeHttpCall(
		[Frozen] IMutationCommandDispatcher<TestState> decorated,
		[Frozen] MockHttpMessageHandler mockHttp,
		MutationHttpDecorator<TestState> sut,
		FluxServerCommunicationAlwaysPost sender,
		MutationCommandContext<TestCommand> context,
		MutationCommandResult expectedResult,
		object expectedResponse)
	{
		//Arrange
		decorated.SetupCommandDispatcher(context.MutationCommand, expectedResult);
		var request = mockHttp.When(FluxServerCommunicationAlwaysPost.MockRefreshUrl)
			.Respond(HttpStatusCode.OK, "application/json", JsonSerializer.Serialize(expectedResponse));

		//Act 
		var actualResult = await sut.Dispatch(sender, context, CancellationToken.None);

		//Assert
		decorated.VerifyCommandDispatcher(1, context.MutationCommand);
		mockHttp.GetMatchCount(request).ShouldBe(1);
		context.RequiresHttpRefresh.ShouldBeTrue();
		context.HttpCallMade.ShouldBeTrue();
		context.HttpCallSucceeded.ShouldBeTrue();
		context.CommandReplacedByResponse.ShouldBeFalse();
		actualResult.ShouldBe(expectedResult);
	}

	[Theory, AutoNSubstituteData]
	internal async Task HttpDecoratorDispatch_WithHttpRefreshAttribute_And_UpdateWithResponseBody_ShouldMakeHttpCall(
		[Frozen] IMutationCommandDispatcher<TestState> decorated,
		[Frozen] MockHttpMessageHandler mockHttp,
		MutationHttpDecorator<TestState> sut,
		FluxServerCommunicationAlwaysPostUpdateWithResponseBody sender,
		TestCommand initCommand,
		TestCommand newCommand,
		MutationCommandResult expectedResult)
	{
		//Arrange
		decorated.SetupCommandDispatcher(newCommand, expectedResult);
		var context = new MutationCommandContext<TestCommand>(initCommand);
		var request = mockHttp.When(FluxServerCommunicationAlwaysPost.MockRefreshUrl)
			.Respond(HttpStatusCode.OK, "application/json", JsonSerializer.Serialize(newCommand));

		//Act 
		var actualResult = await sut.Dispatch(sender, context, CancellationToken.None);

		//Assert
		decorated.VerifyCommandDispatcher(1, newCommand);
		mockHttp.GetMatchCount(request).ShouldBe(1);
		context.RequiresHttpRefresh.ShouldBeTrue();
		context.HttpCallMade.ShouldBeTrue();
		context.HttpCallSucceeded.ShouldBeTrue();
		context.CommandReplacedByResponse.ShouldBeTrue();
		context.InitialCommand.ShouldBe(initCommand);
		context.MutationCommand.ShouldBe(newCommand);
		actualResult.ShouldBe(expectedResult);
	}

	[Theory, AutoNSubstituteData]
	internal async Task HttpDecoratorDispatch_WithHttpRefreshAttribute_And_ComponentParameters_ShouldMakeHttpCall(
		[Frozen] IMutationCommandDispatcher<TestState> decorated,
		[Frozen] MockHttpMessageHandler mockHttp,
		MutationHttpDecorator<TestState> sut,
		FluxServerCommunicationWithComponentParametersPost sender,
		MutationCommandContext<TestCommand> context,
		MutationCommandResult expectedResult,
		object expectedResponse,
		int clientId,
		int userId)
	{
		//Arrange
		decorated.SetupCommandDispatcher(context.MutationCommand, expectedResult);
		sender.ClientId = clientId;
		sender.UserId = userId;
		var httpUrl = FluxServerCommunicationWithComponentParametersPost.MockRefreshUrlTemplate
			.Replace("{ClientId}", clientId.ToString())
			.Replace("{UserId}", userId.ToString());
		var request = mockHttp.When(httpUrl)
			.Respond(HttpStatusCode.OK, "application/json", JsonSerializer.Serialize(expectedResponse));

		//Act 
		var actualResult = await sut.Dispatch(sender, context, CancellationToken.None);

		//Assert
		decorated.VerifyCommandDispatcher(1, context.MutationCommand);
		mockHttp.GetMatchCount(request).ShouldBe(1);
		context.RequiresHttpRefresh.ShouldBeTrue();
		context.HttpCallMade.ShouldBeTrue();
		context.HttpCallSucceeded.ShouldBeTrue();
		context.CommandReplacedByResponse.ShouldBeFalse();
		actualResult.ShouldBe(expectedResult);
	}


	[Theory, AutoNSubstituteData]
	internal async Task HttpDecoratorDispatch_WithInvalidHttpRefreshAttribute_ShouldNotNotMakeHttpCall(
		[Frozen] IMutationCommandDispatcher<TestState> decorated,
		[Frozen] MockHttpMessageHandler mockHttp,
		MutationHttpDecorator<TestState> sut,
		FluxServerCommunicationWithInvalidUrlPost sender,
		MutationCommandContext<TestCommand> context,
		MutationCommandResult expectedResult)
	{
		//Arrange
		decorated.SetupCommandDispatcher(context.MutationCommand, expectedResult);
		var expectedError = new HttpUrlConstructionError("http://test.#%$@#carlton.com/clients/");
		var request = mockHttp.When("*");

		//Act 
		var actualResult = await sut.Dispatch(sender, context, CancellationToken.None);

		//Assert
		decorated.VerifyCommandDispatcher(0, context.MutationCommand);
		mockHttp.GetMatchCount(request).ShouldBe(0);
		context.RequiresHttpRefresh.ShouldBeTrue();
		context.HttpCallMade.ShouldBeFalse();
		context.HttpCallSucceeded.ShouldBeFalse();
		context.CommandReplacedByResponse.ShouldBeFalse();
		actualResult.ShouldBe(expectedError);
	}

	[Theory, AutoNSubstituteData]
	internal async Task HttpDecoratorDispatch_WithHttpRefreshAttribute_And_UnreplacedParameters_ShouldNotMakeHttpCall(
		[Frozen] IMutationCommandDispatcher<TestState> decorated,
		[Frozen] MockHttpMessageHandler mockHttp,
		MutationHttpDecorator<TestState> sut,
		FluxServerCommunicationWithUnreplacedParametersPost sender,
		MutationCommandContext<TestCommand> context,
		int clientId,
		int userId)
	{
		//Arrange
		var request = mockHttp.When("*");
		var expectedError = new HttpUrlConstructionUnreplacedTokensError(
			FluxServerCommunicationWithUnreplacedParametersPost.MockRefreshUrlTemplate,
			new List<string> { "{ClientId}", "{UserId}" });
		decorated.SetupCommandDispatcherError(context.MutationCommand, expectedError);
		sender.ClientId = clientId;
		sender.UserId = userId;

		//Act 
		var actualResult = await sut.Dispatch(sender, context, CancellationToken.None);

		//Assert
		decorated.VerifyCommandDispatcher(0, context.MutationCommand);
		mockHttp.GetMatchCount(request).ShouldBe(0);
		context.RequiresHttpRefresh.ShouldBeTrue();
		context.HttpCallMade.ShouldBeFalse();
		context.HttpCallSucceeded.ShouldBeFalse();
		context.CommandReplacedByResponse.ShouldBeFalse();
		actualResult.IsSuccess.ShouldBeFalse();
		actualResult.GetError().ShouldBeOfType<HttpUrlConstructionUnreplacedTokensError>();
		actualResult.GetError().Message.ShouldBe(expectedError.Message);
	}

	[Theory, AutoNSubstituteData]
	internal async Task HttpDecoratorDispatch_With500Response_ShouldReturnError(
		[Frozen] IMutationCommandDispatcher<TestState> decorated,
		[Frozen] MockHttpMessageHandler mockHttp,
		MutationHttpDecorator<TestState> sut,
		FluxServerCommunicationAlwaysPost sender,
		MutationCommandContext<TestCommand> context,
		object expectedResponse)
	{
		//Arrange
		var request = mockHttp.When(FluxServerCommunicationAlwaysPost.MockRefreshUrl)
				.Respond(HttpStatusCode.InternalServerError, "application/json", JsonSerializer.Serialize(expectedResponse));

		//Act 
		var actualResult = await sut.Dispatch(sender, context, CancellationToken.None);

		//Assert
		decorated.VerifyCommandDispatcher(0, context.MutationCommand);
		mockHttp.GetMatchCount(request).ShouldBe(1);
		context.RequiresHttpRefresh.ShouldBeTrue();
		context.HttpCallMade.ShouldBeTrue();
		context.HttpCallSucceeded.ShouldBeFalse();
		context.CommandReplacedByResponse.ShouldBeFalse();
		actualResult.IsSuccess.ShouldBeFalse();
		actualResult.GetError().ShouldBeOfType<HttpRequestFailedError>()
			.HttpResponse.StatusCode.ShouldBe(HttpStatusCode.InternalServerError);
	}

	[Theory, AutoNSubstituteData]
	internal async Task HttpDecoratorDispatch_WithInvalidJsonResponse_ShouldReturnError(
		[Frozen] IMutationCommandDispatcher<TestState> decorated,
		[Frozen] MockHttpMessageHandler mockHttp,
		MutationHttpDecorator<TestState> sut,
		FluxServerCommunicationAlwaysPost sender,
		MutationCommandContext<TestCommand> context)
	{
		//Arrange
		var request = mockHttp.When(FluxServerCommunicationAlwaysPost.MockRefreshUrl)
				.Respond(HttpStatusCode.OK, "application/json", "{3243423;'sdf");

		//Act 
		var actualResult = await sut.Dispatch(sender, context, CancellationToken.None);

		//Assert
		decorated.VerifyCommandDispatcher(0, context.MutationCommand);
		mockHttp.GetMatchCount(request).ShouldBe(1);
		context.RequiresHttpRefresh.ShouldBeTrue();
		context.HttpCallMade.ShouldBeTrue();
		context.HttpCallSucceeded.ShouldBeFalse();
		context.CommandReplacedByResponse.ShouldBeFalse();
		actualResult.IsSuccess.ShouldBeFalse();
		actualResult.GetError().ShouldBeOfType<JsonError>();
	}
}
