namespace Carlton.Core.Components.Flux.Handlers;

public class ViewModelQueryHandler<TState, TViewModel> : IViewModelQueryHandler<TState, TViewModel>
{
    private readonly TState _state;

    public ViewModelQueryHandler(TState state)
        => _state = state;

    public Task<TViewModel> Handle(ViewModelQuery query, CancellationToken cancellationToken)
    {
        return Task.FromResult(_state.Adapt<TViewModel>());
    }
}


