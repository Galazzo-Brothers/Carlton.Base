namespace Carlton.Core.Components.Flux;

public static partial class Log
{
    [LoggerMessage(
      EventId = LogEvents.DataComponent_OnInitialized_Started,
      Level = LogLevel.Debug,
      Message = "DataComponent {ComponentTypeName} OnInitializedStarted")]
    public static partial void DataComponentOnInitializedStarted(ILogger logger, string componentTypeName);

    [LoggerMessage(
        EventId = LogEvents.DataComponent_OnInitialized_Completed,
        Level = LogLevel.Debug,
        Message = "DataComponent {ComponentTypeName} OnInitializedCompleted")]
    public static partial void DataComponentOnInitializedCompleted(ILogger logger, string componentTypeName);

    [LoggerMessage(
        EventId = LogEvents.DataComponent_OnInitialized_Async_Started,
        Level = LogLevel.Debug,
        Message = "DataComponent {ComponentTypeName} OnInitializedAsyncStarted")]
    public static partial void DataComponentOnInitializedAsyncStarted(ILogger logger, string componentTypeName);

    [LoggerMessage(
        EventId = LogEvents.DataComponent_OnInitialized_Async_Completed,
        Level = LogLevel.Debug,
        Message = "DataComponent {ComponentTypeName} OnInitializedAsyncCompleted")]
    public static partial void DataComponentOnInitializedAsyncCompleted(ILogger logger, string componentTypeName);

    [LoggerMessage(
       EventId = LogEvents.DataComponent_OnParametersSet_Started,
       Level = LogLevel.Debug,
       Message = "DataComponent {ComponentTypeName} OnParametersSetStarted")]
    public static partial void DataComponentOnParametersSetStarted(ILogger logger, string componentTypeName);

    [LoggerMessage(
        EventId = LogEvents.DataComponent_OnParametersSet_Completed,
        Level = LogLevel.Debug,
        Message = "DataComponent {ComponentTypeName} OnParametersSetCompleted")]
    public static partial void DataComponentOnParametersSetCompleted(ILogger logger, string componentTypeName);

    [LoggerMessage(
     EventId = LogEvents.DataComponent_OnParametersSet_Async_Started,
     Level = LogLevel.Debug,
     Message = "DataComponent {ComponentTypeName} OnParametersSetAsyncStarted")]
    public static partial void DataComponentOnParametersSetAsyncStarted(ILogger logger, string componentTypeName);

    [LoggerMessage(
        EventId = LogEvents.DataComponent_OnParametersSet_Async_Completed,
        Level = LogLevel.Debug,
        Message = "DataComponent {ComponentTypeName} OnParametersSetAsyncCompleted")]
    public static partial void DataComponentOnParametersSetAsyncCompleted(ILogger logger, string componentTypeName);

    [LoggerMessage(
      EventId = LogEvents.DataComponent_OnAfterRender_Started,
      Level = LogLevel.Debug,
      Message = "DataComponent {ComponentTypeName} OnAfterRenderStarted")]
    public static partial void DataComponentOnAfterRenderStarted(ILogger logger, string componentTypeName);

    [LoggerMessage(
        EventId = LogEvents.DataComponent_OnAfterRender_Completed,
        Level = LogLevel.Debug,
        Message = "DataComponent {ComponentTypeName} OnAfterRenderCompleted")]
    public static partial void DataComponentOnAfterRenderCompleted(ILogger logger, string componentTypeName);

    [LoggerMessage(
     EventId = LogEvents.DataComponent_OnAfterRender_Async_Started,
     Level = LogLevel.Debug,
     Message = "DataComponent {ComponentTypeName} OnAfterRenderAsyncStarted")]
    public static partial void DataComponentOnAfterRenderAsyncStarted(ILogger logger, string componentTypeName);

    [LoggerMessage(
        EventId = LogEvents.DataComponent_OnAfterRender_Async_Completed,
        Level = LogLevel.Debug,
        Message = "DataComponent {ComponentTypeName} OnAfterRenderAsyncCompleted")]
    public static partial void DataComponentOnAfterRenderAsyncCompleted(ILogger logger, string componentTypeName);
}
