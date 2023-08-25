namespace Carlton.Core.Components.Flux;

public static class LogEvents
{
    //DataComponent
    public const int DataWrapper_OnInitialized_Started = 101;
    public const int DataWrapper_OnInitialized_Completed = 102;
    public const int DataWrapper_Event_Received = 103;
    public const int DataWrapper_Event_Completed = 104;

    //DataComponent Errors
    public const int DataWrapper_Error = 110;

    //ViewModel Queries
    public const int ViewModel_Started = 1000;
    public const int ViewModel_JsInterop_Started = 1001;
    public const int ViewModel_JsInterop_Completed = 1002;
    public const int ViewModel_JsInterop_Skipped = 1003;
    public const int ViewModel_HttpRefresh_Started = 1004;
    public const int ViewModel_HttpRefresh_Completed = 1005;
    public const int ViewModel_HttpRefresh_Skipped = 1006;
    public const int ViewModel_Mapping_Started = 1007;
    public const int ViewModel_Mapping_Completed = 1008;
    public const int ViewModel_Validation_Started = 1009;
    public const int ViewModel_Validation_Completed = 1010;
    public const int ViewModel_Completed = 1100;

    //ViewModel Queries Errors
    public const int ViewModel_HttpRefresh_Http_Error = 1101;
    public const int ViewModel_JsInterop_Error = 1102;
    public const int ViewModel_HTTP_Error = 1103;
    public const int ViewModel_JSON_Error = 1104;
    public const int ViewModel_Validation_Error = 1105;
    public const int ViewModel_Unhandled_Error = 1050;

    //ViewModel Query Error Messages
    public const string ViewModel_JSON_ErrorMsg = "An error occurred while parsing, serializing or de-serializing JSON for a ViewModel";
    public const string ViewModel_HTTP_ErrorMsg = "An error occurred while communicating with the remote server endpoint for a ViewModel";
    public const string ViewModel_JSInterop_ErrorMsg = "An error occurred during the JSInterop for a ViewModel";
    public const string ViewModel_Validation_ErrorMsg = "An error occurred during the validation of ViewModel";
    public const string ViewModel_Unhandled_ErrorMsg = $"An unhandled exception occurred during a ViewModelQuery";

    //MutationCommand Requests
    public const int Mutation_Started = 2000;
    public const int Mutation_Validation_Started = 2001;
    public const int Mutation_Validation_Completed = 2002;
    public const int Mutation_JsInterop_Started = 2003;
    public const int Mutation_JsInterop_Completed = 2004;
    public const int Mutation_JsInterop_Skipped = 2005;
    public const int Mutation_HttpCall_Started = 2006;
    public const int Mutation_HttpCall_Completed = 2007;
    public const int Mutation_HttpCall_Skipped = 2008;
    public const int Mutation_Apply_Started = 2009;
    public const int Mutation_Apply_Completed = 2010;
    public const int Mutation_Completed = 2100;

    //Mutation Errors
    public const int Mutation_Validation_Error = 2101;
    public const int Mutation_JSInterop_Error = 2102;
    public const int Mutation_HTTP_Error = 2103;
    public const int Mutation_Apply_Error = 2104;
    public const int Mutation_JSON_Error = 2105;
    public const int Mutation_Unhandled_Error = 2050;

    //Mutation Error Messages
    public const string Mutation_JSON_ErrorMsg = "An error occurred while parsing, serializing or de-serializing JSON for a Mutation Command";
    public const string Mutation_JSInterop_ErrorMsg = "An error occurred during the JSInterop for a Mutation Command";
    public const string Mutation_HTTP_ErrorMsg = "An error occurred while communicating with the remote server endpoint for a Mutation Command";
    public const string Mutation_Validation_ErrorMsg = "An error occurred during the validation of Mutation Command";

}
