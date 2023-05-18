namespace Carlton.Base.State;

public static partial class Log
{
    [LoggerMessage(
        EventId = LogEvents.DataWrapper_IsLoadingChanged,
        Level = LogLevel.Debug,
        Message = "DataWrapper {ComponentTypeName} IsLoading: {IsLoading}")]
    public static partial void DataWrapperIsLoadingChanged(ILogger logger, string componentTypeName, bool isLoading);

    [LoggerMessage(
        EventId = LogEvents.DataWrapper_Event_Received,
        Level = LogLevel.Information,
        Message = "DataWrapper {ComponentTypeName} Received Event {ReceivedEvent}, Listening for {ObservedEvents}")]
    public static partial void DataWrapperEventReceived(ILogger logger, string componentTypeName, object receivedEvent, IEnumerable<object> observedEvents);

    [LoggerMessage(
        EventId = LogEvents.DataWrapper_Event_Completed,
        Level = LogLevel.Information,
        Message = "DataWrapper {ComponentTypeName} Completed Receiving {ReceivedEvent}, ViewModel was refreshed: {RefreshOccurred}")]
    public static partial void DataWrapperEventCompleted(ILogger logger, string componentTypeName, object receivedEvent, bool refreshOccurred);

    [LoggerMessage(
        EventId = LogEvents.DataWrapper_Parameters_Setting,
        Level = LogLevel.Debug,
        Message = "DataWrapper {ComponentTypeName} Setting ViewModel Parameter: {ViewModel}")]
    public static partial void DataWrapperParametersSetting(ILogger logger, string componentTypeName, object viewModel);

    [LoggerMessage(
        EventId = LogEvents.DataWrapper_Parameters_Set,
        Level = LogLevel.Debug,
        Message = "DataWrapper {ComponentTypeName} ViewModel {ViewModel} set")]
    public static partial void DataWrapperParametersSet(ILogger logger, string componentTypeName, object viewModel);

    [LoggerMessage(
        EventId = LogEvents.DataWrapper_Error,
        Level = LogLevel.Error,
        Message = "An error occurred in DataWrapper {ComponentTypeName}")]
    public static partial void DataWrapperError(ILogger logger, Exception ex, string componentTypeName);

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
        EventId = LogEvents.DataWrapper_OnInitialized_Async_Started,
        Level = LogLevel.Debug,
        Message = "DataWrapper {ComponentTypeName} OnInitializedAsyncStarted")]
    public static partial void DataWrapperOnInitializedAsyncStarted(ILogger logger, string componentTypeName);

    [LoggerMessage(
        EventId = LogEvents.DataWrapper_OnInitialized_Async_Completed,
        Level = LogLevel.Debug,
        Message = "DataWrapper {ComponentTypeName} OnInitializedAsyncCompleted")]
    public static partial void DataWrapperOnInitializedAsyncCompleted(ILogger logger, string componentTypeName);

    [LoggerMessage(
       EventId = LogEvents.DataWrapper_OnParametersSet_Started,
       Level = LogLevel.Debug,
       Message = "DataWrapper {ComponentTypeName} OnParametersSetStarted")]
    public static partial void DataWrapperOnParametersSetStarted(ILogger logger, string componentTypeName);

    [LoggerMessage(
        EventId = LogEvents.DataWrapper_OnParametersSet_Completed,
        Level = LogLevel.Debug,
        Message = "DataWrapper {ComponentTypeName} OnParametersSetCompleted")]
    public static partial void DataWrapperOnParametersSetCompleted(ILogger logger, string componentTypeName);

    [LoggerMessage(
     EventId = LogEvents.DataWrapper_OnParametersSet_Async_Started,
     Level = LogLevel.Debug,
     Message = "DataWrapper {ComponentTypeName} OnParametersSetAsyncStarted")]
    public static partial void DataWrapperOnParametersSetAsyncStarted(ILogger logger, string componentTypeName);

    [LoggerMessage(
        EventId = LogEvents.DataWrapper_OnParametersSet_Async_Completed,
        Level = LogLevel.Debug,
        Message = "DataWrapper {ComponentTypeName} OnParametersSetAsyncCompleted")]
    public static partial void DataWrapperOnParametersSetAsyncCompleted(ILogger logger, string componentTypeName);

    [LoggerMessage(
      EventId = LogEvents.DataWrapper_OnAfterRender_Started,
      Level = LogLevel.Debug,
      Message = "DataWrapper {ComponentTypeName} OnAfterRenderStarted")]
    public static partial void DataWrapperOnAfterRenderStarted(ILogger logger, string componentTypeName);

    [LoggerMessage(
        EventId = LogEvents.DataWrapper_OnAfterRender_Completed,
        Level = LogLevel.Debug,
        Message = "DataWrapper {ComponentTypeName} OnAfterRenderCompleted")]
    public static partial void DataWrapperOnAfterRenderCompleted(ILogger logger, string componentTypeName);

    [LoggerMessage(
     EventId = LogEvents.DataWrapper_OnAfterRender_Async_Started,
     Level = LogLevel.Debug,
     Message = "DataWrapper {ComponentTypeName} OnAfterRenderAsyncStarted")]
    public static partial void DataWrapperOnAfterRenderAsyncStarted(ILogger logger, string componentTypeName);

    [LoggerMessage(
        EventId = LogEvents.DataWrapper_OnAfterRender_Async_Completed,
        Level = LogLevel.Debug,
        Message = "DataWrapper {ComponentTypeName} OnAfterRenderAsyncCompleted")]
    public static partial void DataWrapperOnAfterRenderAsyncCompleted(ILogger logger, string componentTypeName);
}