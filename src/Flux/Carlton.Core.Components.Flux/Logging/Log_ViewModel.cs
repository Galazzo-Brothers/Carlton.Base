
namespace Carlton.Core.Components.Flux;

public static partial class Log
{
    public const string ViewModelRequestScopeMessage = "Initiating ViewModel Request {ViewModel} : {RequestID}";

    [LoggerMessage(
        EventId = LogEvents.ViewModelRequest_Started,
        Level = LogLevel.Information,
        Message = "Starting ViewModel Request {RequestName} : {RequestID}")]
    public static partial void ViewModelRequestStarted(ILogger logger, string requestName, Guid requestID);

    [LoggerMessage(
        EventId = LogEvents.ViewModelRequest_Handling_Started,
        Level = LogLevel.Information,
        Message = "Started Handling {RequestName} : {Request}")]
    public static partial void ViewModelRequestHandlingStarted(ILogger logger, string requestName, object request);

    [LoggerMessage(
       EventId = LogEvents.ViewModelRequest_JsInterop_Started,
       Level = LogLevel.Information,
       Message = "Started JS Interop Refresh {RequestName} : {Request}")]
    public static partial void ViewModelRequestJsInteropRefreshStarting(ILogger logger, string requestName, object request);

    [LoggerMessage(
        EventId = LogEvents.ViewModelRequest_JsInterop_Completed,
        Level = LogLevel.Information,
        Message = "Completed JS Interop Refresh {RequestName} : {Request}")]
    public static partial void ViewModelRequestJsInteropRefreshCompleted(ILogger logger, string requestName, object request);

    [LoggerMessage(
       EventId = LogEvents.ViewModelRequest_JsInterop_Skipping,
       Level = LogLevel.Information,
       Message = "Skipping Js Interop Refresh {RequestName} : {Request}")]
    public static partial void ViewModelRequestSkippingJsInteropRefresh(ILogger logger, string requestName, object request);

    [LoggerMessage(
        EventId = LogEvents.ViewModelRequest_JsInterop_Error,
        Level = LogLevel.Error,
        Message = "An error occurred during Js Interop Refresh {RequestName} : {Request}")]
    public static partial void ViewModelRequestJsInteropRefreshError(ILogger logger, Exception ex, string requestName, object request);

    [LoggerMessage(
       EventId = LogEvents.ViewModelRequest_HttpRefresh_Started,
       Level = LogLevel.Information,
       Message = "Started Http Refresh {RequestName} : {Request}")]
    public static partial void ViewModelRequestHttpRefreshStarting(ILogger logger, string requestName, object request);

    [LoggerMessage(
        EventId = LogEvents.ViewModelRequest_HttpRefresh_Completed,
        Level = LogLevel.Information,
        Message = "Completed Http Refresh {RequestName} : {Request}")]
    public static partial void ViewModelRequestHttpRefreshCompleted(ILogger logger, string requestName, object request);

    [LoggerMessage(
       EventId = LogEvents.ViewModelRequest_HttpRefresh_Skipping,
       Level = LogLevel.Information,
       Message = "Skipping Http Refresh {RequestName} : {Request}")]
    public static partial void ViewModelRequestSkippingHttpRefresh(ILogger logger, string requestName, object request);

    [LoggerMessage(
        EventId = LogEvents.ViewModelRequest_HttpRefresh_Http_Error,
        Level = LogLevel.Error,
        Message = "An error occurred during Http Refresh {RequestName} : {Request}")]
    public static partial void ViewModelRequestHttpRefreshError(ILogger logger, Exception ex, string requestName, object request);

    [LoggerMessage(
        EventId = LogEvents.ViewModelRequest_HttpRefresh_Mapping_Error,
        Level = LogLevel.Error,
        Message = "An error occurred during Http Refresh Mapping {RequestName} : {Request}")]
    public static partial void ViewModelRequestHttpRefreshMappingError(ILogger logger, Exception ex, string requestName, object request);

    [LoggerMessage(
        EventId = LogEvents.ViewModelRequest_HttpRefresh_RouteConstruction_Error,
        Level = LogLevel.Error,
        Message = "An error occurred during Http Refresh Mapping {RequestName} : {Request}")]
    public static partial void ViewModelRequestHttpRefreshRouteConstructionError(ILogger logger, Exception ex, string requestName, object request);

    [LoggerMessage(
        EventId = LogEvents.ViewModelRequest_RetrievingViewModel_Started,
        Level = LogLevel.Information,
        Message = "Starting Retrieving ViewModel {ViewModelName}")]
    public static partial void ViewModelRequestRetrievingViewModelStarted(ILogger logger, string viewModelName);

    [LoggerMessage(
       EventId = LogEvents.ViewModelRequest_RetrievingViewModel_Completed,
       Level = LogLevel.Information,
       Message = "Completed Retrieving ViewModel {ViewModelName}: {ViewModel}")]
    public static partial void ViewModelRequestRetrievingViewModelCompleted(ILogger logger, string viewModelName, object viewModel);

    [LoggerMessage(
        EventId = LogEvents.ViewModelRequest_RetrievingViewModel_Error,
        Level = LogLevel.Error,
        Message = "Error Retrieving ViewModel {ViewModelName}")]
    public static partial void ViewModelRequestRetrievingViewModelError(ILogger logger, Exception ex, string viewModelName);

    [LoggerMessage(
        EventId = LogEvents.ViewModelRequest_Handling_Completed,
        Level = LogLevel.Information,
        Message = "Handled {RequestName} : {Request}")]
    public static partial void ViewModelRequestHandlingCompleted(ILogger logger, string requestName, object request);

    [LoggerMessage(
        EventId = LogEvents.ViewModelRequest_Completed,
        Level = LogLevel.Information,
        Message = "Completed ViewModel Request {RequestName}, : {Request}")]
    public static partial void ViewModelRequestCompleted(ILogger logger, string requestName, object request);
}