namespace Carlton.Core.Lab.Models.ViewModels;

public sealed record EventConsoleViewModel
{
	[Required]
	public IEnumerable<ComponentRecordedEvent> RecordedEvents { get; init; } = new List<ComponentRecordedEvent>();
};



