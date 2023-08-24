namespace Carlton.Core.Components.Flux.Models;

public record ViewModelQuery(object Sender, Guid QueryID)
{
    public ViewModelQuery(object sender) : this(sender, Guid.NewGuid())
    { 
    }   
}
