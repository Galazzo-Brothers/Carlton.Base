using Blazored.LocalStorage;

namespace Carlton.Core.Components.Flux.Decorators.Mutations;

public class MutationLocalStorageDecorator<TState> : IMutationCommandDispatcher<TState>
{
    private readonly IMutationCommandDispatcher<TState> _decorated;
    private readonly IFluxState<TState> _fluxState;
    private readonly ILocalStorageService _localStorage;

    public MutationLocalStorageDecorator(
        IMutationCommandDispatcher<TState> decorated,
        IFluxState<TState> fluxState,
        ILocalStorageService localStorage)
    {
        _decorated = decorated;
        _fluxState = fluxState;
        _localStorage = localStorage;
    }

    public async Task<Unit> Dispatch<TCommand>(object sender, TCommand command, CancellationToken cancellationToken)
        where TCommand : MutationCommand
    {
        //Continue with dispatch and update the state store
        await _decorated.Dispatch(sender, command, cancellationToken);

        //Update LocalStorage
        await _localStorage.SetItemAsync("carltonFluxState", _fluxState.State);

        return Unit.Value;
    }
}
