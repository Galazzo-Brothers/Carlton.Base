using Carlton.Core.Utilities.Logging;

namespace Carlton.Core.Flux.Contracts;

public interface IBrowserStorageService
{
    public Task CommitLogs();
    public Task<IEnumerable<LogMessage>> GetLogs(DateTime dateTime);
    public Task ClearLogs();
    public Task SaveState<TState>(TState state);
}
