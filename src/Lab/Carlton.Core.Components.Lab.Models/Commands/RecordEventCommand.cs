namespace Carlton.Core.Components.Lab.Models.Commands;

public sealed record RecordEventCommand(string RecordedEventName, object EventArgs) : MutationCommand;


