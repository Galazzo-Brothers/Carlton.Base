namespace Carlton.Core.Lab.Models.Common;

/// <summary>
/// Represents a recorded event of a component.
/// </summary>
public record ComponentRecordedEvent
{
	/// <summary>
	/// Gets or initializes the name of the recorded event.
	/// </summary>
	[Required(AllowEmptyStrings = false)]
	public required string Name { get; init; }

	// <summary>
	/// Gets or initializes the object associated with the recorded event.
	/// </summary>
	[Required]
	public required object EventObj { get; init; }
}
