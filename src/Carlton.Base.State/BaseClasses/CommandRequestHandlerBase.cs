namespace Carlton.Base.State;

public abstract class CommandRequestHandlerBase<TCommand> : IRequestHandler<CommandRequest<TCommand>>
    where TCommand : ICommand
{
    private readonly ILogger<CommandRequestHandlerBase<TCommand>> _logger;

    protected ICommandProcessor Processor { get; init; }

    protected CommandRequestHandlerBase(ICommandProcessor processor, ILogger<CommandRequestHandlerBase<TCommand>> logger)
        => (Processor, _logger) = (processor, logger);

    public async Task Handle(CommandRequest<TCommand> request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Handling {RequestName}: {Request}", request.RequestName, request);
            await Processor.ProcessCommand(request.Sender, request.Command);
            _logger.LogInformation("Handled {RequestName}: {Request}", request.RequestName, request);
        }
        catch(Exception ex)
        {
            throw new CommandException<TCommand>(request, ex);
        }
    }
}
