namespace Carlton.Core.Lab.Models.Commands;

/// <summary>
/// Represents a command to record an event.
/// </summary>
public sealed record RecordEventCommand
{
	/// <summary>
	/// Gets or initializes the name of the recorded event.
	/// </summary>
	[Required]
	public required string RecordedEventName { get; init; }

	/// <summary>
	/// Gets or initializes the arguments of the recorded event.
	/// </summary>
	public object EventArgs { get; init; } = new object();
};

