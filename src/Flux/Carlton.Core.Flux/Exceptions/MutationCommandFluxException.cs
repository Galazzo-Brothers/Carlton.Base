using System.Text;
namespace Carlton.Core.Flux.Exceptions;

/// <summary>
/// Represents an exception specific to mutation commands in the Flux framework.
/// </summary>
/// <typeparam name="TCommand">The type of mutation command associated with the exception.</typeparam>
/// <remarks>
/// Initializes a new instance of the <see cref="MutationCommandFluxException{TCommand}"/> class with the specified message, event ID, mutation command, and inner exception.
/// </remarks>
/// <param name="message">The error message that explains the reason for the exception.</param>
/// <param name="eventId">The event ID associated with the exception.</param>
/// <param name="command">The mutation command associated with the exception.</param>
/// <param name="innerException">The exception that caused the current exception.</param>
public class MutationCommandFluxException<TCommand>(string message,
	int eventId,
	TCommand command,
	Exception innerException) : FluxException(message, eventId, FluxOperationKind.MutationCommand, innerException)
{
	/// <summary>
	/// Gets the mutation command associated with the exception.
	/// </summary>
	public TCommand Command { get; init; } = command;

	/// <summary>
	/// Gets the display name of the type of mutation command.
	/// </summary>
	public string CommandTypeName { get => typeof(TCommand).GetDisplayName(); }

	/// <summary>
	/// Initializes a new instance of the <see cref="MutationCommandFluxException{TCommand}"/> class with the specified message, event ID, mutation command, and inner exception.
	/// </summary>
	/// <param name="message">The error message that explains the reason for the exception.</param>
	/// <param name="eventId">The event ID associated with the exception.</param>
	/// <param name="command">The mutation command associated with the exception.</param>
	public MutationCommandFluxException(
		string message,
		int eventId,
		TCommand command)
		: this(message, eventId, command, null)
	{
	}

	/// <summary>
	/// Returns a string that represents the current object.
	/// </summary>
	/// <returns>A string that represents the current object.</returns>
	public override string ToString()
	{
		var sb = new StringBuilder(base.ToString());
		sb.AppendLine($"MutationCommand Type Name: {CommandTypeName}");
		return sb.ToString();
	}
}
