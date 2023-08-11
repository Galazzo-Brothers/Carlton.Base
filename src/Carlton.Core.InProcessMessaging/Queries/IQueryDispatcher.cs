namespace Carlton.Core.InProcessMessaging.Queries;

public interface IQueryDispatcher
{
    public Task<TQueryResult> Dispatch<TQuery, TQueryResult>(TQuery query, CancellationToken cancellationToken);
}


