using Carlton.Core.Flux.Attributes;
using Carlton.Core.Flux.Handlers.Base;
using System.Net.Http.Json;
namespace Carlton.Core.Flux.Internals.Dispatchers.Mutations.Decorators;

internal sealed class MutationHttpDecorator<TState>(IMutationCommandDispatcher<TState> _decorated, HttpClient _client)
	: BaseHttpDecorator<TState>(_client), IMutationCommandDispatcher<TState>
{
	public async Task<Result<MutationCommandResult, FluxError>> Dispatch<TCommand>(object sender, MutationCommandContext<TCommand> context, CancellationToken cancellationToken)
	{
		var attributes = sender.GetType().GetCustomAttributes();
		var fluxServerAttribute = attributes.OfType<FluxCommandServerUrlAttribute<TCommand>>().FirstOrDefault();
		var requiresRefresh = GetRefreshPolicy(fluxServerAttribute?.ServerCommunicationPolicy);

		//Continue on if no refresh required
		if (requiresRefresh)
		{
			//Update the context for logging and auditing
			context.MarkAsRequiresHttpRefresh();

			//Construct Http Refresh URL
			var urlParameterAttributes = GetParameterAttributes(sender);
			var serverUrlResult = GetServerUrl(fluxServerAttribute, urlParameterAttributes, sender);

			//Send Request
			var response = await SendRequest(fluxServerAttribute.HttpVerb, serverUrlResult, context, fluxServerAttribute.UpdateWithResponseBody, cancellationToken);

			//If http request or parse failed, return error
			if (!response.IsSuccess)
				return response;
		}

		return await _decorated.Dispatch(sender, context, cancellationToken);
	}

	private async Task<Result<MutationCommandResult, FluxError>> SendRequest<TCommand>(
		HttpVerb httpVerb,
		Result<string, FluxError> serverUrlResult,
		MutationCommandContext<TCommand> context,
		bool updateWithResponseBody,
		CancellationToken cancellationToken)
	{
		return await serverUrlResult.Match
		(
			async serverUrl =>
			{
				try
				{
					//Execute Http Request
					Result<HttpResponseMessage, FluxError> response = httpVerb switch
					{
						HttpVerb.POST => await Client.PostAsJsonAsync(serverUrl, context, cancellationToken),
						HttpVerb.PUT => await Client.PutAsJsonAsync(serverUrl, context, cancellationToken),
						HttpVerb.PATCH => await Client.PatchAsJsonAsync(serverUrl, context, cancellationToken),
						HttpVerb.DELETE => await Client.DeleteAsync(serverUrl, cancellationToken),
						_ => UnsupportedHttpVerbError(httpVerb).ToResult<HttpResponseMessage, FluxError>(),
					};

					//Parse Http Response
					return await response.Match
					(
						async serverResponse => await HandleSuccess(context, updateWithResponseBody, serverUrl, serverResponse, cancellationToken),
						err => err.ToResultTask<MutationCommandResult, FluxError>()
					);
				}
				catch (HttpRequestException ex)
				{
					return HttpError(ex);
				}
				catch (JsonException ex)
				{
					return JsonError(ex);
				}
				catch (NotSupportedException ex) when (ex.Message.Contains("Serialization and deserialization"))
				{
					return JsonError(ex);
				}
			},
			err => err.ToResultTask<MutationCommandResult, FluxError>()
		);
	}

	private static async Task<Result<MutationCommandResult, FluxError>> HandleSuccess<TCommand>(
		MutationCommandContext<TCommand> context,
		bool updateWithResponseBody,
		string serverUrl,
		HttpResponseMessage serverResponse,
		CancellationToken cancellationToken)
	{
		//Update Context with Http Request
		context.MarkAsHttpCallMade(serverUrl, System.Net.HttpStatusCode.OK);

		//If http request failed, return error
		if (!serverResponse.IsSuccessStatusCode)
			return HttpRequestFailedError(serverResponse);

		//Check if command should be replaced with response body
		if (updateWithResponseBody)
		{
			//Command should be replaced with server response body
			var serverCommand = await serverResponse.Content.ReadFromJsonAsync<TCommand>(cancellationToken);
			context.MarkAsHttpCallSucceeded(serverCommand);
			context.ReplaceCommandWithResponseBody(serverCommand);
		}
		else if (serverResponse.Content.IsJsonContent())
		{
			//Command will not be replaced but we will still audit the json response
			var responseContent = await serverResponse.Content.ReadFromJsonAsync<object>(cancellationToken);
			context.MarkAsHttpCallSucceeded(responseContent);
		}
		else
		{
			//response is not json, just mark the call as successful and move on
			context.MarkAsHttpCallSucceeded("Response content was not text/json");
		}

		return new MutationCommandResult();
	}
}



