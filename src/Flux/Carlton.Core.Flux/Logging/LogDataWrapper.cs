namespace Carlton.Core.Flux.Logging;

public static partial class LogDataWrapper
{
    [LoggerMessage(
       EventId = LogEvents.DataWrapper_Error,
       Level = LogLevel.Error,
       Message = "DataWrapper for ViewModel of type {Type} threw an unhandled exception during rendering")]
    public static partial void DataWrapperUnhandledException(this ILogger logger, Exception ex, string type);
}