namespace Carlton.Core.Flux.Logging;

public static partial class LogViewModelQuery
{
    [LoggerMessage(
     EventId = LogEvents.ViewModel_Started,
     Level = LogLevel.Information,
     Message = "Started processing query for ViewModel of type {Type}.")]
    public static partial void ViewModelStarted(this ILogger logger, string type);

    [LoggerMessage(
       EventId = LogEvents.ViewModel_JsInterop_Started,
       Level = LogLevel.Debug,
       Message = "Started JSInterop refresh for ViewModel of type {Type}.")]
    public static partial void ViewModelJsInteropRefreshStarted(this ILogger logger, string type);

    [LoggerMessage(
        EventId = LogEvents.ViewModel_JsInterop_Completed,
        Level = LogLevel.Debug,
        Message = "Completed JSInterop refresh for ViewModel of type {Type}.")]
    public static partial void ViewModelJsInteropRefreshCompleted(this ILogger logger, string type);

    [LoggerMessage(
        EventId = LogEvents.ViewModel_JsInterop_Error,
        Level = LogLevel.Error,
        Message = "An error occurred during JSInterop refresh for ViewModel of type {Type}.")]
    public static partial void ViewModelJsInteropRefreshError(this ILogger logger, Exception ex, string type);

    [LoggerMessage(
       EventId = LogEvents.ViewModel_HttpRefresh_Started,
       Level = LogLevel.Information,
       Message = "Started Http refresh for ViewModel of type {Type}.")]
    public static partial void ViewModelHttpRefreshStarted(this ILogger logger, string type);

    [LoggerMessage(
        EventId = LogEvents.ViewModel_HttpRefresh_Completed,
        Level = LogLevel.Information,
        Message = "Completed Http refresh for ViewModel of type {Type}.")]
    public static partial void ViewModelHttpRefreshCompleted(this ILogger logger, string type);

    [LoggerMessage(
       EventId = LogEvents.ViewModel_HttpRefresh_Skipped,
       Level = LogLevel.Debug,
       Message = "Skipping Http refresh for ViewModel of type {Type}.")]
    public static partial void ViewModelHttpRefreshSkipped(this ILogger logger, string type);

    [LoggerMessage(
       EventId = LogEvents.ViewModel_HTTP_URL_Error,
       Level = LogLevel.Error,
       Message = "An error occurred during the creation of an Http Refresh URL for ViewModel of type {Type}.")]
    public static partial void ViewModelHttpUrlError(this ILogger logger, Exception ex, string type);

    [LoggerMessage(
        EventId = LogEvents.ViewModel_HTTP_Error,
        Level = LogLevel.Error,
        Message = "An error occurred during the Http refresh for ViewModel of type {Type}.")]
    public static partial void ViewModelHttpRefreshError(this ILogger logger, Exception ex, string type);

    [LoggerMessage(
        EventId = LogEvents.ViewModel_Mapping_Started,
        Level = LogLevel.Debug,
        Message = "Starting mapping ViewModel of type {Type}.")]
    public static partial void ViewModelMappingStarted(this ILogger logger, string type);

    [LoggerMessage(
       EventId = LogEvents.ViewModel_Mapping_Completed,
       Level = LogLevel.Debug,
       Message = "Completed mapping ViewModel of type {Type}.")]
    public static partial void ViewModelMappingCompleted(this ILogger logger, string type);

    [LoggerMessage(
        EventId = LogEvents.ViewModel_Completed,
        Level = LogLevel.Information,
        Message = "Completed processing query for ViewModel of type {Type}.")]
    public static partial void ViewModelCompleted(this ILogger logger, string type);

    [LoggerMessage(
       EventId = LogEvents.ViewModel_Validation_Started,
       Level = LogLevel.Debug,
       Message = "Started validating ViewModel of type {Type}.")]
    public static partial void ViewModelValidationStarted(this ILogger logger, string type);

    [LoggerMessage(
        EventId = LogEvents.ViewModel_Validation_Completed,
        Level = LogLevel.Debug,
        Message = "Completed validating ViewModel of type {Type}.")]
    public static partial void ViewModelValidationCompleted(this ILogger logger, string type);

    [LoggerMessage(
        EventId = LogEvents.ViewModel_JSON_Error,
        Level = LogLevel.Error,
        Message = "An error occurred parsing, serializing or de-serializing JSON for ViewModel of type {Type}.")]
    public static partial void ViewModelJsonError(this ILogger logger, Exception ex, string type);

    [LoggerMessage(
    EventId = LogEvents.ViewModel_Mapping_Error,
    Level = LogLevel.Error,
    Message = "An error occurred mapping ViewModel of type {Type}.")]
    public static partial void ViewModelMappingError(this ILogger logger, Exception ex, string type);

    [LoggerMessage(
      EventId = LogEvents.ViewModel_Validation_Error,
      Level = LogLevel.Error,
      Message = "An error occurred validating ViewModel of type {Type}.")]
    public static partial void ViewModelValidationError(this ILogger logger, Exception ex, string type);

    [LoggerMessage(
         EventId = LogEvents.ViewModel_Unhandled_Error,
         Level = LogLevel.Error,
         Message = "An unhandled error occurred while processing a query for  ViewModel of type {Type}.")]
    public static partial void ViewModelUnhandledError(this ILogger logger, Exception ex, string type);
}