
namespace Carlton.Core.Components.Flux;

public static partial class Log
{
    [LoggerMessage(
        EventId = LogEvents.DataWrapper_OnInitialized_Started,
        Level = LogLevel.Information,
        Message = "DataWrapper {ComponentTypeName} OnInitializedStarted")]
    public static partial void DataWrapperOnInitializedStarted(ILogger logger, string componentTypeName);

    [LoggerMessage(
        EventId = LogEvents.DataWrapper_OnInitialized_Completed,
        Level = LogLevel.Information,
        Message = "DataWrapper {ComponentTypeName} OnInitializedCompleted")]
    public static partial void DataWrapperOnInitializedCompleted(ILogger logger, string componentTypeName);

    [LoggerMessage(
       EventId = LogEvents.DataWrapper_Event_Raise_Started,
       Level = LogLevel.Information,
       Message = "DataWrapper {ComponentTypeName} started raising Event {ReceivedEvent}")]
    public static partial void DataWrapperEventRaiseStarted(ILogger logger, string componentTypeName, object receivedEvent);

    [LoggerMessage(
      EventId = LogEvents.DataWrapper_Event_Raise_Completed,
      Level = LogLevel.Information,
      Message = "DataWrapper {ComponentTypeName} completed raising Event {ReceivedEvent}")]
    public static partial void DataWrapperEventRaiseCompleted(ILogger logger, string componentTypeName, object receivedEvent);

    [LoggerMessage(
     EventId = LogEvents.DataWrapper_Event_Listening,
     Level = LogLevel.Debug,
     Message = "DataWrapper {ComponentTypeName} is listening for {ObservedEvents}")]
    public static partial void DataWrapperListeningEvents(ILogger logger, string componentTypeName, string observedEvents);

    [LoggerMessage(
      EventId = LogEvents.DataWrapper_Event_Received_Started,
      Level = LogLevel.Information,
      Message = "DataWrapper {ComponentTypeName} Started Receiving Event {ReceivedEvent}")]
    public static partial void DataWrapperEventReceivedStarted(ILogger logger, string componentTypeName, object receivedEvent);

    [LoggerMessage(
        EventId = LogEvents.DataWrapper_Event_Received_Completed,
        Level = LogLevel.Information,
        Message = "DataWrapper {ComponentTypeName} Completed Receiving {ReceivedEvent}, ViewModel was refreshed")]
    public static partial void DataWrapperEventReceivedCompleted(ILogger logger, string componentTypeName, object receivedEvent);


    [LoggerMessage(
      EventId = LogEvents.DataWrapper_Event_Skipped,
      Level = LogLevel.Debug,
      Message = "DataWrapper {ComponentTypeName} skipping raised event {ReceivedEvent}")]
    public static partial void DataWrapperEventSkipped(ILogger logger, string componentTypeName, object receivedEvent);

    [LoggerMessage(
       EventId = LogEvents.DataWrapper_Error,
       Level = LogLevel.Error,
       Message = "DataWrapper {ComponentTypeName} threw an unhandled exception during rendering")]
    public static partial void DataWrapperUnhandledException(ILogger logger, string componentTypeName, Exception exception);
}