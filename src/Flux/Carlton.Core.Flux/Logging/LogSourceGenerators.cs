namespace Carlton.Core.Flux.Logging;

public static partial class LogSourceGenerators
{
    [LoggerMessage(
       EventId = LogEvents.ViewModel_Completed,
       Level = LogLevel.Information,
   Message = "Completed processing query for ViewModel of type {Type}")]
    public static partial void ViewModelCompleted(this ILogger logger, string type);

    [LoggerMessage(
        EventId = LogEvents.ViewModel_JsInterop_Completed,
        Level = LogLevel.Debug,
        Message = "Completed JSInterop refresh for ViewModel of type {Type}")]
    public static partial void ViewModelJsInteropRefreshCompleted(this ILogger logger, string type);

    [LoggerMessage(
        EventId = LogEvents.ViewModel_JsInterop_Error,
        Level = LogLevel.Error,
        Message = "An error occurred during JSInterop refresh for ViewModel of type {Type}")]
    public static partial void ViewModelJsInteropRefreshErrored
        (this ILogger logger, Exception ex, string type);

    [LoggerMessage(
         EventId = LogEvents.Mutation_Completed,
         Level = LogLevel.Information,
         Message = "Completed processing mutation of type {Type}")]
    public static partial void MutationCompleted(this ILogger logger, string type);
}