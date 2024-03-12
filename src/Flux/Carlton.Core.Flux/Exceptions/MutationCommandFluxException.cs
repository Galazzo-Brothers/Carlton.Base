using System.Text;
namespace Carlton.Core.Flux.Exceptions;

public class MutationCommandFluxException<TCommand>(string message,
	int eventId,
	TCommand command,
	Exception innerException) : FluxException(message, eventId, FluxOperationKind.CommandMutation, innerException)
{
	public TCommand Command { get; init; } = command;
	public string CommandTypeName { get => typeof(TCommand).GetDisplayName(); }

	public MutationCommandFluxException(
		string message,
		int eventId,
		TCommand command)
		: this(message, eventId, command, null)
	{
	}

	public override string ToString()
	{
		var sb = new StringBuilder(base.ToString());
		sb.AppendLine($"MutationCommand Type Name: {CommandTypeName}");
		return sb.ToString();
	}
}
