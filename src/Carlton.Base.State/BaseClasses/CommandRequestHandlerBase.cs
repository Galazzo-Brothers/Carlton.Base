namespace Carlton.Base.State;

public abstract class CommandRequestHandlerBase<TCommand> : IRequestHandler<CommandRequest<TCommand>>
    where TCommand : ICommand
{
    protected ICommandProcessor Processor { get; init; }

    protected CommandRequestHandlerBase(ICommandProcessor processor)
        => Processor = processor;

    public async Task Handle(CommandRequest<TCommand> request, CancellationToken cancellationToken)
    {
        await Processor.ProcessCommand(request.Sender, request.Command);
    }
}
