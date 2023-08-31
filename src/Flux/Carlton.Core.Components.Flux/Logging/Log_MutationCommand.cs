namespace Carlton.Core.Components.Flux;

public static partial class Log
{
    public const string MutationScopeMessage = "Initiating Mutation {Type} : {MutationID}";

    [LoggerMessage(
       EventId = LogEvents.Mutation_Started,
       Level = LogLevel.Information,
       Message = "Starting processing mutation command of type {Type}. Mutation : {Mutation}")]
    public static partial void MutationStarted(ILogger logger, string type, MutationCommand mutation);

    [LoggerMessage(
      EventId = LogEvents.Mutation_Validation_Started,
      Level = LogLevel.Information,
      Message = "Started validating mutation command of type {Type}.")]
    public static partial void MutationValidationStarted(ILogger logger, string type);

    [LoggerMessage(
      EventId = LogEvents.Mutation_Validation_Completed,
      Level = LogLevel.Information,
      Message = "Completed validating mutation command of type {Type}.")]
    public static partial void MutationValidationCompleted(ILogger logger, string type);

    [LoggerMessage(
     EventId = LogEvents.Mutation_Validation_Error,
     Level = LogLevel.Error,
     Message = "An error occured while validating mutation command of {Type}, Error Details: {ex}.")]
    public static partial void MutationValidationError(ILogger logger, Exception ex, string type);

    [LoggerMessage(
      EventId = LogEvents.Mutation_JsInterop_Started,
      Level = LogLevel.Information,
      Message = "Started JSInterop interception for mutation command of type {Type}.")]
    public static partial void MutationJSInteropInterceptionStarted(ILogger logger, string type);

    [LoggerMessage(
        EventId = LogEvents.Mutation_JsInterop_Completed,
        Level = LogLevel.Information,
        Message = "Completed JSInterop interception for mutation command of type {Type}.")]
    public static partial void MutationJSInteropInterceptionCompleted(ILogger logger, string type);

    [LoggerMessage(
        EventId = LogEvents.Mutation_JsInterop_Skipped,
        Level = LogLevel.Information,
        Message = "Skipping JSInterop interception for mutation command of type {Type}.")]
    public static partial void MutationJSInteropInterceptionSkipped(ILogger logger, string type);

    [LoggerMessage(
        EventId = LogEvents.Mutation_JSInterop_Error,
        Level = LogLevel.Error,
        Message = "An error occured during JSInterop Interception for mutation command of {Type}, Error Details: {ex}.")]
    public static partial void MutationJSInteropInterceptionError(ILogger logger, Exception ex, string type);

    [LoggerMessage(
       EventId = LogEvents.Mutation_HttpCall_Started,
       Level = LogLevel.Information,
       Message = "Started Http interception for mutation command of type {Type}.")]
    public static partial void MutationHttpInterceptionStarted(ILogger logger, string type);

    [LoggerMessage(
        EventId = LogEvents.Mutation_HttpCall_Completed,
        Level = LogLevel.Information,
        Message = "Completed Http interception for mutation command of type {Type}.")]
    public static partial void MutationHttpInterceptionCompleted(ILogger logger, string type);

    [LoggerMessage(
        EventId = LogEvents.Mutation_HttpCall_Skipped,
        Level = LogLevel.Information,
        Message = "Skipping Http interception for mutation command of type {Type}.")]
    public static partial void MutationHttpInterceptionSkipped(ILogger logger, string type);

    [LoggerMessage(
        EventId = LogEvents.Mutation_HTTP_URL_Error,
        Level = LogLevel.Error,
        Message = "An error occurred while constructing the Http interception URL for the mutation command of {type}, Error Details: {ex},")]
    public static partial void MutationHttpUrlError(ILogger logger, Exception ex, string type);

    [LoggerMessage(
        EventId = LogEvents.Mutation_HTTP_Error,
        Level = LogLevel.Error,
        Message = "An error occurred during the Http interception for the mutation command of {type}, Error Details: {ex}.")]
    public static partial void MutationHttpInterceptionError(ILogger logger, Exception ex, string type);

    [LoggerMessage(
       EventId = LogEvents.Mutation_HTTP_Response_Update_Error,
       Level = LogLevel.Error,
       Message = "An error occurred during the Http interception response update for the mutation command of {type}, Error Details: {ex}.")]
    public static partial void MutationHttpResponseUpdateError(ILogger logger, Exception ex, string type);

    [LoggerMessage(
      EventId = LogEvents.Mutation_Apply_Started,
      Level = LogLevel.Information,
      Message = "Started mutating the state store with mutation command of type {Type}.")]
    public static partial void MutationApplyStarted(ILogger logger, string type);

    [LoggerMessage(
        EventId = LogEvents.Mutation_Apply_Completed,
        Level = LogLevel.Information,
        Message = "Completed mutating the state store with mutation command of type {Type}.")]
    public static partial void MutationApplyCompleted(ILogger logger, string type);

    [LoggerMessage(
      EventId = LogEvents.Mutation_Apply_Error,
      Level = LogLevel.Error,
      Message = "An error occured while mutating the state store with mutation command of type {Type}, rolling back state store to it's previous state. Error Details: {ex}.")]
    public static partial void MutationApplyError(ILogger logger, Exception ex, string type);

    [LoggerMessage(
       EventId = LogEvents.Mutation_Completed,
       Level = LogLevel.Information,
       Message = "Completed processing mutation command of type {Type}.")]
    public static partial void MutationCompleted(ILogger logger, string type);

    [LoggerMessage(
    EventId = LogEvents.Mutation_JSON_Error,
    Level = LogLevel.Error,
    Message = "An error occurred parsing, serializing or de-serializing JSON for muration command of type {Type}, Error Details: {ex}.")]
    public static partial void MutationJsonError(ILogger logger, Exception ex, string type);

    [LoggerMessage(
      EventId = LogEvents.Mutation_Unhandled_Error,
      Level = LogLevel.Error,
      Message = "An unhandled error occurred while processing a mutation command of type {Type}, Error Details: {ex}.")]
    public static partial void MutationUnhandledError(ILogger logger, Exception ex, string type);
}
