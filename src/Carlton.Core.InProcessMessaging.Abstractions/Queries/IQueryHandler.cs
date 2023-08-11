namespace Carlton.Core.InProcessMessaging.Queries;

public interface IQueryHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{
    public Task<TResponse> Handle(IQuery<TResponse> request, CancellationToken cancellationToken);
}





