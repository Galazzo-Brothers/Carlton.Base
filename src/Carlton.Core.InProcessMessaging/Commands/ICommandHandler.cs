namespace Carlton.Core.InProcessMessaging.Commands;


public interface ICommandHandler<in TCommand, TCommandResult>
{
    public Task<TCommandResult> Handle(TCommand command, CancellationToken cancellationToken);
}

