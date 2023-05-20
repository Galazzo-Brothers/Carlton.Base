namespace Carlton.Base.State;

public class CommandDispatcher : ICommandDispatcher
{
    private readonly IServiceProvider _serviceProvider;
    
    public CommandDispatcher(IServiceProvider serviceProvider)
        => _serviceProvider = serviceProvider;

    public Task<Unit> Dispatch<TCommand>(CommandRequest<TCommand> request, CancellationToken cancellationToken) where TCommand : ICommand
    {
        var handler = _serviceProvider.GetRequiredService<ICommandHandler<TCommand>>();
        return handler.Handle(request, cancellationToken);
    }
}
