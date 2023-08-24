namespace Carlton.Core.Components.Lab.Models;

public sealed record ClearEventsCommand : MutationCommand
{
    public ClearEventsCommand(object sender) : base(sender)
    {
    }
}
