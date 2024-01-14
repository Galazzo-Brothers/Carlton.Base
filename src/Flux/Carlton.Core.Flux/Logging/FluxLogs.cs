using Carlton.Core.Utilities.Disposable;
namespace Carlton.Core.Flux.Logging;

public static class LogEvents
{
    //Logging Scopes
    public const string FluxComponentInitialization = "FluxComponentInitialization: {@FluxComponentInitialization}";
    public const string IsFluxChildRequest = "IsFluxChildRequest : {@IsFluxChildRequest}";
    public const string FluxParentRequestId = "FluxParentRequestId : {@FluxParentRequestId}";
    public const string FluxStateEvent = "FluxStateEvent : {@FluxStateEvent}";
    public const string FluxAction = "FluxAction: {@FluxAction}";
    public const string FluxRequestId = "FluxRequestId: {@FluxRequestId}";
    public const string MutationCommand = "MutationCommand";
    public const string ViewModelQuery = "ViewModelQuery";
    public const string FluxRequestContext = "FluxRequestContext : {@FluxRequestContext}";

    //ViewModel Query Events
    public const int ViewModel_JsInterop_Completed = 1200;
    public const int ViewModel_JsInterop_Error = 1210;

    public const int ViewModel_Completed = 1000;
    public const int ViewModel_Unhandled_Error = 1100;
    public const int ViewModel_HTTP_URL_Error = 1120;
    public const int ViewModel_HTTP_Request_Error = 1130;
    public const int ViewModel_HTTP_Response_JSON_Error = 1140;
    public const int ViewModel_Mapping_Error = 1150;
    public const int ViewModel_Validation_Error = 1160;

    //Mutation Command Start Events
    public const int Mutation_Completed = 2000;
    public const int Mutation_Unhandled_Error = 2100;
    public const int Mutation_Validation_Error = 2210;
    public const int Mutation_HttpInterception_UrlConstruction_Error = 2320;
    public const int Mutation_HttpInterception_Request_Error = 2330;
    public const int Mutation_HttpInterception_Response_JSON_Error = 2340;
    public const int Mutation_HttpInterception_Response_Update_Error = 2350;
    public const int Mutation_Apply_Error = 2410;
    public const int Mutation_SaveLocalStorage_Error = 2510;
    public const int Mutation_SaveLocalStorage_JSON_Error = 2531;

    //ViewModel Query Error Messages
    public const string ViewModel_Unhandled_ErrorMsg = "An unhandled exception occurred during a ViewModelQuery for a ViewModel of type";
    public const string ViewModel_HTTP_ErrorMsg = "An error occurred while communicating with the remote server endpoint for a ViewModel of type";
    public const string ViewModel_HTTP_URL_ErrorMsg = "An error occurred while constructing the remote server endpoint for a ViewMode of type";
    public const string ViewModel_JSON_ErrorMsg = "An error occurred while parsing, serializing or de-serializing JSON for a ViewModel of type";
    public const string ViewModel_Mapping_ErrorMsg = "An error occurred during the mapping of a ViewModel of type";
    public const string ViewModel_Validation_ErrorMsg = "An error occurred while validating ViewModel of type";
    public const string ViewModel_JSInterop_ErrorMsg = "An error occurred during the JSInterop for a ViewModel of type";

    //Mutation Error Messages
    public const string Mutation_Unhandled_ErrorMsg = $"An exception occurred during MutationCommand of type";
    public const string Mutation_Validation_ErrorMsg = "An error occurred while validation MutationCommand of type";
    public const string Mutation_HTTP_ErrorMsg = "An error occurred while communicating with the remote server endpoint for MutationCommand of type";
    public const string Mutation_HTTP_URL_ErrorMsg = "An error occurred while constructing the remote server endpoint for MutationCommand of type";
    public const string Mutation_HTTP_JSON_ErrorMsg = "An error occurred while parsing the JSON HTTP response for MutationCommand of type";
    public const string Mutation_HTTP_Response_Update_ErrorMsg = "An error occurred while updating the command with values from the server response for MutationCommand of type";
    public const string Mutation_Apply_ErrorMsg = "An error occurred while applying MutationCommand of type";
    public const string Mutation_LocalStorage_ErrorMsg = "An error occurred while writing to locastorage for MutationCommand of type";
    public const string Mutation_LocalStorage_JSON_ErrorMsg = "An error occurred while serializing for local storage MutationCommand of type";

    //Specific Http Errors
    public const string InvalidRefreshUrlParametersMsg = "The HTTP refresh endpoint is invalid, following URL parameters were not replaced: ";
    public const string InvalidRefreshUrlMsg = "The HTTP refresh endpoint is invalid";
    public const string InvalidRefreshUrlCreationEnumValueMsg = "Unexpected enum value during creation of HTTP refresh endpoint";
    public const string ErrorUpdatingCommandFromServerResponseMsg = "An error occurred updating the command with the server response of type";


    public static IDisposable GetFluxComponentStateChangedLogginScopes(ILogger logger, FluxStateChangedEventArgs args)
    {
        return new CompositeDisposable
        (
            logger.BeginScope(IsFluxChildRequest, true),
            logger.BeginScope(FluxParentRequestId, args.ParentRequestId),
            logger.BeginScope(FluxStateEvent, args.StateEvent)
        );
    }

    public static IDisposable GetViewModelInitializationLoggingScopes<TViewModel>(ILogger logger)
    {
        return new CompositeDisposable
        (
            logger.BeginScope(FluxComponentInitialization, true)
        );
    }

    public static IDisposable GetViewModelRequestLoggingScopes<TViewModel>(ILogger logger, ViewModelQueryContext<TViewModel> context)
    {
        return new CompositeDisposable
        (
            logger.BeginScope(FluxAction, ViewModelQuery),
            logger.BeginScope(FluxRequestId, context.RequestID),
            logger.BeginScope(FluxRequestContext, context)
        );
    }

    public static IDisposable GetMutationCommandRequestLoggingScopes<TCommand>(ILogger logger, MutationCommandContext<TCommand> context)
    {
        return new CompositeDisposable
        (
            logger.BeginScope(FluxAction, MutationCommand),
            logger.BeginScope(FluxRequestId, context.RequestID),
            logger.BeginScope(FluxRequestContext, context)
        );
    }
}



