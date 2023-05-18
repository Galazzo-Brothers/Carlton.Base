namespace Carlton.Base.State;

public abstract class CommandRequestHandlerBase<TCommand> : IRequestHandler<CommandRequest<TCommand>>
    where TCommand : ICommand
{
    private readonly ILogger<CommandRequestHandlerBase<TCommand>> _logger;

    protected IStateProcessor Processor { get; init; }

    protected CommandRequestHandlerBase(IStateProcessor processor, ILogger<CommandRequestHandlerBase<TCommand>> logger)
        => (Processor, _logger) = (processor, logger);

    public async Task Handle(CommandRequest<TCommand> request, CancellationToken cancellationToken)
    {
        try
        {
            Log.CommandRequestHandlingStarted(_logger, request.RequestName, request);
            await Processor.ProcessCommand(request.Sender, request.Command);
            request.MarkCompleted();
            Log.CommandRequestHandlingCompleted(_logger, request.RequestName, request);
        }
        catch(Exception ex)
        {
            request.MarkErrored();
            throw new CommandException<TCommand>(request, ex);
        }
    }
}
