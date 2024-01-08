namespace Carlton.Core.Flux.Logging;

public static partial class LogMutationCommand
{
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
        EventId = LogEvents.Mutation_HttpInterception_Completed,
        Level = LogLevel.Information,
        Message = "Completed Http interception for mutation command of type {Type}.")]
    public static partial void MutationHttpInterceptionCompleted(this ILogger logger, string type);

    [LoggerMessage(
        EventId = LogEvents.Mutation_HttpInterception_Skipped,
        Level = LogLevel.Debug,
        Message = "Skipped Http interception for mutation command of type {Type}.")]
    public static partial void MutationHttpInterceptionSkipped(this ILogger logger, string type);

    [LoggerMessage(
        EventId = LogEvents.Mutation_HttpInterception_UrlConstruction_Error,
        Level = LogLevel.Error,
        Message = "An error occurred while constructing the Http interception URL for the mutation command of {type}.")]
    public static partial void MutationHttpInterceptionUrlConstructionError(this ILogger logger, Exception ex, string type);

    [LoggerMessage(
        EventId = LogEvents.Mutation_HttpInterception_Response_JSON_Error,
        Level = LogLevel.Error,
        Message = "An error occurred during the serialization phase of the Http interception for the mutation command of {type}.")]
    public static partial void MutationHttpInterceptionJsonResponseParseError(this ILogger logger, Exception ex, string type);

    [LoggerMessage(
        EventId = LogEvents.Mutation_HttpInterception_Request_Error,
        Level = LogLevel.Error,
        Message = "An error occurred during the Http interception for the mutation command of {type}.")]
    public static partial void MutationHttpInterceptionRequestError(this ILogger logger, Exception ex, string type);

    [LoggerMessage(
        EventId = LogEvents.Mutation_HttpInterception_Response_Update_Error,
        Level = LogLevel.Error,
        Message = "An error occurred during the Http interception response update for the mutation command of {type}.")]
    public static partial void MutationHttpInterceptionResponseUpdateError(this ILogger logger, Exception ex, string type);

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
        EventId = LogEvents.Mutation_SaveLocalStorage_Completed,
        Level = LogLevel.Debug,
        Message = "Completed updating local storage mutation command of type {Type}.")]
    public static partial void MutationSaveLocalStorageCompleted(this ILogger logger, string type);

    [LoggerMessage(
        EventId = LogEvents.Mutation_SaveLocalStorage_JSON_Error,
        Level = LogLevel.Warning,
        Message = "An error occurred serializing mutation command of type {Type} for local storage.")]
    public static partial void MutationSaveLocalStorageJsonError(this ILogger logger, Exception ex, string type);

    [LoggerMessage(
        EventId = LogEvents.Mutation_SaveLocalStorage_Error,
        Level = LogLevel.Warning,
        Message = "An unhandled error occurred while committing the mutation command of type {Type} to local storage.")]
    public static partial void MutationCommandSaveLocalStorageError(this ILogger logger, Exception ex, string type);

    [LoggerMessage(
        EventId = LogEvents.Mutation_Error,
        Level = LogLevel.Error,
        Message = "An unhandled error occurred while processing a mutation command of type {Type}.")]
    public static partial void MutationError(this ILogger logger, Exception ex, string type);
}
