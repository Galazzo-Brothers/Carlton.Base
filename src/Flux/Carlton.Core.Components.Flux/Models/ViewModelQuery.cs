namespace Carlton.Core.Components.Flux.Models;

public record ViewModelQuery
{
    public Guid QueryID { get; }

    public ViewModelQuery()
        => QueryID = Guid.NewGuid();
}
