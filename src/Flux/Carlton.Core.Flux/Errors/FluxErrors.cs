using Carlton.Core.Flux.Attributes;
namespace Carlton.Core.Flux.Errors;

public static class FluxErrors
{
	public abstract record FluxError(string Message, int EventId);

	public static ValidationError ValidationError(IEnumerable<string> validationErrors)
		=> new(validationErrors);

	public static UnsupportedHttpVerbError UnsupportedHttpVerbError(HttpVerb httpVerb)
		=> new(httpVerb);

	public static HttpUrlConstructionError HttpUrlConstructionError(string url)
		=> new(url);

	public static HttpUrlConstructionUnreplacedTokensError HttpUrlConstructionUnreplacedTokensError(string url, IEnumerable<string> unreplacedTokens)
		=> new(url, unreplacedTokens);

	public static HttpRequestFailedError HttpRequestFailedError(HttpResponseMessage httpResponse)
		=> new(httpResponse);

	public static HttpError HttpError(HttpRequestException exception)
		=> new(exception);

	public static JsonError JsonError(Exception exception)
		=> new(exception);

	public static MutationNotRegisteredError MutationNotRegisteredError(string mutationType)
		=> new(mutationType);

	public static MutationError MutationError(string mutationCommandType, Exception exception)
		=> new(mutationCommandType, exception);

	public static UnhandledFluxError UnhandledFluxError(Exception exception)
		=> new(exception);

	internal static string ReplaceHttpRequestFailedTemplateMessage(HttpResponseMessage httpResponse)
	{
		return string.Format(FluxLogs.Flux_HTTP_FailedRequest_ErrorMsg,
				httpResponse.RequestMessage.Method,
				httpResponse.RequestMessage.RequestUri,
				(int)httpResponse.StatusCode,
				httpResponse.ReasonPhrase);
	}
}

public sealed record ValidationError(IEnumerable<string> ValidationErrors)
	: FluxError($"{FluxLogs.Flux_Validation_ErrorMsg}: {string.Join(Environment.NewLine, ValidationErrors)}", FluxLogs.Flux_Validation_Error);

public sealed record UnsupportedHttpVerbError(HttpVerb HttpVerb)
	: FluxError(string.Format(FluxLogs.Flux_HTTP_UnsupportedVerb_ErrorMsg, HttpVerb), FluxLogs.Flux_HTTP_UnsupportedVerb_Error);

public sealed record HttpUrlConstructionError(string Url)
	: FluxError(string.Format(FluxLogs.Flux_HTTP_Invalid_URL_ErrorMsg, Url), FluxLogs.Flux_HTTP_Invalid_URL_Error);

public sealed record HttpUrlConstructionUnreplacedTokensError(string Url, IEnumerable<string> UnreplacedTokens)
	: FluxError($"{FluxLogs.Flux_HTTP_Invalid_URL_UnreplacedTokens_ErrorMsg}: {string.Join(Environment.NewLine, UnreplacedTokens)}", FluxLogs.Flux_HTTP_Invalid_URL_UnreplacedTokens_Error);

public sealed record HttpRequestFailedError(HttpResponseMessage HttpResponse)
	: FluxError(ReplaceHttpRequestFailedTemplateMessage(HttpResponse), FluxLogs.Flux_HTTP_FailedRequest_Error);

public sealed record HttpError(HttpRequestException Exception)
	: FluxError($"{FluxLogs.Flux_HTTP_ErrorMsg}: {Exception.Message}.", FluxLogs.Flux_HTTP_Error);

public sealed record JsonError(Exception Exception)
	: FluxError($"{FluxLogs.Flux_JSON_ErrorMsg}: {Exception.Message}.", FluxLogs.Flux_JSON_Error);

public sealed record MutationNotRegisteredError(string MutationType)
	: FluxError(string.Format(FluxLogs.Flux_MutationNotRegistered_ErrorMsg, MutationType), FluxLogs.Flux_MutationNotRegistered_Error);

public sealed record MutationError(string MutationCommandType, Exception Exception)
	: FluxError($"{FluxLogs.Flux_Mutation_ErrorMsg} {MutationCommandType}: {Exception.Message}.", FluxLogs.Flux_Mutation_Error);

public sealed record MappingError(string ViewModelType, Exception Exception)
	: FluxError($"{FluxLogs.Flux_Mapping_ErrorMsg} {ViewModelType}: {Exception.Message}.", FluxLogs.Flux_Mutation_Error);

public sealed record UnhandledFluxError(Exception Exception)
	: FluxError($"{FluxLogs.Flux_Unhandled_ErrorMsg}: {Exception.Message}.", FluxLogs.Flux_Unhandled_Error);
