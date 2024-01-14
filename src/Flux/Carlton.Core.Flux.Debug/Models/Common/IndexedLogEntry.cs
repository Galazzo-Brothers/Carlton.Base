namespace Carlton.Core.Flux.Debug.Models.Common;

public record IndexedLogEntry
{
    public required string Key { get; set; }
    public required string IndexDate { get; set; }
    //public required IEnumerable<LogEntry> LogEntries { get; set; }
}



