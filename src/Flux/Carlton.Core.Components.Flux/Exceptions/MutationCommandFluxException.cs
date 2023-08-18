using Carlton.Core.Components.Flux.Exceptions;
using Microsoft.JSInterop;
using System.Text.Json;

namespace Carlton.Core.Components.Flux;


public class MutationCommandFluxException<TState, TCommand> : FluxException
{
    private const string ErrorMessage = $"An exception occurred during a MutationCommand of type {nameof(TCommand)}";

    public MutationCommand Command { get; init; }

    public MutationCommandFluxException(MutationCommand command, Exception innerException) 
        : this(LogEvents.Mutation_Unhandled_Error, ErrorMessage, command, innerException)
    {
    }

    private MutationCommandFluxException(int eventID, string message, MutationCommand command, Exception innerException)
        : base(eventID, message, innerException)
    {
        Command = command;
    }

    public static MutationCommandFluxException<TState, TCommand> ValidationError(MutationCommand command, ValidationException innerException)
    {
        return new MutationCommandFluxException<TState, TCommand>(LogEvents.Mutation_Validation_Error, LogEvents.Mutation_Validation_ErrorMsg, command, innerException);
    }

    public static MutationCommandFluxException<TState, TCommand> JsonError(MutationCommand command, JsonException innerException)
    {
        return new MutationCommandFluxException<TState, TCommand>(LogEvents.Mutation_JSON_Error, LogEvents.Mutation_JSON_ErrorMsg, command, innerException);
    }

    public static MutationCommandFluxException<TState, TCommand> HttpError(MutationCommand command, HttpRequestException innerException)
    {
        return new MutationCommandFluxException<TState, TCommand>(LogEvents.Mutation_HTTP_Error, LogEvents.ViewModel_HTTP_ErrorMsg, command, innerException);
    }

    public static MutationCommandFluxException<TState, TCommand> JSInteropError(MutationCommand command, JSException innerException) 
    {
        return new MutationCommandFluxException<TState, TCommand>(LogEvents.Mutation_JSInterop_Error, LogEvents.Mutation_JSInterop_ErrorMsg, command, innerException);
    }

    public override string ToString()
    {
        return $"{ErrorMessage}" +
            $"{Environment.NewLine}" +
            $"CommandID: {Command.CommandID}" +
            $"Parameters: {JsonSerializer.Serialize(Command)}" +
            $"{base.ToString()}";
    }
}
