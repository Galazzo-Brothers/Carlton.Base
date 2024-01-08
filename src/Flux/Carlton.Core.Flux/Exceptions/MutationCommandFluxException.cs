namespace Carlton.Core.Flux.Exceptions;


public class MutationCommandFluxException<TState, TCommand> : FluxException
{
    public MutationCommandContext<TCommand> Context { get; init; }

    public MutationCommandFluxException(MutationCommandContext<TCommand> command, Exception innerException) 
        : this(LogEvents.Mutation_Error, LogEvents.Mutation_Unhandled_ErrorMsg, command, innerException)
    {
    }

    private MutationCommandFluxException(int eventID, string message, MutationCommandContext<TCommand> context, Exception innerException)
        : base(eventID, message, innerException)
    {
        Context = context;
    }

    public static MutationCommandFluxException<TState, TCommand> ValidationError(MutationCommandContext<TCommand> context, ValidationException innerException)
    {
        return new MutationCommandFluxException<TState, TCommand>(LogEvents.Mutation_Validation_Error, LogEvents.Mutation_Validation_ErrorMsg, context, innerException);
    }

    public static MutationCommandFluxException<TState, TCommand> HttpJsonError(MutationCommandContext<TCommand> context, JsonException innerException)
    {
        return new MutationCommandFluxException<TState, TCommand>(LogEvents.Mutation_HttpInterception_Response_JSON_Error, LogEvents.Mutation_HTTP_JSON_ErrorMsg, context, innerException);
    }

    public static MutationCommandFluxException<TState, TCommand> HttpJsonError(MutationCommandContext<TCommand> context, NotSupportedException innerException)
    {
        return new MutationCommandFluxException<TState, TCommand>(LogEvents.Mutation_HttpInterception_Response_JSON_Error, LogEvents.Mutation_HTTP_JSON_ErrorMsg, context, innerException);
    }

    public static MutationCommandFluxException<TState, TCommand> HttpError(MutationCommandContext<TCommand> context, HttpRequestException innerException)
    {
        return new MutationCommandFluxException<TState, TCommand>(LogEvents.Mutation_HttpInterception_Request_Error, LogEvents.Mutation_HTTP_ErrorMsg, context, innerException);
    }

    public static MutationCommandFluxException<TState, TCommand> HttpUrlError(MutationCommandContext<TCommand> context, InvalidOperationException innerException)
    {
        return new MutationCommandFluxException<TState, TCommand>(LogEvents.Mutation_HttpInterception_UrlConstruction_Error, LogEvents.Mutation_HTTP_URL_ErrorMsg, context, innerException);
    }

    public static MutationCommandFluxException<TState, TCommand> HttpResponseUpdateError(MutationCommandContext<TCommand> context, InvalidOperationException innerException)
    {
        return new MutationCommandFluxException<TState, TCommand>(LogEvents.Mutation_HttpInterception_Response_Update_Error, LogEvents.Mutation_HTTP_Response_Update_ErrorMsg, context, innerException);
    }

    public static MutationCommandFluxException<TState, TCommand> LocalStorageJsonError(MutationCommandContext<TCommand> context, JsonException innerException)
    {
        return new MutationCommandFluxException<TState, TCommand>(LogEvents.Mutation_SaveLocalStorage_JSON_Error, LogEvents.Mutation_LocalStorage_JSON_ErrorMsg, context, innerException);
    }

    public static MutationCommandFluxException<TState, TCommand> LocalStorageJsonError(MutationCommandContext<TCommand> context, NotSupportedException innerException)
    {
        return new MutationCommandFluxException<TState, TCommand>(LogEvents.Mutation_SaveLocalStorage_JSON_Error, LogEvents.Mutation_LocalStorage_JSON_ErrorMsg, context, innerException);
    }

    public static MutationCommandFluxException<TState, TCommand> LocalStorageError(MutationCommandContext<TCommand> context, Exception innerException)
    {
        return new MutationCommandFluxException<TState, TCommand>(LogEvents.Mutation_SaveLocalStorage_Error, LogEvents.Mutation_LocalStorage_ErrorMsg, context, innerException);
    }

    public override string ToString()
    {
        return $"{Message}" +
            $"{Environment.NewLine}" +
            $"CommandID: {Context.RequestID}" +
            $"Parameters: {JsonSerializer.Serialize(Context)}" +
            $"{base.ToString()}";
    }
}
