namespace Carlton.Core.Flux.Debug.Models.Commands;

public class LoadLogMessagesCommand
{
    public IEnumerable<LogMessage> LogMessages { get; init; } = new List<LogMessage>();
}
