namespace Carlton.Core.Flux.Exceptions;


public class MutationCommandFluxException<TState, TCommand> : FluxException
{
    private MutationCommandFluxException(int eventID, string message, MutationCommandContext<TCommand> context, Exception innerException)
        : base(eventID, context, message, innerException)
    {
        Context = context;
    }

    public static MutationCommandFluxException<TState, TCommand> ValidationError(MutationCommandContext<TCommand> context, ValidationException innerException)
    {
        var message = $"{LogEvents.Mutation_Validation_ErrorMsg} {context.CommandTypeName}";
        return new MutationCommandFluxException<TState, TCommand>(LogEvents.Mutation_Validation_Error, message, context, innerException);
    }

    public static MutationCommandFluxException<TState, TCommand> HttpJsonError(MutationCommandContext<TCommand> context, JsonException innerException)
    {
        var message = $"{LogEvents.Mutation_HTTP_JSON_ErrorMsg} {context.CommandTypeName}";
        return new MutationCommandFluxException<TState, TCommand>(LogEvents.Mutation_HttpInterception_Response_JSON_Error, message, context, innerException);
    }

    public static MutationCommandFluxException<TState, TCommand> HttpJsonError(MutationCommandContext<TCommand> context, NotSupportedException innerException)
    {
        var message = $"{LogEvents.Mutation_HTTP_JSON_ErrorMsg} {context.CommandTypeName}";
        return new MutationCommandFluxException<TState, TCommand>(LogEvents.Mutation_HttpInterception_Response_JSON_Error, message, context, innerException);
    }

    public static MutationCommandFluxException<TState, TCommand> HttpError(MutationCommandContext<TCommand> context, HttpRequestException innerException)
    {
        var message = $"{LogEvents.Mutation_HTTP_ErrorMsg} {context.CommandTypeName}";
        return new MutationCommandFluxException<TState, TCommand>(LogEvents.Mutation_HttpInterception_Request_Error, message, context, innerException);
    }

    public static MutationCommandFluxException<TState, TCommand> HttpUrlError(MutationCommandContext<TCommand> context, InvalidOperationException innerException)
    {
        var message = $"{LogEvents.Mutation_HTTP_URL_ErrorMsg} {context.CommandTypeName}";
        return new MutationCommandFluxException<TState, TCommand>(LogEvents.Mutation_HttpInterception_UrlConstruction_Error, message, context, innerException);
    }

    public static MutationCommandFluxException<TState, TCommand> HttpResponseUpdateError(MutationCommandContext<TCommand> context, InvalidOperationException innerException)
    {
        var message = $"{LogEvents.Mutation_HTTP_Response_Update_ErrorMsg} {context.CommandTypeName}";
        return new MutationCommandFluxException<TState, TCommand>(LogEvents.Mutation_HttpInterception_Response_Update_Error, message, context, innerException);
    }

    public static MutationCommandFluxException<TState, TCommand> MutationApplyError(MutationCommandContext<TCommand> context, InvalidOperationException innerException)
    {
        var message = $"{LogEvents.Mutation_Apply_Error} {context.CommandTypeName}";
        return new MutationCommandFluxException<TState, TCommand>(LogEvents.Mutation_Apply_Error, message, context, innerException);
    }

    public static MutationCommandFluxException<TState, TCommand> LocalStorageJsonError(MutationCommandContext<TCommand> context, JsonException innerException)
    {
        var message = $"{LogEvents.Mutation_LocalStorage_JSON_ErrorMsg} {context.CommandTypeName}";
        return new MutationCommandFluxException<TState, TCommand>(LogEvents.Mutation_SaveLocalStorage_JSON_Error, message, context, innerException);
    }

    public static MutationCommandFluxException<TState, TCommand> LocalStorageJsonError(MutationCommandContext<TCommand> context, NotSupportedException innerException)
    {
        var message = $"{LogEvents.Mutation_LocalStorage_JSON_ErrorMsg} {context.CommandTypeName}";
        return new MutationCommandFluxException<TState, TCommand>(LogEvents.Mutation_SaveLocalStorage_JSON_Error, message, context, innerException);
    }

    public static MutationCommandFluxException<TState, TCommand> LocalStorageError(MutationCommandContext<TCommand> context, Exception innerException)
    {
        var message = $"{LogEvents.Mutation_LocalStorage_ErrorMsg} {context.CommandTypeName}";
        return new MutationCommandFluxException<TState, TCommand>(LogEvents.Mutation_SaveLocalStorage_Error, message, context, innerException);
    }

    public static MutationCommandFluxException<TState, TCommand> UnhandledError(MutationCommandContext<TCommand> context, Exception innerException)
    {
        var message = $"{LogEvents.Mutation_Unhandled_ErrorMsg} {context.CommandTypeName}";
        return new MutationCommandFluxException<TState, TCommand>(LogEvents.Mutation_Unhandled_Error, message, context, innerException);
    }

    public override string ToString()
    {
        return $"{Message}" +
            $"{Environment.NewLine}" +
            $"CommandID: {Context.RequestId}" +
            $"Parameters: {JsonSerializer.Serialize(Context)}" +
            $"{base.ToString()}";
    }
}
