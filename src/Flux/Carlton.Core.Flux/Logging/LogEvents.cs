namespace Carlton.Core.Flux.Logging;

public static class LogEvents
{
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
    public const int ViewModel_Started = 2000;
    public const int ViewModel_Completed = 2100;

    public const int ViewModel_HttpRefresh_Started = 2300;
    public const int ViewModel_HttpRefresh_Completed = 2310;
    public const int ViewModel_HttpRefresh_Skipped = 2311;

    public const int ViewModel_Mapping_Started = 2400;
    public const int ViewModel_Mapping_Completed = 2410;

    public const int ViewModel_Validation_Started = 2500;
    public const int ViewModel_Validation_Completed = 2510;

    public const int ViewModel_JsInterop_Started = 2600;
    public const int ViewModel_JsInterop_Completed = 2610;

    public const int ViewModel_Unhandled_Error = 2200;
    public const int ViewModel_HTTP_Error = 2230;
    public const int ViewModel_HTTP_URL_Error = 2231;
    public const int ViewModel_JSON_Error = 2232;
    public const int ViewModel_Mapping_Error = 2240;
    public const int ViewModel_Validation_Error = 2250;
    public const int ViewModel_JsInterop_Error = 2260;

    //Mutation Command Start Events
    public const int Mutation_Started = 3000;
    public const int Mutation_Completed = 3100;

    public const int Mutation_Validation_Started = 3300;
    public const int Mutation_Validation_Completed = 3310;

    public const int Mutation_HttpCall_Started = 3400;
    public const int Mutation_HttpCall_Completed = 3410;
    public const int Mutation_HttpCall_Skipped = 3411;

    public const int Mutation_Apply_Started = 3500;
    public const int Mutation_Apply_Completed = 3510;

    public const int Mutation_LocalStorage_Started = 3600;
    public const int Mutation_LocalStorage_Completed = 3610;

    //Mutation Errors
    public const int Mutation_Unhandled_Error = 3200;
    public const int Mutation_Validation_Error = 3230;
    public const int Mutation_HTTP_Error = 3240;
    public const int Mutation_HTTP_URL_Error = 3241;
    public const int Mutation_HTTP_JSON_Error = 3242;
    public const int Mutation_HTTP_Response_Update_Error = 3243;
    public const int Mutation_Apply_Error = 3250;
    public const int Mutation_LocalStorage_Unhandled_Error = 3260;
    public const int Mutation_LocalStorage_JSON_Error = 3261;

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



