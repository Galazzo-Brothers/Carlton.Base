using System.Text.Json;

namespace Carlton.Core.Components.Flux;


public class MutationCommandFluxException<TCommand> : Exception
{
    private const string ErrorMessage = $"An exception occurred during a command of type {nameof(TCommand)}";

    public MutationCommand Command { get; init; }

    public MutationCommandFluxException(MutationCommand command) : this(ErrorMessage, command)
    {
    }

    public MutationCommandFluxException(string message, MutationCommand command)
        : base(message)
    {
        Command = command;
    }

    public MutationCommandFluxException(string message, MutationCommand command, Exception innerException) : base(message, innerException)
    {
        Command = command;
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
