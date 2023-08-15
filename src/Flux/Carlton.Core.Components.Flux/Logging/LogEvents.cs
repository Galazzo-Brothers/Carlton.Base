namespace Carlton.Core.Components.Flux;

public static class LogEvents
{
    //Debug Lifecycle Methods
    public const int DataWrapper_IsLoadingChanged = 10;
    public const int DataWrapper_OnInitialized_Started = 11;
    public const int DataWrapper_OnInitialized_Completed = 12;
    public const int DataWrapper_OnInitialized_Async_Started = 13;
    public const int DataWrapper_OnInitialized_Async_Completed = 14;
    public const int DataWrapper_OnParametersSet_Started = 15;
    public const int DataWrapper_OnParametersSet_Completed = 16;
    public const int DataWrapper_OnParametersSet_Async_Started = 17;
    public const int DataWrapper_OnParametersSet_Async_Completed = 18;
    public const int DataWrapper_OnAfterRender_Started = 19;
    public const int DataWrapper_OnAfterRender_Completed = 20;
    public const int DataWrapper_OnAfterRender_Async_Started = 21;
    public const int DataWrapper_OnAfterRender_Async_Completed = 22;

    public const int DataComponent_OnInitialized_Started = 23;
    public const int DataComponent_OnInitialized_Completed = 24;
    public const int DataComponent_OnInitialized_Async_Started = 25;
    public const int DataComponent_OnInitialized_Async_Completed = 26;
    public const int DataComponent_OnParametersSet_Started = 27;
    public const int DataComponent_OnParametersSet_Completed = 28;
    public const int DataComponent_OnParametersSet_Async_Started = 29;
    public const int DataComponent_OnParametersSet_Async_Completed = 30;
    public const int DataComponent_OnAfterRender_Started = 31;
    public const int DataComponent_OnAfterRender_Completed = 32;
    public const int DataComponent_OnAfterRender_Async_Started = 33;
    public const int DataComponent_OnAfterRender_Async_Completed = 34;


    //DataComponent
    public const int DataWrapper_Event_Received = 100;
    public const int DataWrapper_Event_Completed = 101;
    public const int DataWrapper_Parameters_Setting = 102;
    public const int DataWrapper_Parameters_Set = 103;

    //DataComponent Errors
    public const int DataWrapper_Error = 110;

    //ViewModel Queries
    public const int ViewModel_Started = 1000;
    public const int ViewModel_JsInterop_Started = 1002;
    public const int ViewModel_JsInterop_Completed = 1004;
    public const int ViewModel_JsInterop_Skipped = 1008;
    public const int ViewModel_HttpRefresh_Started = 1010;
    public const int ViewModel_HttpRefresh_Completed = 1012;
    public const int ViewModel_HttpRefresh_Skipped = 1014;
    public const int ViewModel_Mapping_Started = 1016;
    public const int ViewModel_Mapping_Completed = 1018;
    public const int ViewModel_Validation_Started = 1020;
    public const int ViewModel_Validation_Completed = 1022;
    public const int ViewModel_Completed = 1024;

    //ViewModel Queries Errors
    public const int ViewModel_HttpRefresh_Http_Error = 1102;
    public const int ViewModel_JsInterop_Error = 1104;
    public const int ViewModel_HttpRefresh_Error = 1106;
    public const int ViewModel_JSON_Error = 1108;
    public const int ViewModel_Validation_Error = 1110;
    public const int ViewModel_Unhandled_Error = 1200;

    //ViewModel Query Error Messages
    public const string ViewModel_JSON_ErrorMsg = "An error occurred while parsing, serializing or de-serializing JSON for a ViewModel";
    public const string ViewModel_HTTP_ErrorMsg = "An error occurred while communicating with the remote server endpoint for a ViewModel";
    public const string ViewModel_JSInterop_ErrorMsg = "An error occurred during the JSInterop for a ViewModel";
    public const string ViewModel_Validation_ErrorMsg = "An error occurred during the validation of ViewModel";

    //Command Requests
    public const int Command_Started = 1001;
    public const int Command_Handling_Started = 1003;
    public const int Command_Validation_Started = 1005;
    public const int Command_Validation_Completed = 1007;
    public const int Command_HttpCall_Started = 1009;
    public const int Command_HttpCall_Completed = 1011;
    public const int Command_HttpCall_Skipped = 1013;
    public const int Command_Processing_Started = 1015;
    public const int Command_Processing_Completed = 1017;
    public const int Command_Handling_Completed = 1019;
    public const int Command_Completed = 1021;

    //Command Request Errors
    public const int Command_Validation_Error = 1101;
    public const int Command_HttpCall_Error = 1201;
    public const int Command_Processing_Error = 1301;

}
