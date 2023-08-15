namespace Carlton.Core.Components.Flux;

public static partial class Log
{
    public const string ViewModelRequestScopeMessage = "Initiating ViewModelQuery {ViewModelType} : {QueryID}";

    [LoggerMessage(
        EventId = LogEvents.ViewModel_Started,
        Level = LogLevel.Information,
        Message = "Started ViewModelQuery {ViewModelType}")]
    public static partial void ViewModelStarted(ILogger logger, string viewModelType);

    [LoggerMessage(
       EventId = LogEvents.ViewModel_JsInterop_Started,
       Level = LogLevel.Information,
       Message = "Started JSInterop Refresh for ViewModel {ViewModelType}")]
    public static partial void ViewModelJsInteropRefreshStarted(ILogger logger, string viewModelType);

    [LoggerMessage(
        EventId = LogEvents.ViewModel_JsInterop_Completed,
        Level = LogLevel.Information,
        Message = "Completed JSInterop Refresh for ViewModel {ViewModelType}")]
    public static partial void ViewModelJsInteropRefreshCompleted(ILogger logger, string viewModelType);

    [LoggerMessage(
       EventId = LogEvents.ViewModel_JsInterop_Skipped,
       Level = LogLevel.Information,
       Message = "Skipping JSInterop Refresh for ViewModel {viewModelType}")]
    public static partial void ViewModelJsInteropRefreshSkipped(ILogger logger, string viewModelType);

    [LoggerMessage(
        EventId = LogEvents.ViewModel_JsInterop_Error,
        Level = LogLevel.Error,
        Message = "An error occurred during JSInterop Refresh for ViewModel {ViewModelType}")]
    public static partial void ViewModelJsInteropRefreshError(ILogger logger, Exception ex, string viewModelType);

    [LoggerMessage(
       EventId = LogEvents.ViewModel_HttpRefresh_Started,
       Level = LogLevel.Information,
       Message = "Started Http Refresh for ViewModel {ViewModelType}")]
    public static partial void ViewModelHttpRefreshStarted(ILogger logger, string viewModelType);

    [LoggerMessage(
        EventId = LogEvents.ViewModel_HttpRefresh_Completed,
        Level = LogLevel.Information,
        Message = "Completed Http Refresh for ViewModel {ViewModelType}")]
    public static partial void ViewModelHttpRefreshCompleted(ILogger logger, string viewModelType);

    [LoggerMessage(
       EventId = LogEvents.ViewModel_HttpRefresh_Skipped,
       Level = LogLevel.Information,
       Message = "Skipping Http Refresh for ViewModel {ViewModelType}")]
    public static partial void ViewModelHttpRefreshSkipped(ILogger logger, string viewModelType);

    [LoggerMessage(
        EventId = LogEvents.ViewModel_HTTP_Error,
        Level = LogLevel.Error,
        Message = "An error occurred during Http Refresh for ViewModel {ViewModelType}")]
    public static partial void ViewModelHttpRefreshError(ILogger logger, Exception ex, string viewModelType);


    [LoggerMessage(
        EventId = LogEvents.ViewModel_Mapping_Started,
        Level = LogLevel.Information,
        Message = "Starting Mapping ViewModel {ViewModelType}")]
    public static partial void ViewModelMappingStarted(ILogger logger, string viewModelType);

    [LoggerMessage(
       EventId = LogEvents.ViewModel_Mapping_Completed,
       Level = LogLevel.Information,
       Message = "Completed Mapping ViewModel {ViewModelType}")]
    public static partial void ViewModelMappingCompleted(ILogger logger, string viewModelType);

    [LoggerMessage(
        EventId = LogEvents.ViewModel_Completed,
        Level = LogLevel.Information,
        Message = "Completed ViewModelQuery {viewModelType}")]
    public static partial void ViewModelCompleted(ILogger logger, string viewModelType);

    [LoggerMessage(
       EventId = LogEvents.ViewModel_Validation_Started,
       Level = LogLevel.Information,
       Message = "Started Validating ViewModel {ViewModelType}")]
    public static partial void ViewModelValidationStarted(ILogger logger, string viewModelType);

    [LoggerMessage(
        EventId = LogEvents.ViewModel_Validation_Completed,
        Level = LogLevel.Information,
        Message = "Completed Validating ViewModel {viewModelType}")]
    public static partial void ViewModelValidationCompleted(ILogger logger, string viewModelType);

    [LoggerMessage(
        EventId = LogEvents.ViewModel_JSON_Error,
        Level = LogLevel.Error,
        Message = "An error occurred parsing, serializing or de-serializing JSON for ViewModel {ViewModelType}")]
    public static partial void ViewModelJsonError(ILogger logger, Exception ex, string viewModelType);

    [LoggerMessage(
      EventId = LogEvents.ViewModel_Validation_Error,
      Level = LogLevel.Error,
      Message = "An error occurred validating ViewModel {ViewModelType}")]
    public static partial void ViewModelValidationError(ILogger logger, Exception ex, string viewModelType);

    [LoggerMessage(
         EventId = LogEvents.ViewModel_Unhandled_Error,
         Level = LogLevel.Error,
         Message = "An unhandled error occurred while processing a ViewModelQuery {ViewModelType}")]
    public static partial void ViewModelUnhandledError(ILogger logger, Exception ex, string viewModelType);
}