namespace Carlton.Core.Components.Flux.Handlers;

public class ViewModelQueryHandler<TState, TViewModel> : IViewModelQueryHandler<TState, TViewModel>
{
    private readonly TState _state;
    private readonly ILogger<ViewModelQueryHandler<TState, TViewModel>> _logger;

    public ViewModelQueryHandler(TState state, ILogger<ViewModelQueryHandler<TState, TViewModel>> logger)
        => (_state, _logger) = (state, logger);

    public Task<TViewModel> Handle(ViewModelQuery query, CancellationToken cancellationToken)
    {
        Log.ViewModelMappingStarted(_logger, typeof(TViewModel).GetDisplayName());
        return Task.FromResult(_state.Adapt<TViewModel>());
        Log.ViewModelMappingCompleted(_logger, typeof(TViewModel).GetDisplayName());
    }
}


