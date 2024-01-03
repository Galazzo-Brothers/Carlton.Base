namespace Carlton.Core.Flux.Logging;

public static partial class LogMutationCommand
{
    [LoggerMessage(
       EventId = LogEvents.Mutation_Started,
       Level = LogLevel.Information,
       Message = "Starting processing mutation command of type {Type}.")]
    public static partial void MutationStarted(this ILogger logger, string type);

    [LoggerMessage(
      EventId = LogEvents.Mutation_Validation_Started,
      Level = LogLevel.Debug,
      Message = "Started validating mutation command of type {Type}.")]
    public static partial void MutationValidationStarted(this ILogger logger, string type);

    [LoggerMessage(
      EventId = LogEvents.Mutation_Validation_Completed,
      Level = LogLevel.Debug,
      Message = "Completed validating mutation command of type {Type}.")]
    public static partial void MutationValidationCompleted(this ILogger logger, string type);

    [LoggerMessage(
     EventId = LogEvents.Mutation_Validation_Error,
     Level = LogLevel.Error,
     Message = "An error occurred while validating mutation command of {Type}.")]
    public static partial void MutationValidationError(this ILogger logger, Exception ex, string type);

    [LoggerMessage(
       EventId = LogEvents.Mutation_HttpCall_Started,
       Level = LogLevel.Information,
       Message = "Started Http interception for mutation command of type {Type}.")]
    public static partial void MutationHttpInterceptionStarted(this ILogger logger, string type);

    [LoggerMessage(
        EventId = LogEvents.Mutation_HttpCall_Completed,
        Level = LogLevel.Information,
        Message = "Completed Http interception for mutation command of type {Type}.")]
    public static partial void MutationHttpInterceptionCompleted(this ILogger logger, string type);

    [LoggerMessage(
        EventId = LogEvents.Mutation_HttpCall_Skipped,
        Level = LogLevel.Debug,
        Message = "Skipping Http interception for mutation command of type {Type}.")]
    public static partial void MutationHttpInterceptionSkipped(this ILogger logger, string type);

    [LoggerMessage(
        EventId = LogEvents.Mutation_HTTP_URL_Error,
        Level = LogLevel.Error,
        Message = "An error occurred while constructing the Http interception URL for the mutation command of {type}.")]
    public static partial void MutationHttpUrlError(this ILogger logger, Exception ex, string type);

    [LoggerMessage(
     EventId = LogEvents.Mutation_HTTP_JSON_Error,
     Level = LogLevel.Error,
     Message = "An error occurred during the serialization phase of the Http interception for the mutation command of {type}.")]
    public static partial void MutationHttpInterceptionJsonParseError(this ILogger logger, Exception ex, string type);

    [LoggerMessage(
        EventId = LogEvents.Mutation_HTTP_Error,
        Level = LogLevel.Error,
        Message = "An error occurred during the Http interception for the mutation command of {type}.")]
    public static partial void MutationHttpInterceptionError(this ILogger logger, Exception ex, string type);

    [LoggerMessage(
       EventId = LogEvents.Mutation_HTTP_Response_Update_Error,
       Level = LogLevel.Error,
       Message = "An error occurred during the Http interception response update for the mutation command of {type}.")]
    public static partial void MutationHttpResponseUpdateError(this ILogger logger, Exception ex, string type);

    [LoggerMessage(
      EventId = LogEvents.Mutation_Apply_Started,
      Level = LogLevel.Debug,
      Message = "Started mutating the state store with mutation command of type {Type}.")]
    public static partial void MutationApplyStarted(this ILogger logger, string type);

    [LoggerMessage(
        EventId = LogEvents.Mutation_Apply_Completed,
        Level = LogLevel.Debug,
        Message = "Completed mutating the state store with mutation command of type {Type}.")]
    public static partial void MutationApplyCompleted(this ILogger logger, string type);

    [LoggerMessage(
      EventId = LogEvents.Mutation_Apply_Error,
      Level = LogLevel.Error,
      Message = "An error occurred while mutating the state store with mutation command of type {Type}, rolling back state store to it's previous state.")]
    public static partial void MutationApplyError(this ILogger logger, Exception ex, string type);

    [LoggerMessage(
       EventId = LogEvents.Mutation_Completed,
       Level = LogLevel.Information,
       Message = "Completed processing mutation command of type {Type}.")]
    public static partial void MutationCompleted(this ILogger logger, string type);

    [LoggerMessage(
       EventId = LogEvents.Mutation_LocalStorage_Started,
       Level = LogLevel.Debug,
       Message = "Started updating local storage for mutation command of type {Type}.")]
    public static partial void MutationLocalStorageStarted(this ILogger logger, string type);

    [LoggerMessage(
        EventId = LogEvents.Mutation_LocalStorage_Completed,
        Level = LogLevel.Debug,
        Message = "Completed updating local storage mutation command of type {Type}.")]
    public static partial void MutationLocalStorageCompleted(this ILogger logger, string type);

    [LoggerMessage(
        EventId = LogEvents.Mutation_LocalStorage_JSON_Error,
        Level = LogLevel.Warning,
        Message = "An error occurred serializing mutation command of type {Type} for local storage.")]
    public static partial void MutationLocalStorageJsonError(this ILogger logger, Exception ex, string type);

    [LoggerMessage(
        EventId = LogEvents.Mutation_LocalStorage_Unhandled_Error,
        Level = LogLevel.Warning,
        Message = "An unhandled error occurred while committing the mutation command of type {Type} to local storage.")]
    public static partial void MutationLocalStorageUnhandledError(this ILogger logger, Exception ex, string type);

    [LoggerMessage(
      EventId = LogEvents.Mutation_Unhandled_Error,
      Level = LogLevel.Error,
      Message = "An unhandled error occurred while processing a mutation command of type {Type}.")]
    public static partial void MutationUnhandledError(this ILogger logger, Exception ex, string type);
}
