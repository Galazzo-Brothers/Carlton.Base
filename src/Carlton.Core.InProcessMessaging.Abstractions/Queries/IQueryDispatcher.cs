namespace Carlton.Core.InProcessMessaging.Queries;

public interface IQueryDispatcher
{
    public Task<TResponse> Dispatch<TResponse>(IQuery<TResponse> request, CancellationToken cancellationToken);
}


