namespace Carlton.Core.Flux.Logging;

public static partial class LogDataWrapper
{
    [LoggerMessage(
        EventId = LogEvents.DataWrapper_OnInitialized_Completed,
        Level = LogLevel.Information,
        Message = "DataWrapper for ViewModel of type {Type} initalized")]
    public static partial void DataWrapperOnInitializedCompleted(this ILogger logger, string type);

    [LoggerMessage(
        EventId = LogEvents.DataWrapper_Event_Listening,
        Level = LogLevel.Debug,
        Message = "DataWrapper for ViewModel of type {Type} is listening for events: {ObservableEvents}")]
    public static partial void DataWrapperListeningEvents(this ILogger logger, string type, string observableEvents);

    [LoggerMessage(
        EventId = LogEvents.DataWrapper_Event_Received_Completed,
        Level = LogLevel.Information,
        Message = "DataWrapper for ViewModel of type {Type} Completed receiving event {StateEvent}, ViewModel was refreshed")]
    public static partial void DataWrapperEventReceivedCompleted(this ILogger logger, string type, string stateEvent);

    [LoggerMessage(
        EventId = LogEvents.DataWrapper_Event_Skipped,
        Level = LogLevel.Debug,
        Message = "DataWrapper for ViewModel of type {Type} skipping event {StateEvent}")]
    public static partial void DataWrapperEventSkipped(this ILogger logger, string type, string stateEvent);

    [LoggerMessage(
       EventId = LogEvents.DataWrapper_Error,
       Level = LogLevel.Error,
       Message = "DataWrapper for ViewModel of type {Type} threw an unhandled exception during rendering")]
    public static partial void DataWrapperUnhandledException(this ILogger logger, Exception ex, string type);
}