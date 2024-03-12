using Carlton.Core.Flux.Dispatchers;
using Carlton.Core.Utilities.Disposable;
namespace Carlton.Core.Flux.Internals.Logging;

internal static class FluxLogs
{
	//Logging Scopes
	public const string FluxComponentInitialization = "FluxComponentInitialization:{@FluxComponentInitialization}";
	public const string IsFluxChildRequest = "IsFluxChildRequest:{@IsFluxChildRequest}";
	public const string FluxParentRequestId = "FluxParentRequestId:{@FluxParentRequestId}";
	public const string FluxStateEvent = "FluxStateEvent:{@FluxStateEvent}";
	public const string FluxAction = "FluxAction:{@FluxAction}";
	public const string FluxRequestId = "FluxRequestId:{@FluxRequestId}";
	public const string MutationCommand = "MutationCommand";
	public const string ViewModelQuery = "ViewModelQuery";
	public const string FluxRequestContext = "FluxRequestContext:{@FluxRequestContext}";
	public const string ViewModelType = "ViewModelType:{ViewModelType}";
	public const string MutationCommandType = "MutationCommandType:{MutationCommandType}";
	public const string EventIdScope = "EventId:{@EventId}";
	public const string Error = "Error:{@Error}";
	public const string RequestErrored = "RequestErrored:{@RequestErrored}";
	public const string JsModule = "JsModule:{@JsModule}";
	public const string JsFunction = "JsFunction:{@JsFunction}";
	public const string JsParameters = "JsParameters:{@JsParameters}";

	//Flux Completed Events
	public const int JsInterop_Completed = 1000;
	public const int ViewModel_Completed = 2000;
	public const int Mutation_Completed = 3000;

	//Flux Error Events
	public const int Flux_Unhandled_Error = 100;
	public const int Flux_Validation_Error = 101;
	public const int Flux_HTTP_UnsupportedVerb_Error = 102;
	public const int Flux_HTTP_Invalid_URL_Error = 103;
	public const int Flux_HTTP_Invalid_URL_UnreplacedTokens_Error = 104;
	public const int Flux_HTTP_FailedRequest_Error = 105;
	public const int Flux_HTTP_Error = 106;
	public const int Flux_JSON_Error = 107;
	public const int Flux_MutationNotRegistered_Error = 108;
	public const int Flux_Mutation_Error = 109;
	public const int Flux_Mapping_Error = 110;
	public const int Flux_LocalStorage_Error = 111;
	public const int Flux_JsInterop_Error = 112;
	public const int Flux_ComponentRendering_Error = 113;

	//Flux Error Messages
	public const string Flux_Unhandled_ErrorMsg = "An unhandled exception occurred during a flux operation";
	public const string Flux_Validation_ErrorMsg = "An error occurred while validating flux operation of type";
	public const string Flux_HTTP_UnsupportedVerb_ErrorMsg = "{0} is not a currently supported HTTP verb for flux server refresh operations.";
	public const string Flux_HTTP_Invalid_URL_ErrorMsg = "An error occurred while constructing the remote server endpoint, {0} is not a valid URL.";
	public const string Flux_HTTP_Invalid_URL_UnreplacedTokens_ErrorMsg = "The HTTP refresh endpoint is invalid, following URL parameters were not replaced";
	public const string Flux_HTTP_FailedRequest_ErrorMsg = "The HTTP {0} request to {1} failed with status code {2} ({3}).";
	public const string Flux_HTTP_ErrorMsg = "An exception occurred while sending an HTTP request";
	public const string Flux_JSON_ErrorMsg = "An exception occurred while serializing or deserializing a JSON payload";
	public const string Flux_MutationNotRegistered_ErrorMsg = "mutation of type {0} has not been registered";
	public const string Flux_Mutation_ErrorMsg = "An exception occurred while processing mutation of type";
	public const string Flux_Mapping_ErrorMsg = "An exception occurred while mapping to ViewModel of type";
	public const string Flux_LocalStorage_ErrorMsg = "An exception occurred while committing updated state to local storage";
	public const string Flux_JSInterop_ErrorMsg = "An error occurred during the JSInterop for a ViewModel of type";


	//Friendly Error
	public const string FriendlyErrorMsg = "Oops! We are sorry an error has occurred. Please try again.";

	public static IDisposable BeginJsInteropLoggingScopes(this ILogger logger, string jsModule, string jsFunction, object[] jsParameters)
	{
		return new CompositeDisposable
		(
			logger.BeginScope(JsModule, jsModule),
			logger.BeginScope(JsFunction, jsFunction),
			logger.BeginScope(JsParameters, jsParameters)
		);
	}

	public static IDisposable BeginRequestErrorLoggingScopes(this ILogger logger, FluxError error)
	{
		return new CompositeDisposable
		(
			logger.BeginScope(EventIdScope, error.EventId),
			logger.BeginScope(Error, error),
			logger.BeginScope(RequestErrored, true)
		);
	}

	public static IDisposable BeginRequestErrorLoggingScopes(this ILogger logger, int eventId)
	{
		return new CompositeDisposable
		(
			logger.BeginScope(EventIdScope, eventId),
			logger.BeginScope(RequestErrored, true)
		);
	}

	public static IDisposable BeginFluxComponentChildRequestLoggingScopes(this ILogger logger, Guid requestId)
	{
		return new CompositeDisposable
		(
			logger.BeginScope(IsFluxChildRequest, true),
			logger.BeginScope(FluxParentRequestId, requestId)
		);
	}

	public static IDisposable BeginFluxComponentEventReceivedLoggingScopes(this ILogger logger, string stateEvent)
	{
		return new CompositeDisposable
		(
			logger.BeginScope(FluxStateEvent, stateEvent)
		);
	}

	public static IDisposable BeginViewModelInitializationLoggingScopes(this ILogger logger)
	{
		return new CompositeDisposable
		(
			logger.BeginScope(FluxComponentInitialization, true)
		);
	}

	public static IDisposable BeginViewModelRequestLoggingScopes<TViewModel>(this ILogger logger, ViewModelQueryContext<TViewModel> context)
	{
		return new CompositeDisposable
		(
			logger.BeginScope(FluxAction, ViewModelQuery),
			logger.BeginScope(ViewModelType, context.FluxOperationTypeName),
			logger.BeginScope(FluxRequestId, context.RequestId),
			logger.BeginScope(FluxRequestContext, context)
		);
	}

	public static IDisposable BeginMutationCommandRequestLoggingScopes<TCommand>(this ILogger logger, MutationCommandContext<TCommand> context)
	{
		return new CompositeDisposable
		(
			logger.BeginScope(FluxAction, MutationCommand),
			logger.BeginScope(MutationCommandType, context.FluxOperationTypeName),
			logger.BeginScope(FluxRequestId, context.RequestId),
			logger.BeginScope(FluxRequestContext, context)
		);
	}
}



