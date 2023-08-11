using Carlton.Core.Components.Flux.Contracts;

namespace Carlton.Core.Components.Flux.Handlers;

public class BaseViewModelQueryHandler<TState, TViewModel> : IViewModelQueryHandler<TState, TViewModel>
{
    private readonly TState _state;

    public BaseViewModelQueryHandler(TState state)
        => _state = state;

    public Task<TViewModel> Handle(ViewModelQuery query, CancellationToken cancellationToken)
    {
        return Task.FromResult(_state.Adapt<TViewModel>());
    }
}


