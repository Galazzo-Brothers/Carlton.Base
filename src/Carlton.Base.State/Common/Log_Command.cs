namespace Carlton.Base.State;

public static partial class Log
{
    public const string CommandScopeMessage = "Initiating Command Request {Command}, : {RequestID}";

    [LoggerMessage(
       EventId = LogEvents.Command_Started,
       Level = LogLevel.Information,
       Message = "Starting Command Request {CommandName}, : {RequestID}")]
    public static partial void CommandRequestStarted(ILogger logger, string commandName, Guid requestID);

    [LoggerMessage(
        EventId = LogEvents.Command_Handling_Started,
        Level = LogLevel.Information,
        Message = "Started Handling {RequestName}: {Request}")]
    public static partial void CommandRequestHandlingStarted(ILogger logger, string requestName, object request);

    [LoggerMessage(
      EventId = LogEvents.Command_Validation_Started,
      Level = LogLevel.Information,
      Message = "Started Validating {RequestName}: {Request}")]
    public static partial void CommandRequestValidationStarted(ILogger logger, string requestName, object request);

    [LoggerMessage(
      EventId = LogEvents.Command_Validation_Completed,
      Level = LogLevel.Information,
      Message = "Completed Validating {RequestName}: {Request}")]
    public static partial void CommandRequestValidationCompleted(ILogger logger, string requestName, object request);

    [LoggerMessage(
     EventId = LogEvents.Command_Validation_Error,
     Level = LogLevel.Error,
     Message = "Completed Validating {RequestName}: {Request}")]
    public static partial void CommandRequestValidationError(ILogger logger, Exception ex, string requestName, object request);

    [LoggerMessage(
       EventId = LogEvents.Command_HttpCall_Started,
       Level = LogLevel.Information,
       Message = "Started Command Http call {RequestName}: {Request}")]
    public static partial void CommandRequestHttpCallStarted(ILogger logger, string requestName, object request);

    [LoggerMessage(
        EventId = LogEvents.Command_HttpCall_Completed,
        Level = LogLevel.Information,
        Message = "Completed Command Http call {RequestName}: {Request}")]
    public static partial void CommandRequestHttpCallCompleted(ILogger logger, string requestName, object request);

    [LoggerMessage(
        EventId = LogEvents.Command_HttpCall_Skipped,
        Level = LogLevel.Information,
        Message = "Skipping Command Http call {RequestName}: {Request}")]
    public static partial void CommandRequestHttpCallSkipped(ILogger logger, string requestName, object request);

    [LoggerMessage(
        EventId = LogEvents.Command_HttpCall_Error,
        Level = LogLevel.Error,
        Message = "Error during Command Http call {RequestName}: {Request}")]
    public static partial void CommandRequestHttpCallError(ILogger logger, Exception ex, string requestName, object request);

    [LoggerMessage(
        EventId = LogEvents.Command_Processing_Started,
        Level = LogLevel.Information,
        Message = "Started Processing {RequestName}: {Request}, {BeforeState}")]
    public static partial void CommandRequestProcessingStarted(ILogger logger, string requestName, object request, object beforeState);

    [LoggerMessage(
        EventId = LogEvents.Command_Processing_Completed,
        Level = LogLevel.Information,
        Message = "Completed Processing {RequestName}: {Request} {AfterState}")]
    public static partial void CommandRequestProcessingCompleted(ILogger logger, string requestName, object request, object afterState);

    [LoggerMessage(
      EventId = LogEvents.Command_Processing_Error,
      Level = LogLevel.Error,
      Message = "Error Processing {RequestName}, Rolling back state changes: {Request}")]
    public static partial void CommandRequestProcessingError(ILogger logger, Exception ex, string requestName, object request);

    [LoggerMessage(
        EventId = LogEvents.Command_Handling_Completed,
        Level = LogLevel.Information,
        Message = "Completed Handling {RequestName}: {Request}")]
    public static partial void CommandRequestHandlingCompleted(ILogger logger, string requestName, object request);

    [LoggerMessage(
       EventId = LogEvents.Command_Completed,
       Level = LogLevel.Information,
       Message = "Completed Command Request {CommandName}, : {RequestID}")]
    public static partial void CommandRequestCompleted(ILogger logger, string commandName, Guid requestID);

}
