namespace Carlton.Core.InProcessMessaging.Commands;

public class CommandDispatcher : ICommandDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public CommandDispatcher(IServiceProvider serviceProvider)
        => _serviceProvider = serviceProvider;

 
    public async Task<TCommandResult> Dispatch<TCommand, TCommandResult>(TCommand command, CancellationToken cancellationToken)
    {
        var handler = _serviceProvider.GetRequiredService<ICommandHandler<TCommand, TCommandResult>>();
        return await handler.Handle(command, cancellationToken);
    }

    public async Task<TCommandResult> Dispatch<TCommandResult>(object command, CancellationToken cancellation)
    {
        dynamic handler = _serviceProvider.GetRequiredService(typeof(ICommandHandler<,>).MakeGenericType(command.GetType(), typeof(TCommandResult)));
        dynamic dynamicCommand = command;
        return await handler.Handle(dynamicCommand, cancellation);
    }

    public async Task<Unit> Dispatch(object command, CancellationToken cancellationToken)
    {
        return await Dispatch<Unit>(command, cancellationToken);
    }
}
