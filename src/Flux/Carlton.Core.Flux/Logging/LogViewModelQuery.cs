namespace Carlton.Core.Flux.Logging;

public static partial class LogViewModelQuery
{
    [LoggerMessage(
        EventId = LogEvents.ViewModel_JsInterop_Completed,
        Level = LogLevel.Debug,
        Message = "Completed JSInterop refresh for ViewModel of type {Type}")]
    public static partial void ViewModelJsInteropRefreshCompleted(this ILogger logger, string type);

    [LoggerMessage(
        EventId = LogEvents.ViewModel_JsInterop_Error,
        Level = LogLevel.Error,
        Message = "An error occurred during JSInterop refresh for ViewModel of type {Type}")]
    public static partial void ViewModelJsInteropRefreshErrored
        (this ILogger logger, Exception ex, string type);

    [LoggerMessage(
        EventId = LogEvents.ViewModel_HttpRefresh_Completed,
        Level = LogLevel.Information,
        Message = "Completed Http refresh for ViewModel of type {Type}")]
    public static partial void ViewModelHttpRefreshCompleted(this ILogger logger, string type);

    [LoggerMessage(
        EventId = LogEvents.ViewModel_HttpRefresh_Skipped,
        Level = LogLevel.Debug,
        Message = "Skipping Http refresh for ViewModel of type {Type}")]
    public static partial void ViewModelHttpRefreshSkipped(this ILogger logger, string type);

    [LoggerMessage(
        EventId = LogEvents.ViewModel_HTTP_URL_Error,
        Level = LogLevel.Error,
        Message = "An error occurred during the creation of an Http Refresh URL for ViewModel of type {Type}")]
    public static partial void ViewModelHttpUrlError(this ILogger logger, Exception ex, string type);

    [LoggerMessage(
        EventId = LogEvents.ViewModel_HTTP_Request_Error,
        Level = LogLevel.Error,
        Message = "An error occurred during the Http request for ViewModel of type {Type}")]
    public static partial void ViewModelHttpRequestError(this ILogger logger, Exception ex, string type);

    [LoggerMessage(
        EventId = LogEvents.ViewModel_Mapping_Completed,
        Level = LogLevel.Debug,
        Message = "Completed mapping ViewModel of type {Type}")]
    public static partial void ViewModelMappingCompleted(this ILogger logger, string type);

    [LoggerMessage(
        EventId = LogEvents.ViewModel_Completed,
        Level = LogLevel.Information,
        Message = "Completed processing query for ViewModel of type {Type}")]
    public static partial void ViewModelCompleted(this ILogger logger, string type);

    [LoggerMessage(
        EventId = LogEvents.ViewModel_Validation_Completed,
        Level = LogLevel.Debug,
        Message = "Completed validating ViewModel of type {Type}")]
    public static partial void ViewModelValidationCompleted(this ILogger logger, string type);

    [LoggerMessage(
        EventId = LogEvents.ViewModel_HTTP_Response_JSON_Error,
        Level = LogLevel.Error,
        Message = "An error occurred parsing, serializing or de-serializing JSON for ViewModel of type {Type}")]
    public static partial void ViewModelHttpResponseJsonError(this ILogger logger, Exception ex, string type);

    [LoggerMessage(
        EventId = LogEvents.ViewModel_Mapping_Error,
        Level = LogLevel.Error,
        Message = "An error occurred mapping ViewModel of type {Type}")]
    public static partial void ViewModelMappingError(this ILogger logger, Exception ex, string type);

    [LoggerMessage(
        EventId = LogEvents.ViewModel_Validation_Error,
        Level = LogLevel.Error,
        Message = "An error occurred validating ViewModel of type {Type}")]
    public static partial void ViewModelValidationError(this ILogger logger, Exception ex, string type);

    [LoggerMessage(
        EventId = LogEvents.ViewModel_Unhandled_Error,
        Level = LogLevel.Error,
        Message = "An unhandled error occurred while processing a query for  ViewModel of type {Type}")]
    public static partial void ViewModelUnhandledError(this ILogger logger, Exception ex, string type);
}