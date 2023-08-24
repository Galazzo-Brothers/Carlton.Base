namespace Carlton.Core.Components.Lab.Models;

public sealed record RecordEventCommand(object Sender, string RecordedEventName, object EventArgs) 
    : MutationCommand(Sender)
{
}


