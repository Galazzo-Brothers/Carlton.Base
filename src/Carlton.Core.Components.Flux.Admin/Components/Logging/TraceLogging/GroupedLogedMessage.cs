using Carlton.Core.Components.Flux.Services;
namespace Carlton.Core.Components.Flux.Admin.Components.Logging;

public class GroupedLogMessage
{
    public required IndexDBLogMessage StartingMessage { get; set; }
    public required IndexDBLogMessage EndingMessage { get; set; }
    public IEnumerable<IndexDBLogMessage> ChildMessages { get; set; } = new List<IndexDBLogMessage>();
}
