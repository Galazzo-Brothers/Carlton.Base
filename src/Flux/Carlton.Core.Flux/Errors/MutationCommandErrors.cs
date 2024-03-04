namespace Carlton.Core.Flux.Errors;

public static class MutationCommandErrors
{
    public record MutationCommandError(string Message, int ErrorCode, Type MutationCommandType);

    public record ValidationError(Type mutationCommandType, IEnumerable<string> ValidationErrors)
          : MutationCommandError(new MutationCommandError($"{FluxLogs.Mutation_Validation_ErrorMsg} {mutationCommandType.GetDisplayName()}",
            FluxLogs.Mutation_Validation_Error,
            mutationCommandType));

    public record HttpJsonError(Type mutationCommandType)
         : MutationCommandError(new MutationCommandError($"{FluxLogs.Mutation_HTTP_JSON_ErrorMsg} {mutationCommandType.GetDisplayName()}",
           FluxLogs.Mutation_HttpInterception_Response_JSON_Error,
           mutationCommandType));

    public record HttpError(Type mutationCommandType)
         : MutationCommandError(new MutationCommandError($"{FluxLogs.Mutation_HTTP_ErrorMsg} {mutationCommandType.GetDisplayName()}",
           FluxLogs.Mutation_HttpInterception_Request_Error,
           mutationCommandType));

    public record HttpUrlError(Type mutationCommandType)
         : MutationCommandError(new MutationCommandError($"{FluxLogs.Mutation_HTTP_URL_ErrorMsg} {mutationCommandType.GetDisplayName()}",
           FluxLogs.ViewModel_HTTP_URL_Error,
           mutationCommandType));


    public record HttpResponseUpdateError(Type mutationCommandType)
        : MutationCommandError(new MutationCommandError($"{FluxLogs.Mutation_HttpInterception_Response_Update_Error} {mutationCommandType.GetDisplayName()}",
          FluxLogs.Mutation_HttpInterception_Response_Update_Error,
          mutationCommandType));

    public record MutationApplyError(Type mutationCommandType)
        : MutationCommandError(new MutationCommandError($"{FluxLogs.Mutation_Apply_ErrorMsg} {mutationCommandType.GetDisplayName()}",
           FluxLogs.Mutation_Apply_Error,
           mutationCommandType));

    public record LocalStorageJsonError(Type mutationCommandType)
        : MutationCommandError(new MutationCommandError($"{FluxLogs.Mutation_LocalStorage_JSON_ErrorMsg} {mutationCommandType.GetDisplayName()}",
          FluxLogs.Mutation_SaveLocalStorage_JSON_Error,
          mutationCommandType));

    public record UnhandledError(Type mutationCommandType)
      : MutationCommandError(new MutationCommandError($"{FluxLogs.Mutation_Unhandled_ErrorMsg} {mutationCommandType.GetDisplayName()}",
        FluxLogs.Mutation_Unhandled_Error,
        mutationCommandType));
}






