namespace Carlton.Core.Flux.Debug.Models.Commands;

public class LoadLogMessagesCommand
{
    public IReadOnlyList<LogMessage> LogMessages { get; init; } = new List<LogMessage>();
}
