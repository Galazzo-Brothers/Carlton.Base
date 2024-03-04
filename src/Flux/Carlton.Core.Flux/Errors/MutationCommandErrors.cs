namespace Carlton.Core.Flux.Errors;

public static class MutationCommandErrors
{
    public record MutationCommandFluxError(string Message, int ErrorCode, Type MutationCommandType);

    public record ValidationError(Type mutationCommandType, IEnumerable<string> ValidationErrors)
          : MutationCommandFluxError(new MutationCommandFluxError($"{FluxLogs.Mutation_Validation_ErrorMsg} {mutationCommandType.GetDisplayName()}",
            FluxLogs.Mutation_Validation_Error,
            mutationCommandType));

    public record HttpJsonError(Type mutationCommandType)
         : MutationCommandFluxError(new MutationCommandFluxError($"{FluxLogs.Mutation_HTTP_JSON_ErrorMsg} {mutationCommandType.GetDisplayName()}",
           FluxLogs.Mutation_HttpInterception_Response_JSON_Error,
           mutationCommandType));

    public record HttpError(Type mutationCommandType)
         : MutationCommandFluxError(new MutationCommandFluxError($"{FluxLogs.Mutation_HTTP_ErrorMsg} {mutationCommandType.GetDisplayName()}",
           FluxLogs.Mutation_HttpInterception_Request_Error,
           mutationCommandType));

    public record HttpUrlError(Type mutationCommandType)
         : MutationCommandFluxError(new MutationCommandFluxError($"{FluxLogs.Mutation_HTTP_URL_ErrorMsg} {mutationCommandType.GetDisplayName()}",
           FluxLogs.ViewModel_HTTP_URL_Error,
           mutationCommandType));


    public record HttpResponseUpdateError(Type mutationCommandType)
        : MutationCommandFluxError(new MutationCommandFluxError($"{FluxLogs.Mutation_HttpInterception_Response_Update_Error} {mutationCommandType.GetDisplayName()}",
          FluxLogs.Mutation_HttpInterception_Response_Update_Error,
          mutationCommandType));

    public record MutationApplyError(Type mutationCommandType)
        : MutationCommandFluxError(new MutationCommandFluxError($"{FluxLogs.Mutation_Apply_ErrorMsg} {mutationCommandType.GetDisplayName()}",
           FluxLogs.Mutation_Apply_Error,
           mutationCommandType));

    public record LocalStorageJsonError(Type mutationCommandType)
        : MutationCommandFluxError(new MutationCommandFluxError($"{FluxLogs.Mutation_LocalStorage_JSON_ErrorMsg} {mutationCommandType.GetDisplayName()}",
          FluxLogs.Mutation_SaveLocalStorage_JSON_Error,
          mutationCommandType));

    public record UnhandledError(Type mutationCommandType)
      : MutationCommandFluxError(new MutationCommandFluxError($"{FluxLogs.Mutation_Unhandled_ErrorMsg} {mutationCommandType.GetDisplayName()}",
        FluxLogs.Mutation_Unhandled_Error,
        mutationCommandType));
}






