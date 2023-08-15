using Microsoft.JSInterop;
using System.Text.Json;

namespace Carlton.Core.Components.Flux;


public class MutationCommandFluxException<TState, TCommand> : Exception
{
    private const string ErrorMessage = $"An exception occurred during a MutationCommand of type {nameof(TCommand)}";

    public int EventID { get; init; }
    public MutationCommand Command { get; init; }

    public MutationCommandFluxException(MutationCommand command, Exception innerException) 
        : this(ErrorMessage, LogEvents.Mutation_Unhandled_Error, command, innerException)
    {
    }

    private MutationCommandFluxException(string message, int eventID, MutationCommand command, Exception innerException)
        : base(message, innerException)
    {
        Command = command;
        EventID = eventID;
    }

    public static MutationCommandFluxException<TState, TCommand> ValidationError(MutationCommand command, ValidationException innerException)
    {
        return new MutationCommandFluxException<TState, TCommand>(LogEvents.Mutation_Validation_ErrorMsg, LogEvents.Mutation_Validation_Error, command, innerException);
    }

    public static MutationCommandFluxException<TState, TCommand> JsonError(MutationCommand command, JsonException innerException)
    {
        return new MutationCommandFluxException<TState, TCommand>(LogEvents.Mutation_JSON_ErrorMsg, LogEvents.Mutation_JSON_Error, command, innerException);
    }

    public static MutationCommandFluxException<TState, TCommand> HttpError(MutationCommand command, HttpRequestException innerException)
    {
        return new MutationCommandFluxException<TState, TCommand>(LogEvents.ViewModel_HTTP_ErrorMsg, LogEvents.Mutation_HTTP_Error, command, innerException);
    }

    public static MutationCommandFluxException<TState, TCommand> JSInteropError(MutationCommand command, JSException innerException) 
    {
        return new MutationCommandFluxException<TState, TCommand>(LogEvents.Mutation_JSInterop_ErrorMsg, LogEvents.Mutation_JSInterop_Error, command, innerException);
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
