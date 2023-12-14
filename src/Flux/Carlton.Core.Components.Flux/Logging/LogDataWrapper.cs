
namespace Carlton.Core.Components.Flux;

public static partial class LogDataWrapper
{
    [LoggerMessage(
        EventId = LogEvents.DataWrapper_OnInitialized_Started,
        Level = LogLevel.Information,
        Message = "DataWrapper for ViewModel of type {Type} OnInitializedStarted")]
    public static partial void DataWrapperOnInitializedStarted(this ILogger logger, string type);

    [LoggerMessage(
        EventId = LogEvents.DataWrapper_OnInitialized_Completed,
        Level = LogLevel.Information,
        Message = "DataWrapper for ViewModel of type {Type} OnInitializedCompleted")]
    public static partial void DataWrapperOnInitializedCompleted(this ILogger logger, string type);

    [LoggerMessage(
       EventId = LogEvents.DataWrapper_Event_Raise_Started,
       Level = LogLevel.Information,
       Message = "DataWrapper for ViewModel of type {Type} started raising Event {ReceivedEvent}")]
    public static partial void DataWrapperEventRaiseStarted(this ILogger logger, string type, object receivedEvent);

    [LoggerMessage(
      EventId = LogEvents.DataWrapper_Event_Raise_Completed,
      Level = LogLevel.Information,
      Message = "DataWrapper for ViewModel of type {Type} completed raising Event {ReceivedEvent}")]
    public static partial void DataWrapperEventRaiseCompleted(this ILogger logger, string type, object receivedEvent);

    [LoggerMessage(
     EventId = LogEvents.DataWrapper_Event_Listening,
     Level = LogLevel.Debug,
     Message = "DataWrapper for ViewModel of type {Type} is listening for {ObservedEvents}")]
    public static partial void DataWrapperListeningEvents(this ILogger logger, string type, string observedEvents);

    [LoggerMessage(
      EventId = LogEvents.DataWrapper_Event_Received_Started,
      Level = LogLevel.Information,
      Message = "DataWrapper for ViewModel of type {Type} Started Receiving Event {ReceivedEvent}")]
    public static partial void DataWrapperEventReceivedStarted(this ILogger logger, string type, object receivedEvent);

    [LoggerMessage(
        EventId = LogEvents.DataWrapper_Event_Received_Completed,
        Level = LogLevel.Information,
        Message = "DataWrapper for ViewModel of {Type} Completed Receiving {ReceivedEvent}, ViewModel was refreshed")]
    public static partial void DataWrapperEventReceivedCompleted(this ILogger logger, string type, object receivedEvent);


    [LoggerMessage(
      EventId = LogEvents.DataWrapper_Event_Skipped,
      Level = LogLevel.Debug,
      Message = "DataWrapper for ViewModel of type {Type} skipping raised event {ReceivedEvent}")]
    public static partial void DataWrapperEventSkipped(this ILogger logger, string type, object receivedEvent);

    [LoggerMessage(
       EventId = LogEvents.DataWrapper_Error,
       Level = LogLevel.Error,
       Message = "DataWrapper for ViewModel of type {Type} threw an unhandled exception during rendering")]
    public static partial void DataWrapperUnhandledException(this ILogger logger, string type, Exception exception);
}