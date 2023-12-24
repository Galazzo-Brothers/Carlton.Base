namespace Carlton.Core.Lab.Logging;

public static class LabLogEvents
{
    //Lab ViewModel Events
    public const int ViewModel_JsInterop_Started = 3001;
    public const int ViewModel_JsInterop_Completed = 3002;
    
    //Lab ViewModel Errors
    public const int ViewModel_JsInterop_Error = 3100;
    public const string ViewModel_JSInterop_ErrorMsg = "An error occurred during the JSInterop for a ViewModel";
}
