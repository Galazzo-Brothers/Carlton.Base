using Carlton.Core.Components.Flux.Mutations;

namespace Carlton.Core.Components.Flux;

public class CommandHandler<TCommand> : ICommandHandler<TCommand>
{
    private readonly IStateMutator _mutator;

    public CommandHandler(IStateMutator mutator)
        => _mutator = mutator;

    public async Task<Unit> Handle(CommandRequest<TCommand> request, CancellationToken cancellationToken)
    {
        await _mutator.Mutate(request.Sender, request.Command);
        return Unit.Value;
    }
}
