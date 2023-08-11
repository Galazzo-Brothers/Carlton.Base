namespace Carlton.Core.InProcessMessaging.Queries;

public interface IQueryHandler<in TQuery, TQueryResult>
{
    public Task<TQueryResult> Handle(TQuery query, CancellationToken cancellationToken);
}





