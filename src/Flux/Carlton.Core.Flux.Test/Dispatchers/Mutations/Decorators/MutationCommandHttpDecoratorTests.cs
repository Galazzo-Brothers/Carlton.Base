using Carlton.Core.Flux.Contracts;
using Carlton.Core.Foundation.Test;
using Carlton.Core.Flux.Dispatchers.Mutations.Decorators;
using Carlton.Core.Flux.Dispatchers.Mutations;
using System.Net;
using System.Text.Json;
namespace Carlton.Core.Flux.Tests.Dispatchers.Mutations.Decorators;

public class MutationCommandHttpDecoratorTests
{
	[Theory, AutoNSubstituteData]
	public async Task HttpDecoratorDispatch_NoHttpRefreshAttribute_ShouldNotMakeHttpCall(
	   [Frozen] IMutationCommandDispatcher<TestState> decorated,
	   [Frozen] MockHttpMessageHandler mockHttp,
	   MutationHttpDecorator<TestState> sut,
	   object sender,
	   MutationCommandContext<TestCommand1> context,
	   MutationCommandResult expectedResult)
	{
		//Arrange
		var request = mockHttp.When("*");
		decorated.SetupCommandDispatcher(context.MutationCommand);

		//Act 
		var actualResult = await sut.Dispatch(sender, context, CancellationToken.None);

		//Assert
		decorated.VerifyCommandDispatcher(1, context.MutationCommand);
		mockHttp.GetMatchCount(request).ShouldBe(0);
		context.RequiresHttpRefresh.ShouldBeFalse();
		context.HttpRefreshOccurred.ShouldBeFalse();
		actualResult.ShouldBe(expectedResult);
	}

	[Theory, AutoNSubstituteData]
	public async Task HttpDecoratorDispatch_WithNeverHttpRefreshAttribute_ShouldNotMakeHttpCall(
	  [Frozen] IMutationCommandDispatcher<TestState> decorated,
	  [Frozen] MockHttpMessageHandler mockHttp,
	  MutationHttpDecorator<TestState> sut,
	  FluxServerCommunicationNever sender,
	  MutationCommandContext<FluxServerCommunicationNever> context,
	  MutationCommandResult expectedResult)
	{
		//Arrange
		var request = mockHttp.When("*");
		decorated.SetupCommandDispatcher(context.MutationCommand);

		//Act 
		var actualResult = await sut.Dispatch(sender, context, CancellationToken.None);

		//Assert
		decorated.VerifyCommandDispatcher(1, context.MutationCommand);
		mockHttp.GetMatchCount(request).ShouldBe(0);
		context.RequiresHttpRefresh.ShouldBeFalse();
		context.HttpRefreshOccurred.ShouldBeFalse();
		actualResult.ShouldBe(expectedResult);
	}

	[Theory, AutoNSubstituteData]
	public async Task HttpDecoratorDispatch_WithUnsupportedHttpVerb_ShouldNotMakeHttpCall(
		[Frozen] IMutationCommandDispatcher<TestState> decorated,
		[Frozen] MockHttpMessageHandler mockHttp,
		MutationHttpDecorator<TestState> sut,
		FluxServerCommunicationAlwaysGet sender,
		MutationCommandContext<FluxServerCommunicationAlwaysGet> context)
	{
		//Arrange
		var request = mockHttp.When("*");
		decorated.SetupCommandDispatcher(context.MutationCommand);

		//Act 
		var actualResult = await sut.Dispatch(sender, context, CancellationToken.None);

		//Assert
		decorated.VerifyCommandDispatcher(0, context.MutationCommand);
		mockHttp.GetMatchCount(request).ShouldBe(0);
		context.RequiresHttpRefresh.ShouldBeTrue();
		context.HttpRefreshOccurred.ShouldBeFalse();
		actualResult.GetError().ShouldBeOfType<UnsupportedHttpVerbError>();
	}


	[Theory, AutoNSubstituteData]
	public async Task HttpDecoratorDispatch_WithHttpRefreshAttribute_ShouldMakeHttpCall(
	  [Frozen] IMutationCommandDispatcher<TestState> decorated,
	  [Frozen] MockHttpMessageHandler mockHttp,
	  MutationHttpDecorator<TestState> sut,
	  FluxServerCommunicationAlwaysPost sender,
	  MutationCommandContext<FluxServerCommunicationAlwaysPost> context,
	  MutationCommandResult expectedResult)
	{
		//Arrange
		var request = mockHttp.When(FluxServerCommunicationAlwaysPost.MockRefreshUrl)
			.Respond(HttpStatusCode.OK, "application/json", JsonSerializer.Serialize(expectedResult));
		decorated.SetupCommandDispatcher(context.MutationCommand);

		//Act 
		var actualResult = await sut.Dispatch(sender, context, CancellationToken.None);

		//Assert
		decorated.VerifyCommandDispatcher(1, context.MutationCommand);
		mockHttp.GetMatchCount(request).ShouldBe(1);
		context.RequiresHttpRefresh.ShouldBeTrue();
		context.HttpRefreshOccurred.ShouldBeTrue();
		actualResult.ShouldBe(expectedResult);
	}
}
