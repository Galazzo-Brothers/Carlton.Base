
namespace Carlton.Core.Components.Flux;

public static partial class Log
{
    [LoggerMessage(
        EventId = LogEvents.DataWrapper_OnInitialized_Started,
        Level = LogLevel.Debug,
        Message = "DataWrapper {ComponentTypeName} OnInitializedStarted")]
    public static partial void DataWrapperOnInitializedStarted(ILogger logger, string componentTypeName);

    [LoggerMessage(
        EventId = LogEvents.DataWrapper_OnInitialized_Completed,
        Level = LogLevel.Debug,
        Message = "DataWrapper {ComponentTypeName} OnInitializedCompleted")]
    public static partial void DataWrapperOnInitializedCompleted(ILogger logger, string componentTypeName);

    [LoggerMessage(
        EventId = LogEvents.DataWrapper_Event_Received,
        Level = LogLevel.Information,
        Message = "DataWrapper {ComponentTypeName} Received Event {ReceivedEvent}, Listening for {ObservedEvents}")]
    public static partial void DataWrapperEventReceived(ILogger logger, string componentTypeName, object receivedEvent, string observedEvents);

    [LoggerMessage(
        EventId = LogEvents.DataWrapper_Event_Completed,
        Level = LogLevel.Information,
        Message = "DataWrapper {ComponentTypeName} Completed Receiving {ReceivedEvent}, ViewModel was refreshed: {RefreshOccurred}")]
    public static partial void DataWrapperEventCompleted(ILogger logger, string componentTypeName, object receivedEvent, bool refreshOccurred);

    [LoggerMessage(
       EventId = LogEvents.DataWrapper_Error,
       Level = LogLevel.Error,
       Message = "DataWrapper {ComponentTypeName} threw an unhandled exception during rendering")]
    public static partial void DataWrapperUnhandledException(ILogger logger, string componentTypeName, Exception exception);
}