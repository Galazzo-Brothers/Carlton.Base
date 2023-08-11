namespace Carlton.Core.InProcessMessaging.Commands;


public interface ICommandHandler<TRequest, TResponse>
    where TRequest : ICommand<TResponse>
{
    public Task<TResponse> Handle(ICommand<TResponse> request, CancellationToken cancellationToken);
}
