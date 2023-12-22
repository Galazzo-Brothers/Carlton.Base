using Microsoft.Extensions.Logging;

namespace Carlton.Core.Components.Lab.Logging;

public static partial class LabLogViewModelQuery
{
    [LoggerMessage(
       EventId = LabLogEvents.ViewModel_JsInterop_Started,
       Level = LogLevel.Debug,
       Message = "Started JSInterop refresh for ViewModel of type {Type}.")]
    public static partial void ViewModelJsInteropRefreshStarted(this ILogger logger, string type);

    [LoggerMessage(
        EventId = LabLogEvents.ViewModel_JsInterop_Completed,
        Level = LogLevel.Debug,
        Message = "Completed JSInterop refresh for ViewModel of type {Type}.")]
    public static partial void ViewModelJsInteropRefreshCompleted(this ILogger logger, string type);

    [LoggerMessage(
        EventId = LabLogEvents.ViewModel_JsInterop_Error,
        Level = LogLevel.Error,
        Message = "An error occurred during JSInterop refresh for ViewModel of type {Type}.")]
    public static partial void ViewModelJsInteropRefreshError(this ILogger logger, Exception ex, string type);
}
