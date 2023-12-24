namespace Carlton.Core.Flux.Models;

public record ViewModelQuery
{
    public Guid QueryTraceID { get; }

    public ViewModelQuery()
      => QueryTraceID = Guid.NewGuid();

    public ViewModelQuery(Guid queryTraceID)
     => QueryTraceID = queryTraceID;
}
