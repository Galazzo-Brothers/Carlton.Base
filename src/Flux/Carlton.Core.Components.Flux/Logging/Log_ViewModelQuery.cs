namespace Carlton.Core.Components.Flux;

public static partial class Log
{
    public const string ViewModelRequestScopeMessage = "Initiating ViewModelQuery {Type} : {QueryID}";

    [LoggerMessage(
        EventId = LogEvents.ViewModel_Started,
        Level = LogLevel.Information,
        Message = "Started processing query for ViewModel of type {Type}.")]
    public static partial void ViewModelStarted(ILogger logger, string type);

    [LoggerMessage(
       EventId = LogEvents.ViewModel_JsInterop_Started,
       Level = LogLevel.Information,
       Message = "Started JSInterop refresh for ViewModel of type {Type}.")]
    public static partial void ViewModelJsInteropRefreshStarted(ILogger logger, string type);

    [LoggerMessage(
        EventId = LogEvents.ViewModel_JsInterop_Completed,
        Level = LogLevel.Information,
        Message = "Completed JSInterop refresh for ViewModel of type {Type}.")]
    public static partial void ViewModelJsInteropRefreshCompleted(ILogger logger, string type);

    [LoggerMessage(
       EventId = LogEvents.ViewModel_JsInterop_Skipped,
       Level = LogLevel.Information,
       Message = "Skipping JSInterop refresh for ViewModel of type {Type}.")]
    public static partial void ViewModelJsInteropRefreshSkipped(ILogger logger, string type);

    [LoggerMessage(
        EventId = LogEvents.ViewModel_JsInterop_Error,
        Level = LogLevel.Error,
        Message = "An error occurred during JSInterop refresh for ViewModel of type {Type}.")]
    public static partial void ViewModelJsInteropRefreshError(ILogger logger, Exception ex, string type);

    [LoggerMessage(
       EventId = LogEvents.ViewModel_HttpRefresh_Started,
       Level = LogLevel.Information,
       Message = "Started Http refresh for ViewModel of type {Type}.")]
    public static partial void ViewModelHttpRefreshStarted(ILogger logger, string type);

    [LoggerMessage(
        EventId = LogEvents.ViewModel_HttpRefresh_Completed,
        Level = LogLevel.Information,
        Message = "Completed Http refresh for ViewModel of type {Type}.")]
    public static partial void ViewModelHttpRefreshCompleted(ILogger logger, string type);

    [LoggerMessage(
       EventId = LogEvents.ViewModel_HttpRefresh_Skipped,
       Level = LogLevel.Information,
       Message = "Skipping Http refresh for ViewModel of type {Type}.")]
    public static partial void ViewModelHttpRefreshSkipped(ILogger logger, string type);

    [LoggerMessage(
       EventId = LogEvents.ViewModel_HTTP_URL_Error,
       Level = LogLevel.Error,
       Message = "An error occurred during the creation of an Http Refresh URL for ViewModel of type {Type}.")]
    public static partial void ViewModelHttpUrlError(ILogger logger, Exception ex, string type);

    [LoggerMessage(
        EventId = LogEvents.ViewModel_HTTP_Error,
        Level = LogLevel.Error,
        Message = "An error occurred during the Http refresh for ViewModel of type {Type}.")]
    public static partial void ViewModelHttpRefreshError(ILogger logger, Exception ex, string type);

    [LoggerMessage(
        EventId = LogEvents.ViewModel_Mapping_Started,
        Level = LogLevel.Information,
        Message = "Starting mapping ViewModel of type {Type}.")]
    public static partial void ViewModelMappingStarted(ILogger logger, string type);

    [LoggerMessage(
       EventId = LogEvents.ViewModel_Mapping_Completed,
       Level = LogLevel.Information,
       Message = "Completed mapping ViewModel of type {Type}.")]
    public static partial void ViewModelMappingCompleted(ILogger logger, string type);

    [LoggerMessage(
        EventId = LogEvents.ViewModel_Completed,
        Level = LogLevel.Information,
        Message = "Completed processing query for ViewModel of type {Type}.")]
    public static partial void ViewModelCompleted(ILogger logger, string type);

    [LoggerMessage(
       EventId = LogEvents.ViewModel_Validation_Started,
       Level = LogLevel.Information,
       Message = "Started validating ViewModel of type {Type}.")]
    public static partial void ViewModelValidationStarted(ILogger logger, string type);

    [LoggerMessage(
        EventId = LogEvents.ViewModel_Validation_Completed,
        Level = LogLevel.Information,
        Message = "Completed validating ViewModel of type {Type}.")]
    public static partial void ViewModelValidationCompleted(ILogger logger, string type);

    [LoggerMessage(
        EventId = LogEvents.ViewModel_JSON_Error,
        Level = LogLevel.Error,
        Message = "An error occurred parsing, serializing or de-serializing JSON for ViewModel of type {Type}.")]
    public static partial void ViewModelJsonError(ILogger logger, Exception ex, string type);

    [LoggerMessage(
      EventId = LogEvents.ViewModel_Validation_Error,
      Level = LogLevel.Error,
      Message = "An error occurred validating ViewModel of type {Type}.")]
    public static partial void ViewModelValidationError(ILogger logger, Exception ex, string type);

    [LoggerMessage(
         EventId = LogEvents.ViewModel_Unhandled_Error,
         Level = LogLevel.Error,
         Message = "An unhandled error occurred while processing a query for  ViewModel of type {Type}.")]
    public static partial void ViewModelUnhandledError(ILogger logger, Exception ex, string type);
}