namespace Carlton.Core.Flux.Logging;

public static class LogEvents
{
    //Logging Scopes
    public const string FluxComponentInitialization = "FluxComponentInitialization";
    public const string FluxComponentStateEvent = "FluxComponentStateEvent : {StateEvent}";
    public const string FluxAction = "FluxAction: {@FluxAction}";
    public const string MutationCommand = "MutationCommand";
    public const string ViewModelQuery = "ViewModelQuery";
    public const string ViewModelScope = "ViewModelQueryContext : {@ViewModelQueryContext}";
    public const string CommandScope = "MutationCommandContext : {@MutationCommandContext}";

    //DataComponent Events
    public const int DataWrapper_OnInitialized_Started = 1000;
    public const int DataWrapper_OnInitialized_Completed = 1100;

    public const int DataWrapper_Command_Dispatch_Started = 1001;
    public const int DataWrapper_Command_Dispatch_Completed = 1101;

    public const int DataWrapper_Event_Received_Started = 1002;
    public const int DataWrapper_Event_Received_Completed = 1102;

    public const int DataWrapper_Error = 1200;

    public const int DataWrapper_Event_Listening = 1301;
    public const int DataWrapper_Event_Skipped = 1302;

    //ViewModel Query Events
    public const int ViewModel_JsInterop_Completed = 2010;
    public const int ViewModel_JsInterop_Error = 2110;

    public const int ViewModel_Completed = 2000;
    public const int ViewModel_Unhandled_Error = 2100;

    public const int ViewModel_HttpRefresh_Completed = 2200;
    public const int ViewModel_HttpRefresh_Skipped = 2210;
    public const int ViewModel_HTTP_URL_Error = 2220;
    public const int ViewModel_HTTP_Request_Error = 2230;
    public const int ViewModel_HTTP_Response_JSON_Error = 2240;

    public const int ViewModel_Mapping_Completed = 2300;
    public const int ViewModel_Mapping_Error = 2310;

    public const int ViewModel_Validation_Completed = 2400;
    public const int ViewModel_Validation_Error = 2410;

    //Mutation Command Start Events
    public const int Mutation_Completed = 3000;
    public const int Mutation_Error = 3100;

    public const int Mutation_Validation_Completed = 3200;
    public const int Mutation_Validation_Error = 3210;

    public const int Mutation_HttpInterception_Completed = 3300;
    public const int Mutation_HttpInterception_Skipped = 3310;
    public const int Mutation_HttpInterception_UrlConstruction_Error = 3320;
    public const int Mutation_HttpInterception_Request_Error = 3330;
    public const int Mutation_HttpInterception_Response_JSON_Error = 3340;
    public const int Mutation_HttpInterception_Response_Update_Error = 3350;

    public const int Mutation_Apply_Completed = 3400;
    public const int Mutation_Apply_Error = 3410;

    public const int Mutation_LocalStorage_Started = 3500;
    public const int Mutation_SaveLocalStorage_Error = 3510;
    public const int Mutation_SaveLocalStorage_Completed = 3520;
    public const int Mutation_SaveLocalStorage_JSON_Error = 3531;

    //ViewModel Query Error Messages
    public const string ViewModel_Unhandled_ErrorMsg = $"An unhandled exception occurred during a ViewModelQuery";
    public const string ViewModel_HTTP_ErrorMsg = "An error occurred while communicating with the remote server endpoint for a ViewModel";
    public const string ViewModel_HTTP_URL_ErrorMsg = "An error occurred while constructing the remote server endpoint for a ViewModel";
    public const string ViewModel_JSON_ErrorMsg = "An error occurred while parsing, serializing or de-serializing JSON for a ViewModel";
    public const string ViewModel_Mapping_ErrorMsg = "An error occurred during the mapping of a ViewModel";
    public const string ViewModel_Validation_ErrorMsg = "An error occurred during the validation of a ViewModel";
    public const string ViewModel_JSInterop_ErrorMsg = "An error occurred during the JSInterop for a ViewModel";

    //Mutation Error Messages
    public const string Mutation_Unhandled_ErrorMsg = $"An exception occurred during a MutationCommand";
    public const string Mutation_HTTP_ErrorMsg = "An error occurred while communicating with the remote server endpoint for a Mutation Command";
    public const string Mutation_HTTP_URL_ErrorMsg = "An error occurred while constructing the remote server endpoint for a Mutation Command";
    public const string Mutation_HTTP_JSON_ErrorMsg = "An error occurred while parsing, serializing or de-serializing a Mutation Command for an HTTP call";
    public const string Mutation_HTTP_Response_Update_ErrorMsg = "An error occurred while updating the command with values from the server response for a Mutation Command";
    public const string Mutation_Validation_ErrorMsg = "An error occurred during the validation of Mutation Command";
    public const string Mutation_LocalStorage_ErrorMsg = "An error occurred while writing to locastorage for a Mutation Command";
    public const string Mutation_LocalStorage_JSON_ErrorMsg = "An error occurred while parsing, serializing or de-serializing a Mutation Command for saving to local storage";

    //Specific Http Errors
    public const string InvalidRefreshUrlParametersMsg = "The HTTP refresh endpoint is invalid, following URL parameters were not replaced: ";
    public const string InvalidRefreshUrlMsg = "The HTTP refresh endpoint is invalid";
    public const string InvalidRefreshUrlCreationEnumValueMsg = "Unexpected enum value during creation of HTTP refresh endpoint";
    public const string ErrorUpdatingCommandFromServerResponseMsg = "An error occurred updating the command with the server response of type";
}



