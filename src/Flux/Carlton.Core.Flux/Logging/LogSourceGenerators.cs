namespace Carlton.Core.Flux.Logging;

public static partial class LogSourceGenerators
{
    [LoggerMessage(
        EventId = FluxLogs.ViewModel_Completed,
        Level = LogLevel.Information,
        Message = "Completed processing query for ViewModel of type {ViewModelType}")]
    public static partial void ViewModelQueryCompleted(this ILogger logger, string viewModelType);

    [LoggerMessage(
        EventId = FluxLogs.Mutation_Completed,
        Level = LogLevel.Information,
        Message = "Completed processing mutation of type {MutationCommandType}")]
    public static partial void MutationCommandCompleted(this ILogger logger, string mutationCommandType);
    
    [LoggerMessage(
        EventId = FluxLogs.ViewModel_JsInterop_Completed,
        Level = LogLevel.Information,
        Message = "Completed JSInterop query for ViewModel of type {ViewModelType}")]
    public static partial void ViewModelJsInteropQueryCompleted(this ILogger logger, string viewModelType);

    [LoggerMessage(
       Level = LogLevel.Error,
       Message = "An error occured processing query for ViewModel of type {ViewModelType}")]
    public static partial void ViewModelQueryErrored(this ILogger logger, string viewModelType, Exception ex);

    [LoggerMessage(
       Level = LogLevel.Error,
       Message = "An error occured processing mutation command of type {MutationCommandType}")]
    public static partial void MutationCommandErrored(this ILogger logger, string MutationCommandType, Exception ex);
    
    [LoggerMessage(
       EventId = FluxLogs.ViewModel_JsInterop_Error,
       Level = LogLevel.Error,
       Message = "An error occurred during JSInterop query for ViewModel of type {ViewModelType}")]
    public static partial void ViewModelJsInteropQueryErrored(this ILogger logger, Exception ex, string viewModelType);
}