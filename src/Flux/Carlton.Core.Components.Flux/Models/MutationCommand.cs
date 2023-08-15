namespace Carlton.Core.Components.Flux.Models;

public record MutationCommand(Guid CommandID)
{
    public MutationCommand() : this(Guid.NewGuid())
    { 
    } 
}
