namespace Carlton.Core.Components.Flux.Models;

public record class MutationCommand
{
    public Guid CommandID { get; }

    public MutationCommand()
    {
        CommandID = Guid.NewGuid();
    }
}
