namespace Carlton.Core.Components.Flux;

public static partial class Log
{
    public const string MutationScopeMessage = "Initiating Mutation {MutationType} : {MutationID}";

    [LoggerMessage(
       EventId = LogEvents.Mutation_Started,
       Level = LogLevel.Information,
       Message = "Starting Mutation {Type} : {Mutation}")]
    public static partial void MutationStarted(ILogger logger, string type, MutationCommand mutation);

    [LoggerMessage(
      EventId = LogEvents.Mutation_Validation_Started,
      Level = LogLevel.Information,
      Message = "Started Validating Mutation {Type}")]
    public static partial void MutationValidationStarted(ILogger logger, string type);

    [LoggerMessage(
      EventId = LogEvents.Mutation_Validation_Completed,
      Level = LogLevel.Information,
      Message = "Completed Validating Mutation {Type}")]
    public static partial void MutationValidationCompleted(ILogger logger, string type);

    [LoggerMessage(
     EventId = LogEvents.Mutation_Validation_Error,
     Level = LogLevel.Error,
     Message = "Error Validating Mutation {Type}")]
    public static partial void MutationValidationError(ILogger logger, Exception ex, string type);

    [LoggerMessage(
      EventId = LogEvents.Mutation_JsInterop_Started,
      Level = LogLevel.Information,
      Message = "Started Mutation JSInterop Interception {Type}")]
    public static partial void MutationJSInteropInterceptionStarted(ILogger logger, string type);

    [LoggerMessage(
        EventId = LogEvents.Mutation_JsInterop_Completed,
        Level = LogLevel.Information,
        Message = "Completed Mutation JSInterop Interception {Type}")]
    public static partial void MutationJSInteropInterceptionCompleted(ILogger logger, string type);

    [LoggerMessage(
        EventId = LogEvents.Mutation_JsInterop_Skipped,
        Level = LogLevel.Information,
        Message = "Skipping Mutation JSInterop Interception {Type}")]
    public static partial void MutationJSInteropInterceptionSkipped(ILogger logger, string type);

    [LoggerMessage(
        EventId = LogEvents.Mutation_JSInterop_Error,
        Level = LogLevel.Error,
        Message = "Errored Mutation JSInterop Interception {Type}")]
    public static partial void MutationJSInteropInterceptionError(ILogger logger, Exception ex, string type);

    [LoggerMessage(
       EventId = LogEvents.Mutation_HttpCall_Started,
       Level = LogLevel.Information,
       Message = "Started Mutation Http Interception {Type}")]
    public static partial void MutationHttpInterceptionStarted(ILogger logger, string type);

    [LoggerMessage(
        EventId = LogEvents.Mutation_HttpCall_Completed,
        Level = LogLevel.Information,
        Message = "Completed Mutation Http Interception {Type}")]
    public static partial void MutationHttpInterceptionCompleted(ILogger logger, string type);

    [LoggerMessage(
        EventId = LogEvents.Mutation_HttpCall_Skipped,
        Level = LogLevel.Information,
        Message = "Skipping Mutation Http Interception {Type}")]
    public static partial void MutationHttpInterceptionSkipped(ILogger logger, string type);

    [LoggerMessage(
        EventId = LogEvents.Mutation_HTTP_Error,
        Level = LogLevel.Error,
        Message = "Errored Mutation Http Interception {Type}")]
    public static partial void MutationHttpInterceptionError(ILogger logger, Exception ex, string type);

    [LoggerMessage(
       EventId = LogEvents.Mutation_Completed,
       Level = LogLevel.Information,
       Message = "Completed Mutation {Type}")]
    public static partial void MutationCompleted(ILogger logger, string type);

    [LoggerMessage(
    EventId = LogEvents.Mutation_JSON_Error,
    Level = LogLevel.Error,
    Message = "An error occurred parsing, serializing or de-serializing JSON for Command {Type}")]
    public static partial void MutationJsonError(ILogger logger, Exception ex, string type);

    [LoggerMessage(
      EventId = LogEvents.Mutation_Unhandled_Error,
      Level = LogLevel.Error,
      Message = "An unhandled error occurred while processing a Mutation Command {Type}")]
    public static partial void MutationUnhandledError(ILogger logger, Exception ex, string type);
}
