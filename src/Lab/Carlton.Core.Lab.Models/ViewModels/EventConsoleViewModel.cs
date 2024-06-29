namespace Carlton.Core.Lab.Models.ViewModels;

/// <summary>
/// Represents the view model for the event console.
/// </summary>
public sealed record EventConsoleViewModel
{
	/// <summary>
	/// Gets or initializes the recorded events.
	/// </summary>
	[Required]
	public IEnumerable<ComponentRecordedEvent> RecordedEvents { get; init; } = new List<ComponentRecordedEvent>();
};



