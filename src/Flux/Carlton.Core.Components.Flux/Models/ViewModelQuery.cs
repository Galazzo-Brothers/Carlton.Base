namespace Carlton.Core.Components.Flux.Models;

public record ViewModelQuery
{
    public Guid QueryTraceID { get; }

    public ViewModelQuery()
      => QueryTraceID = Guid.NewGuid();
}
