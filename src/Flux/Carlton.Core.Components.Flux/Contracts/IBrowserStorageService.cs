using Carlton.Core.Components.Flux.Services;
using Carlton.Core.Utilities.Logging;

namespace Carlton.Core.Components.Flux.Contracts;

public interface IBrowserStorageService
{
    public event Action LogsCleared;
    public Task CommitLogs();
    public Task<IEnumerable<IndexDBLogMessage>> GetLogs(DateTime dateTime);
    public Task ClearLogs();
}
