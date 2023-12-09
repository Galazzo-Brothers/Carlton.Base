using Carlton.Core.Utilities.Logging;

namespace Carlton.Core.Components.Flux.Contracts;

public interface IBrowserStorageService
{
    public Task CommitLogs();
    public Task<IEnumerable<LogMessage>> GetLogs(DateTime dateTime);
}
