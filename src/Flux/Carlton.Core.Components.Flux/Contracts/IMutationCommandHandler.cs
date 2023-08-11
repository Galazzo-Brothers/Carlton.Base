using Carlton.Core.Components.Flux.Models;
using Carlton.Core.InProcessMessaging.Commands;

namespace Carlton.Core.Components.Flux.Contracts;

public interface IMutationCommandHandler<TState, TCommand> : ICommandHandler<TCommand, Unit>
{
}
