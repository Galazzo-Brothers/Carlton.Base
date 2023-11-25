namespace Carlton.Core.Components.Flux;

public static partial class Log
{
    [LoggerMessage(
       EventId = LogEvents.Mutation_Started,
       Level = LogLevel.Information,
       Message = "Starting processing mutation command of type {Type}. Mutation : {Mutation}")]
    public static partial void MutationStarted(ILogger logger, string type, MutationCommand mutation);

    [LoggerMessage(
      EventId = LogEvents.Mutation_Validation_Started,
      Level = LogLevel.Debug,
      Message = "Started validating mutation command of type {Type}.")]
    public static partial void MutationValidationStarted(ILogger logger, string type);

    [LoggerMessage(
      EventId = LogEvents.Mutation_Validation_Completed,
      Level = LogLevel.Debug,
      Message = "Completed validating mutation command of type {Type}.")]
    public static partial void MutationValidationCompleted(ILogger logger, string type);

    [LoggerMessage(
     EventId = LogEvents.Mutation_Validation_Error,
     Level = LogLevel.Error,
     Message = "An error occurred while validating mutation command of {Type}.")]
    public static partial void MutationValidationError(ILogger logger, Exception ex, string type);

    [LoggerMessage(
      EventId = LogEvents.Mutation_JsInterop_Started,
      Level = LogLevel.Debug,
      Message = "Started JSInterop interception for mutation command of type {Type}.")]
    public static partial void MutationJSInteropInterceptionStarted(ILogger logger, string type);

    [LoggerMessage(
        EventId = LogEvents.Mutation_JsInterop_Completed,
        Level = LogLevel.Debug,
        Message = "Completed JSInterop interception for mutation command of type {Type}.")]
    public static partial void MutationJSInteropInterceptionCompleted(ILogger logger, string type);

    [LoggerMessage(
        EventId = LogEvents.Mutation_JsInterop_Skipped,
        Level = LogLevel.Debug,
        Message = "Skipping JSInterop interception for mutation command of type {Type}.")]
    public static partial void MutationJSInteropInterceptionSkipped(ILogger logger, string type);

    [LoggerMessage(
        EventId = LogEvents.Mutation_JSInterop_Error,
        Level = LogLevel.Error,
        Message = "An error occurred during JSInterop Interception for mutation command of {Type}.")]
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
        Level = LogLevel.Debug,
        Message = "Skipping Http interception for mutation command of type {Type}.")]
    public static partial void MutationHttpInterceptionSkipped(ILogger logger, string type);

    [LoggerMessage(
        EventId = LogEvents.Mutation_HTTP_URL_Error,
        Level = LogLevel.Error,
        Message = "An error occurred while constructing the Http interception URL for the mutation command of {type}.")]
    public static partial void MutationHttpUrlError(ILogger logger, Exception ex, string type);

    [LoggerMessage(
     EventId = LogEvents.Mutation_HTTP_JSON_Error,
     Level = LogLevel.Error,
     Message = "An error occurred during the serialization phase of the Http interception for the mutation command of {type}.")]
    public static partial void MutationHttpInterceptionJsonParseError(ILogger logger, Exception ex, string type);

    [LoggerMessage(
        EventId = LogEvents.Mutation_HTTP_Error,
        Level = LogLevel.Error,
        Message = "An error occurred during the Http interception for the mutation command of {type}.")]
    public static partial void MutationHttpInterceptionError(ILogger logger, Exception ex, string type);

    [LoggerMessage(
       EventId = LogEvents.Mutation_HTTP_Response_Update_Error,
       Level = LogLevel.Error,
       Message = "An error occurred during the Http interception response update for the mutation command of {type}.")]
    public static partial void MutationHttpResponseUpdateError(ILogger logger, Exception ex, string type);

    [LoggerMessage(
      EventId = LogEvents.Mutation_Apply_Started,
      Level = LogLevel.Debug,
      Message = "Started mutating the state store with mutation command of type {Type}.")]
    public static partial void MutationApplyStarted(ILogger logger, string type);

    [LoggerMessage(
        EventId = LogEvents.Mutation_Apply_Completed,
        Level = LogLevel.Debug,
        Message = "Completed mutating the state store with mutation command of type {Type}.")]
    public static partial void MutationApplyCompleted(ILogger logger, string type);

    [LoggerMessage(
      EventId = LogEvents.Mutation_Apply_Error,
      Level = LogLevel.Error,
      Message = "An error occurred while mutating the state store with mutation command of type {Type}, rolling back state store to it's previous state.")]
    public static partial void MutationApplyError(ILogger logger, Exception ex, string type);

    [LoggerMessage(
       EventId = LogEvents.Mutation_Completed,
       Level = LogLevel.Information,
       Message = "Completed processing mutation command of type {Type}.")]
    public static partial void MutationCompleted(ILogger logger, string type);

    [LoggerMessage(
       EventId = LogEvents.Mutation_LocalStorage_Started,
       Level = LogLevel.Debug,
       Message = "Started updating local storage for mutation command of type {Type}.")]
    public static partial void MutationLocalStorageStarted(ILogger logger, string type);

    [LoggerMessage(
        EventId = LogEvents.Mutation_LocalStorage_Completed,
        Level = LogLevel.Debug,
        Message = "Completed updating local storage mutation command of type {Type}.")]
    public static partial void MutationLocalStorageCompleted(ILogger logger, string type);

    [LoggerMessage(
        EventId = LogEvents.Mutation_LocalStorage_JSON_Error,
        Level = LogLevel.Error,
        Message = "An error occurred serializing mutation command of type {Type} for local storage.")]
    public static partial void MutationJsonError(ILogger logger, Exception ex, string type);

    [LoggerMessage(
      EventId = LogEvents.Mutation_Unhandled_Error,
      Level = LogLevel.Error,
      Message = "An unhandled error occurred while processing a mutation command of type {Type}.")]
    public static partial void MutationUnhandledError(ILogger logger, Exception ex, string type);
}
