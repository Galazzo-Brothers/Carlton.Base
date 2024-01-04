namespace Carlton.Core.Flux.Debug.Storage;

public interface ILogsDataAccess
{
    public Task CommitLogs();
    public Task<IEnumerable<IndexedLogEntry>> GetLogs(DateTime dateTime);
    public Task ClearLogs();
}
