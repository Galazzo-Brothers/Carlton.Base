namespace Carlton.Core.Flux.Debug.Models.Common;

public class LogEntryGroup
{
    public required LogEntry StartingEntry { get; set; }
    public required LogEntry EndingEntry { get; set; }
    public IEnumerable<LogEntry> ChildEntries { get; set; } = new List<LogEntry>();
}
