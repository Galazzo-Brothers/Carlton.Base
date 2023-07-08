namespace Carlton.Base.State;

public class CommandHandler<TCommand> : ICommandHandler<TCommand>
{
    private readonly IStateProcessor _processor;

    public CommandHandler(IStateProcessor processor)
        => _processor = processor;

    public async Task<Unit> Handle(CommandRequest<TCommand> request, CancellationToken cancellationToken)
    {
        await _processor.ProcessCommand(request.Sender, request.Command);
        return Unit.Value;
    }
}
