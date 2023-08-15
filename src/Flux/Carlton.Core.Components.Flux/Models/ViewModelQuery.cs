namespace Carlton.Core.Components.Flux.Models;

public record ViewModelQuery(Guid QueryID)
{
    public ViewModelQuery() : this(Guid.NewGuid())
    { 
    }   
}
