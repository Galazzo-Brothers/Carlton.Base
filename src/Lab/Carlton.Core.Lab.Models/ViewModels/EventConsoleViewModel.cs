namespace Carlton.Core.Lab.Models.ViewModels;

public sealed record EventConsoleViewModel
{
    public IEnumerable<ComponentRecordedEvent> RecordedEvents { get; init; } = new List<ComponentRecordedEvent>();
};



