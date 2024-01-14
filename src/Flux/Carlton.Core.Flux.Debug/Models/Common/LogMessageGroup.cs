namespace Carlton.Core.Flux.Debug.Models.Common;

public class LogMessageGroup
{
    public required LogMessage ParentEntry { get; set; }
    public IEnumerable<LogMessage> ChildEntries { get; set; } = new List<LogMessage>();
}
